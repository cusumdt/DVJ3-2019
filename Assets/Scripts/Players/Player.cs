using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    #region Enums
    public enum Skill
    {
        none,
        ice,
        mele
    }
    enum PlayerState
    {
        OnIce,
        OnSkill,
        OnImpulse,
        OnDash,
        Normal
    }
    #endregion

    #region Control
    [SerializeField]
    Control.PlayerType player;
    Control control;
    #endregion
    public Rigidbody2D rig;
    #region Floats
    [SerializeField]
    float speed;
    [SerializeField]
    float jumpForce;
    float stamina;
    float time;
    float timeEmoji;
    #endregion
    #region Vector2
    Vector2 velocity;
    Vector2 pos;
    #endregion
    #region Bools
    bool jump = true;
    bool dash = false;
    bool InFloor;
    bool iceInvoque = false;
    bool meleInvoque = false;
    bool defeat;
    bool pause;
    bool effect;
    bool immune = false;
    bool emojiOn;
    #endregion
    #region INT
    const int DashCost = 25;
    const int RegenStamina = 10;
    const int MaxStamina = 100;
    [SerializeField]
    int Life;
    #endregion
    #region GameObjects
    [SerializeField]
    GameObject IceSkill;
    [SerializeField]
    GameObject MeleSkill;
    [SerializeField]
    GameObject IceEffectState;
    [SerializeField]
    GameObject[] lifeImage;
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    GameObject Damage;
    [SerializeField]
    GameObject Respawn;
    [SerializeField]
    GameObject Flecha;
    [SerializeField]
    GameObject Emoji1;
    [SerializeField]
    GameObject Emoji2;
    [SerializeField]
    GameObject Emoji3;
    [SerializeField]
    GameObject Emoji4;
    [SerializeField]
    GameObject Enemy;
    #endregion
    #region Quaternions
    Quaternion myRotation;
    Quaternion rotationEnemy;
    #endregion
    #region Others
    Skill ActiveSkill;
    CircleCollider2D circleCollider;
    Animator m_Animator;
    SpriteRenderer sprite;
    [SerializeField]
    Transform[] RespawnPoint;
    [SerializeField]
    Scrollbar StaminaObj;
    [SerializeField]
    Image PowerUpUI;
    PlayerState playerState;
    Vector3 ActualRespawnPoint;
    #endregion
    #region Sprites
    [SerializeField]
    Sprite PowerUpHielo;
    [SerializeField]
    Sprite PowerUpEmpuje;
    [SerializeField]
    Sprite nothing;
    #endregion
 

    void Start()
    {
        Life = 3;
        stamina = MaxStamina;
        pos = new Vector2(transform.position.x, transform.position.y);
        defeat = false;
        ActiveSkill = Skill.none;
        control = new Control();
        control.SetPlayer(player);
        emojiOn = false;
        timeEmoji = 0;
        #region GetComponent
        m_Animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        #endregion
        #region SetEvents
        GameManager.OnPause += PauseInFalse;
        PauseManager.OnPause += SetPause;
        #endregion
        playerState = PlayerState.Normal;

    }

    void Update()
    {
        if (!pause)
        {
            if (Life > 0)
            {
                switch (playerState)
                {
                    case PlayerState.OnIce:
                        StartCoroutine("IceEffect");
                        break;
                    case PlayerState.OnSkill:
                        break;
                    case PlayerState.OnImpulse:
                        if (rotationEnemy.y == -1f)
                        {

                            ImpulseDamage(new Vector2(-1.0f, 0.6f));
                        }
                        else
                        {
                            ImpulseDamage(new Vector2(1.0f, 0.6f));
                        }
                        Debug.Log(rotationEnemy.y);
                        break;
                    case PlayerState.OnDash:
                        myRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, 1.0f);
                        if (myRotation.y == 0.0f)
                        {
                            ImpulseDash(Vector2.right * 0.7f);
                        }
                        else
                        {
                            ImpulseDash(Vector2.left * 0.7f);
                        }
                        break;
                    case PlayerState.Normal:
                        if (!emojiOn)
                        {
                            if (control.Emoji1())
                            {
                                EmojiInvoque(Emoji1);
                            }
                            else if (control.Emoji2())
                            {
                                EmojiInvoque(Emoji2);
                            }
                            else if (control.Emoji3())
                            {
                                EmojiInvoque(Emoji3);
                            }
                            else if (control.Emoji4())
                            {
                                EmojiInvoque(Emoji4);
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
                                transform.rotation = Quaternion.Euler(0, 0, 0);

                            }
                            rig.AddForce(new Vector2(2, 0) * speed, ForceMode2D.Force);

                        }
                        else if (control.Left())
                        {
                            m_Animator.SetBool("Caminata", true);
                            if (transform.rotation.y != 180)
                            {
                                transform.rotation = Quaternion.Euler(0, 180, 0);

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
                                        playerState = PlayerState.OnSkill;
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
                                myRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, 1.0f);
                                m_Animator.SetTrigger("Dash");
                                stamina -= DashCost;
                                playerState = PlayerState.OnDash;
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
                        break;
                    default:
                        break;
                }
            }
            else
            {
                if (!defeat)
                {
                    MultipleTargetCamera.SetTargets(this.transform, 1);
                    GameManager.Get().SetPlayers(prefab);
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
        playerState = PlayerState.Normal;
        rig.velocity = Vector3.zero;
        sprite.color = new Vector4(255, 255, 255, 0);
        playerState = PlayerState.OnSkill;
        circleCollider.isTrigger = false;
        if (Life > 0)
        {
            Instantiate(Flecha, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Quaternion.identity);
            yield return new WaitForSeconds(2.0f);
            playerState = PlayerState.Normal;
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
        playerState = PlayerState.Normal;
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
        InstantiateSkill(IceSkill, false);
    }

    void MeleOn()
    {
        InstantiateSkill(MeleSkill, true);
    }

    void InstantiateSkill(GameObject Skill, bool Children)
    {
        if (playerState != PlayerState.OnIce)
        {
            if (Children)
            {
                if (transform.rotation.y == 0)
                {
                    Instantiate(Skill, new Vector3(transform.position.x + 0.5F, transform.position.y, transform.position.z), Quaternion.identity, this.transform);

                }
                else
                {
                    Instantiate(MeleSkill, new Vector3(transform.position.x - 0.5F, transform.position.y, transform.position.z), new Quaternion(0.0f, 180.0f, 0.0f, 1.0f), this.transform);
                }
            }
            else
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
        }
    }

    void EmojiInvoque(GameObject Emoji)
    {
        Instantiate(Emoji, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, this.transform);
        emojiOn = true;
    }

    void OnSkillSet()
    {
        playerState = PlayerState.Normal;
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
                playerState = PlayerState.Normal;
                StartCoroutine("ImmunePlayer");
            }
            if (coll.transform.tag == "Ice")
            {
                playerState = PlayerState.OnIce;
            }
            if (coll.transform.tag == "Mele")
            {
                TraumaInducer.Shake();
                rotationEnemy = Enemy.transform.rotation;
                playerState = PlayerState.OnImpulse;
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
                playerState = PlayerState.Normal;
                StartCoroutine("ImmunePlayer");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (playerState != PlayerState.OnSkill)
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
    }

    void ImpulseDamage(Vector2 direction)
    {
        if (!circleCollider.isTrigger)
        {
            circleCollider.isTrigger = true;
        }
        rig.AddForce(direction * 2, ForceMode2D.Impulse);
        time += 1 * Time.deltaTime;
        if (playerState != PlayerState.OnImpulse)
        {
            circleCollider.isTrigger = false;
        }

        if (time >= 1.0f)
        {
            circleCollider.isTrigger = false;
            playerState = PlayerState.Normal;
            playerState = PlayerState.OnSkill;
            time = 0;
        }
    }

    void ImpulseDash(Vector2 direction)
    {
        rig.velocity = direction * 30;
        time += 1 * Time.deltaTime;
        if (time >= 0.5f)
        {
            playerState = PlayerState.Normal;
            time = 0;
        }
    }

    public void PauseInFalse(float v)
    {
        pause = false;
    }

    public void SetPause(float v, bool state)
    {
        pause = state;
    }

    public void OnDestroy()
    {
        GameManager.OnPause -= PauseInFalse;
    }
}
