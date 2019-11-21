using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public Sprite PowerUpHielo;
    public Sprite nothing;
    public Sprite PowerUpEmpuje;
    public Image PowerUpUI;

    public Rigidbody2D rig;
    Vector2 pos;
    public float speed;
    public Vector2 velocity;
    public bool jump = true;
    public float jumpForce;
    public bool dash = false;
    Animator m_Animator;
    public bool InFloor;
    public int Life;
    public Transform[] RespawnPoint;
    [SerializeField]
    private bool immune = false;
    private bool OnIce = false;
    SpriteRenderer sprite;
    public float stamina;
    public const int DashCost = 25;
    public const int RegenStamina = 10;
    public const int MaxStamina = 100;
    public Scrollbar StaminaObj;
    public GameObject IceSkill;
    public GameObject MeleSkill;
    public GameObject IceEffectState;
    private bool effect;
    private bool iceInvoque = false;
    private bool meleInvoque = false;
    public GameObject[] lifeImage;
    public GameObject prefab;
    private bool defeat;
    public bool OnImpulse;
    float time;
    public GameObject Damage;
    public GameObject Respawn;
    public GameObject Flecha;
    public bool OnSkill;
    public enum Skill
    {
        none,
        ice,
        mele
    }
    static Quaternion myRotation;
    Quaternion rotation;
    public Skill ActiveSkill;
    CircleCollider2D boxCollider;
    private Quaternion rotationEnemy;
    static private bool pause;
    private bool OnDash;
    Control control;
    public Control.PlayerType player;

    public GameObject Emoji1;
    public GameObject Emoji2;
    public GameObject Emoji3;
    public GameObject Emoji4;

    bool emojiOn;
    float timeEmoji;
    Vector3 ActualRespawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        Life = 3;
        stamina = MaxStamina;
        pos = new Vector2(transform.position.x, transform.position.y);
        m_Animator = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        defeat = false;
        OnSkill = false;
        ActiveSkill = Skill.none;
        boxCollider = GetComponent<CircleCollider2D>();
        control = new Control();
        control.SetPlayer(player);
        emojiOn = false;
        timeEmoji = 0;
    }

    void Update()
    {
        if (!pause)
        {
            if (Life > 0)
            {
                myRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, 1.0f);
                if (OnIce)
                {
                    StartCoroutine("IceEffect");
                }
                else if (OnSkill)
                {
                }
                else if (OnImpulse)
                {
                    if (rotationEnemy.y == 0.0f)
                    {
                        ImpulseDamage(new Vector2(-1.0f, 0.6f));
                    }
                    else
                    {
                        ImpulseDamage(new Vector2(1.0f, 0.6f));
                    }

                }
                else if (OnDash)
                {
                    if (rotation.y == 0.0f)
                    {
                        ImpulseDash(Vector2.right * 0.7f);
                    }
                    else
                    {
                        ImpulseDash(Vector2.left * 0.7f);
                    }
                }
                else
                {

                    if (!emojiOn)
                    {
                        if (control.Emoji1())
                        {
                            Instantiate(Emoji1, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, this.transform);
                            emojiOn = true;
                        }
                        else if (control.Emoji2())
                        {
                            Instantiate(Emoji2, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, this.transform);
                            emojiOn = true;
                        }
                        else if (control.Emoji3())
                        {
                            Instantiate(Emoji3, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, this.transform);
                            emojiOn = true;
                        }
                        else if (control.Emoji4())
                        {
                            Instantiate(Emoji4, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, this.transform);
                            emojiOn = true;
                        }
                    }
                    else
                    {
                        timeEmoji += 1 * Time.deltaTime;
                        if (timeEmoji >= 2.0f)
                        {
                            timeEmoji = 0;
                            emojiOn = false;
                        }
                    }
                    pos = new Vector2(transform.position.x, transform.position.y);
                    if (control.Jump())
                    {
                        if (jump == true && InFloor)
                        {
                            StartCoroutine("Impulse");
                        }
                    }

                    if (control.Right())
                    {
                        m_Animator.SetBool("Caminata", true);
                        if (transform.rotation.y != 0)
                        {
                            transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);

                        }
                        rig.AddForce(new Vector2(2, 0) * speed, ForceMode2D.Force);

                    }
                    else if (control.Left())
                    {
                        m_Animator.SetBool("Caminata", true);
                        if (transform.rotation.y != 180)
                        {
                            transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 1.0f);

                        }
                        rig.AddForce(new Vector2(-2, 0) * speed, ForceMode2D.Force);
                    }
                    else
                    {
                        m_Animator.SetBool("Caminata", false);
                    }
                    velocity = rig.velocity;
                    if (rig.velocity.x >= 4)
                    {
                        rig.velocity = new Vector2(4, rig.velocity.y);
                    }
                    if (rig.velocity.x <= -4)
                    {
                        rig.velocity = new Vector2(-4, rig.velocity.y);
                    }

                    if (control.Skill())
                    {
                        switch (ActiveSkill)
                        {
                            case Skill.ice:
                                if (iceInvoque == false)
                                {
                                    StartCoroutine("IceSkillCD");
                                    m_Animator.SetTrigger("IceSkill");
                                    OnSkill = true;
                                    ActiveSkill = Skill.none;
                                    PowerUpUI.sprite = nothing;
                                }
                                break;
                            case Skill.mele:
                                if (meleInvoque == false)
                                {
                                    StartCoroutine("MeleSkillCD");
                                    m_Animator.SetTrigger("MeleSkill");
                                    ActiveSkill = Skill.none;
                                    PowerUpUI.sprite = nothing;
                                }
                                break;
                            default:

                                break;
                        }
                    }
                    if (stamina > 0 && stamina >= DashCost)
                    {

                        if (control.Dash())
                        {
                            StartCoroutine("Dash");
                            rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, 1.0f);
                            m_Animator.SetTrigger("Dash");
                            stamina -= DashCost;
                            OnDash = true;
                        }
                    }
                    if (stamina > MaxStamina)
                    {
                        stamina = MaxStamina;
                    }
                    if (stamina < MaxStamina)
                    {
                        stamina += RegenStamina * Time.deltaTime;
                    }
                    StaminaObj.size = stamina / MaxStamina;
                }

            }
            else
            {
                if (!defeat)
                {
                    MultipleTargetCamera.SetTargets(this.transform, 1);
                    GameManager.instance.SetPlayers(prefab);
                    defeat = true;
                }

            }
        }
    }
    IEnumerator Impulse()
    {
        rig.AddForce(new Vector2(0, 2) * jumpForce, ForceMode2D.Impulse);
        jump = false;
        InFloor = false;
        yield return new WaitForSeconds(0.2f);
        jump = true;
    }
    IEnumerator ImmunePlayer()
    {

        immune = true;
        OnImpulse = false;
        rig.velocity = Vector3.zero;
        sprite.color = new Vector4(255, 255, 255, 0);
        OnSkill = true;
        boxCollider.isTrigger = false;
        if (Life > 0)
        {

            Instantiate(Flecha, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Quaternion.identity);
            yield return new WaitForSeconds(2.0f);
            OnSkill = false;
            transform.position = ActualRespawnPoint;
            Instantiate(Respawn, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            sprite.color = new Vector4(255, 255, 0, 255);
            yield return new WaitForSeconds(2.0f);
            sprite.color = new Vector4(255, 255, 255, 255);
            immune = false;

        }
    }

    IEnumerator Dash()
    {
        dash = true;
        yield return new WaitForSeconds(0.2f);
        dash = false;
    }
    IEnumerator IceEffect()
    {
        if (!effect)
        {
            effect = true;
            Instantiate(IceEffectState, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, this.transform);
        }
        yield return new WaitForSeconds(2.0f);
        effect = false;
        OnIce = false;
    }
    IEnumerator IceSkillCD()
    {
        iceInvoque = true;
        yield return new WaitForSeconds(2.0f);
        iceInvoque = false;
    }
    IEnumerator MeleSkillCD()
    {
        meleInvoque = true;
        yield return new WaitForSeconds(0.0f);
        meleInvoque = false;
    }

    void IceOn()
    {

        if (transform.rotation.y == 0)
        {
            Instantiate(IceSkill, new Vector3(transform.position.x + 1.0F, transform.position.y, transform.position.z), Quaternion.identity);

        }
        else
        {
            Instantiate(IceSkill, new Vector3(transform.position.x - 1.0F, transform.position.y, transform.position.z), new Quaternion(0.0f, 180.0f, 0.0f, 1.0f));
        }

    }

    void MeleOn()
    {
        if (!OnIce)
        {
            if (transform.rotation.y == 0)
            {
                Instantiate(MeleSkill, new Vector3(transform.position.x + 0.5F, transform.position.y, transform.position.z), Quaternion.identity, this.transform);

            }
            else
            {
                Instantiate(MeleSkill, new Vector3(transform.position.x - 0.5F, transform.position.y, transform.position.z), new Quaternion(0.0f, 180.0f, 0.0f, 1.0f), this.transform);
            }
        }
    }
    void OnSkillSet()
    {
        OnSkill = false;
    }
    void IfDamage()
    {
        Life--;

        Instantiate(Damage, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        if (Life > 0)
        {
            int randomPoint = Random.Range(0, 3);
            ActualRespawnPoint = new Vector3(RespawnPoint[randomPoint].position.x, RespawnPoint[randomPoint].position.y, 0.0f);
            transform.position = ActualRespawnPoint;
        }


        TraumaInducer.Shake();

        lifeImage[Life].SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == "Floor" || coll.transform.tag == "Player")
        {
            InFloor = true;
        }
        else
        {
            InFloor = false;
        }
        if (!immune)
        {
            if (coll.transform.tag == "EnemyObject" || coll.transform.tag == "Lava")
            {
                IfDamage();
                OnDash = false;
                StartCoroutine("ImmunePlayer");
            }
            if (coll.transform.tag == "Ice")
            {
                OnIce = true;
            }
            if (coll.transform.tag == "Mele")
            {
                TraumaInducer.Shake();
                rotationEnemy = Player.GetQuaternion();
                OnImpulse = true;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D coll)
    {
        if (!immune)
        {
            if ((coll.transform.tag == "EnemyObject" || coll.transform.tag == "Lava"))
            {
                IfDamage();
                OnDash = false;
                StartCoroutine("ImmunePlayer");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "IcePowerUp")
        {
            PowerUpUI.sprite = PowerUpHielo;
            ActiveSkill = Skill.ice;
        }
        if (coll.transform.tag == "EmpujePowerUp")
        {
            PowerUpUI.sprite = PowerUpEmpuje;
            ActiveSkill = Skill.mele;
        }

    }
    static public Quaternion GetQuaternion()
    {
        return myRotation;
    }
    void ImpulseDamage(Vector2 direction)
    {
        if (!boxCollider.isTrigger)
        {
            boxCollider.isTrigger = true;
        }
        rig.AddForce(direction * 2, ForceMode2D.Impulse);
        time += 1 * Time.deltaTime;
        if (OnImpulse == false)
        {
            boxCollider.isTrigger = false;
        }

        if (time >= 1.0f)
        {
            boxCollider.isTrigger = false;
            OnImpulse = false;
            OnSkill = true;
            time = 0;
        }
    }
    void ImpulseDash(Vector2 direction)
    {
        rig.velocity = direction * 30;
        time += 1 * Time.deltaTime;
        if (time >= 0.5f)
        {
            OnDash = false;
            time = 0;
        }
    }
    static public void SetPause(bool _pause)
    {
        pause = _pause;
    }
    static public bool GetPause()
    {
        return pause;
    }
}
