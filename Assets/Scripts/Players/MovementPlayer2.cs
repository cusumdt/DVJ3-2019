using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MovementPlayer2 : MonoBehaviour
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
    public Transform player;
    public GameObject IceSkill;
    public GameObject IceEffectState;
    private bool effect;
    private bool iceInvoque = false;
    private bool meleInvoque = false;
    public GameObject[] lifeImage;
    private bool defeat;
    public GameObject prefab;
    public bool OnImpulse;
    float time;
    public GameObject MeleSkill;
    public bool OnSkill;
    CircleCollider2D boxCollider;

    public enum Skill
    {
        none,
        ice,
        mele
    }
    Quaternion myRotation;
    static Quaternion rotation;
    public Skill ActiveSkill;
    private Quaternion rotationEnemy;
    static private bool pause;
    private bool OnDash;
    public GameObject Damage;
    public GameObject Respawn;
    public GameObject Flecha;

    // Start is called before the first frame update
    void Start()
    {
        Life = 3;
        stamina = MaxStamina;
        pos = new Vector2(transform.position.x, transform.position.y);
        m_Animator = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        defeat = false;
        effect = false;
        time = 0;
        boxCollider = GetComponent<CircleCollider2D>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if(!pause)
        {
        if (Life > 0)
        {
            rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, 1.0f);
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
                    ImpulseDamage(new Vector2(1.0f, 0.6f));
                }
                else
                {
                    ImpulseDamage(new Vector2(-1.0f, 0.6f));
                }
                
            }
           
            else if(OnDash)
            {
                 if (myRotation.y == 0.0f)
                {
                    ImpulseDash(Vector2.left* 0.7f);
                }
                else
                {
                    ImpulseDash(Vector2.right * 0.7f);
                }
            }
            else
            {
            pos = new Vector2(transform.position.x, transform.position.y);
            if (Input.GetKeyDown("[0]") || Input.GetKeyDown("joystick 2 button 0"))
            {
                if (jump == true && InFloor)
                {
                    StartCoroutine("Impulse");
                }
            }

            if (Input.GetKey("right") || Input.GetAxis("HorizontalJoystick2") >= 1)
            {
                m_Animator.SetBool("Caminata", true);
                if(transform.rotation.y != 180)
                {
                    transform.rotation = new Quaternion(0.0f,180.0f,0.0f,1.0f );
            
                }    
                rig.AddForce(new Vector2(2, 0) * speed, ForceMode2D.Force);

            }
            else if (Input.GetKey("left") || Input.GetAxis("HorizontalJoystick2") <= -1)
            {
                m_Animator.SetBool("Caminata", true);
                if(transform.rotation.y != 0)
                {
                    transform.rotation= new Quaternion(0.0f,0.0f,0.0f,1.0f );
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
             if (Input.GetKey("5") || Input.GetKey("joystick 2 button 2") )
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

                        if (Input.GetKeyDown("[.]") || Input.GetKeyDown("joystick 2 button 1"))
                        {
                            StartCoroutine("Dash");
                            myRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, 1.0f);
                            m_Animator.SetTrigger("Dash");
                            stamina -= DashCost;
                            OnDash = true;
                        }
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
        else
        {
            if(!defeat)
            {
                MultipleTargetCamera.SetTargets(transform, 1);
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
        sprite.color = new Vector4(255, 255, 255, 0);
        OnSkill = true;
        OnImpulse = false;
        rig.velocity = Vector3.zero;
        boxCollider.isTrigger = false;
        if (Life > 0)
        {
            Instantiate(Flecha, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Quaternion.identity);
            yield return new WaitForSeconds(2.0f);
            OnSkill = false;
            Instantiate(Respawn, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            sprite.color = new Vector4(255, 255, 0, 255);
            yield return new WaitForSeconds(2.0f);
            sprite.color = new Vector4(255, 255, 255, 255);
            immune = false;
        }
    }
     IEnumerator IceEffect()
    {     
        if(!effect)
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
    void IceOn()
    {
        
        if (transform.rotation.y == 0)
        {
            Instantiate(IceSkill, new Vector3(transform.position.x - 1.0f, transform.position.y, transform.position.z), new Quaternion(0.0f, 180.0f, 0.0f, 1.0f), this.transform);
        }
        else
        {
            Instantiate(IceSkill, new Vector3(transform.position.x + 1.0f, transform.position.y, transform.position.z), Quaternion.identity, this.transform);
        }

    }   

    void MeleOn()
    {
        Debug.Log("dalepibe");
        if (transform.rotation.y == 0)
        {
            Instantiate(MeleSkill, new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), new Quaternion(0.0f, 180.0f, 0.0f, 1.0f), this.transform);
        }
        else
        {
            Instantiate(MeleSkill, new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), Quaternion.identity, this.transform);
        }
    }
        void OnSkillSet()
    {
        OnSkill = false ;
    }
    IEnumerator MeleSkillCD()
    {
        meleInvoque = true;
        yield return new WaitForSeconds(0.0f);
        meleInvoque = false;
    }

    

    IEnumerator Dash()
    {
        dash = true;
        yield return new WaitForSeconds(0.2f);
        dash = false;
    }
    void IfDamage()
    {
        Life--;

        Instantiate(Damage, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        if (Life > 0)
        {
            int randomPoint = Random.Range(0, 3);
            transform.position = new Vector3(RespawnPoint[randomPoint].position.x, RespawnPoint[randomPoint].position.y, 0.0f);
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
                StartCoroutine("ImmunePlayer");
            }
             if (coll.transform.tag == "Ice")
            {
                OnIce = true;
            }
            if (coll.transform.tag == "Mele")
            {
                rotationEnemy = Movement.GetQuaternion();
                TraumaInducer.Shake();
                OnImpulse = true;
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
    static public Quaternion GetQuaternion()
    {
        return rotation;
    }
}
