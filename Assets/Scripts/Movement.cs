using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Movement : MonoBehaviour
{
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
    private bool iceInvoque = false;
    public GameObject[] lifeImage;
    public GameObject prefab;
    private bool defeat;
    // Start is called before the first frame update
    void Start()
    {
        Life = 3;
        stamina = MaxStamina;
        pos = new Vector2(transform.position.x, transform.position.y);
        m_Animator = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        defeat = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Life > 0)
        {
            if (OnIce)
            {
                StartCoroutine("IceEffect");
            }
            else
            {
            pos = new Vector2(transform.position.x, transform.position.y);
            if (Input.GetKeyDown("space") || Input.GetKeyDown("joystick 1 button 0"))
            {
                if (jump == true && InFloor)
                {
                    StartCoroutine("Impulse");
                }
            }

            if (Input.GetKey("d") || Input.GetAxis("HorizontalJoystick1") >= 1)
            {
                m_Animator.SetBool("Caminata", true);
                if(transform.rotation.y != 0)
                {
                    transform.rotation = new Quaternion(0.0f,0.0f,0.0f,1.0f );
            
                }   
                rig.AddForce(new Vector2(2, 0) * speed, ForceMode2D.Force);

            }
            else if (Input.GetKey("a") || Input.GetAxis("HorizontalJoystick1") <= -1)
            {
                m_Animator.SetBool("Caminata", true);
                if(transform.rotation.y != 180)
                {
                    transform.rotation = new Quaternion(0.0f,180.0f,0.0f,1.0f );
            
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
            if (Input.GetKey("left shift") && Input.GetKey("a") || Input.GetKey("joystick 1 button 1") && Input.GetAxis("HorizontalJoystick1") <= -1)
            {
                if (dash == true)
                {
                    rig.AddForce(new Vector2(-20, 0) * speed, ForceMode2D.Force);

                }

            }
            if (Input.GetKey("left shift") && Input.GetKey("d") || Input.GetKey("joystick 1 button 1") && Input.GetAxis("HorizontalJoystick1") >= 1)
            {
                if (dash == true)
                {
                    rig.AddForce(new Vector2(20, 0) * speed, ForceMode2D.Force);

                }
            }
             if (Input.GetKey("f"))
            {
                
                if (iceInvoque == false)
                {
                       StartCoroutine("IceSkillCD");
                      if(transform.rotation.y == 0)
                      {
                           Instantiate(IceSkill, new Vector3(transform.position.x + 1.0F, transform.position.y, transform.position.z), Quaternion.identity);
                      }
                      else
                      {
                           Instantiate(IceSkill, new Vector3(transform.position.x -1.0F, transform.position.y, transform.position.z), new Quaternion(0.0f,180.0f,0.0f,1.0f));
                      }

                  
                }
              
            }
            if (stamina > 0 && stamina >= DashCost)
            {

                if (Input.GetKeyDown("left shift") || Input.GetKey("joystick 1 button 1"))
                {
                    StartCoroutine("Dash");
                    //rig.AddForce(new Vector2(100, 0) * speed, ForceMode2D.Force);
                    m_Animator.SetTrigger("Dash");
                    stamina -= DashCost;
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
            if(!defeat)
            {
                GameManager.instance.SetPlayers(prefab);
                defeat = true;
            }
            
           // SceneManager.LoadScene("GameOver");
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
        sprite.color = new Vector4(255, 255, 0, 255);
        yield return new WaitForSeconds(2.0f);
        sprite.color = new Vector4(255, 255, 255, 255);
        immune = false;
    }

    IEnumerator Dash()
    {
        dash = true;
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        dash = false;
    }
    IEnumerator IceEffect()
    {     
        sprite.color = new Vector4(0, 0, 255, 255);
          yield return new WaitForSeconds(2.0f);
          sprite.color = new Vector4(255, 255, 255, 255);
          OnIce = false;
    }
     IEnumerator IceSkillCD()
    {     
        iceInvoque = true;
          yield return new WaitForSeconds(5.0f);
        iceInvoque = false;
    }
    void IfDamage()
    {
        Life--;
        int randomPoint = Random.Range(0, 3);
        transform.position = new Vector3 (RespawnPoint[randomPoint].position.x, RespawnPoint[randomPoint].position.y,0.0f);
        lifeImage[Life].SetActive(false);
    }
      void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == "Floor")
        {
          InFloor = true;
        }
        else
        {
            InFloor = false;
        }
        if (!immune)
        { 
            if (coll.transform.tag == "EnemyObject")
            {
                IfDamage();
                StartCoroutine("ImmunePlayer");
            }
            if (coll.transform.tag == "Ice")
            {
                OnIce = true;
            }
        }

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        // If the Collider2D component is enabled on the collided object

    }

}
