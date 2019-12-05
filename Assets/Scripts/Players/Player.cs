using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public Rigidbody2D rig;
    #region Enums
    public enum Skill {
        none,
        ice,
        mele
    }
    enum PlayerState {
        OnIce,
        OnSkill,
        OnImpulse,
        OnDash,
        Normal
    }
    #endregion
    #region Control
    public Control control;
    #endregion
    #region Floats
    const float LimitTimeWalfSound = 0.2f;
    const float LookToTheLeft = -1f;
    const float ForceDash = 0.7f;
    const float RotationLeft = 180f;
    const float RotationRight = 0f;
    const float Movement = 2;
    const float MaxVelocity = 4;
    const float MinVelocity = -4;
    const float ForceImpulseDash = 30f;
    const float ImpulseDirectionY = 0.6f;
    [SerializeField]
    float speed;
    [SerializeField]
    float jumpForce;
    float stamina;
    float timeSkill;
    float timeWalkSound;
    #endregion
    #region Vector2
    Vector2 velocity;
    Vector2 pos;
    #endregion
    #region Bools
    bool InFloor;
    bool defeat;
    bool cantMove;
    bool effect;
    bool immune = false;
    bool walkSound = true;
    #endregion
    #region INT
    const int MaxRandomPoint = 3;
    const int DashCost = 25;
    const int RegenStamina = 10;
    const int MaxStamina = 100;
    const int MaxLife = 3;
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
    Transform collision;
    #endregion
    #region Sprites
    [SerializeField]
    Sprite PowerUpIce;
    [SerializeField]
    Sprite PowerUpPush;
    [SerializeField]
    Sprite nothing;
    #endregion
    public delegate void Defeat (float enrageVal, string player);
    public static event Defeat WhoDefeat;

    void Start () {
        Life = MaxLife;
        stamina = MaxStamina;
        pos = new Vector2 (transform.position.x, transform.position.y);
        defeat = false;
        ActiveSkill = Skill.none;
        playerState = PlayerState.Normal;
        #region GetComponent
        m_Animator = GetComponent<Animator> ();
        sprite = GetComponent<SpriteRenderer> ();
        circleCollider = GetComponent<CircleCollider2D> ();
        #endregion
        #region SetEvents
        GameManager.OnPause += PauseInFalse;
        PauseManager.OnPause += SetPause;
        #endregion
    }

    void Update () {
        if (!cantMove) {
            if (Life > 0) {
                switch (playerState) {
                    case PlayerState.OnIce:
                        StartCoroutine (IceEffect ());
                        break;
                    case PlayerState.OnImpulse:
                        ImpulseState ();
                        break;
                    case PlayerState.OnDash:
                        DashState ();
                        break;
                    case PlayerState.Normal:
                        NormalState ();
                        break;
                }
            } else {
                if (!defeat) {
                    MultipleTargetCamera.SetTargets (transform, 1);
                    AkSoundEngine.PostEvent ("pl_lost", gameObject);
                    //GameManager.Get().SetPlayers(prefab);
                    DefeatPlayer (transform.name == "player" ? "player" : "player2");
                    defeat = true;
                }
            }
        }
    }
    #region Corrutines
    IEnumerator Impulse () {
        Vector2 force = new Vector2 (0, 2);
        rig.AddForce (force * jumpForce, ForceMode2D.Impulse);
        InFloor = false;
        yield return new WaitForSeconds (0.2f);
    }

    IEnumerator ImmunePlayer () {
        immune = true;
        playerState = PlayerState.Normal;
        rig.velocity = Vector3.zero;
        sprite.color = new Vector4 (255, 255, 255, 0);
        playerState = PlayerState.OnSkill;
        circleCollider.isTrigger = false;
        m_Animator.SetBool ("Damage", false);
        if (Life > 0) {
            Instantiate (Flecha, new Vector3 (transform.position.x, transform.position.y + 2f, transform.position.z), Quaternion.identity);
            yield return new WaitForSeconds (2.0f);
            AkSoundEngine.PostEvent ("pl_respawn", gameObject);
            playerState = PlayerState.Normal;
            transform.position = ActualRespawnPoint;
            Instantiate (Respawn, new Vector3 (transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            sprite.color = new Vector4 (255, 255, 0, 255);
            yield return new WaitForSeconds (2.0f);
            sprite.color = new Vector4 (255, 255, 255, 255);
            immune = false;
        }
    }

    IEnumerator IceEffect () {
        if (!effect) {
            m_Animator.SetBool ("Damage", true);
            effect = true;
            Instantiate (IceEffectState, new Vector3 (transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, this.transform);
        }
        yield return new WaitForSeconds (2.0f);
        m_Animator.SetBool ("Damage", false);
        effect = false;
        playerState = PlayerState.Normal;
    }

    #endregion

    #region Functions
    void IceOn () {
        InstantiateSkill (IceSkill, false);
    }

    void MeleOn () {
        InstantiateSkill (MeleSkill, true);
    }

    void InstantiateSkill (GameObject Skill, bool Children) {
        if (playerState != PlayerState.OnIce) {
            float y = transform.rotation.y;
            float posX = transform.position.x;
            Vector3 position = new Vector3 ((y == 0 ? posX + 1 : posX - 1), transform.position.y, transform.position.z);
            Quaternion direction = y == 0 ? Quaternion.identity : Quaternion.Euler (0, 180, 0);
            Instantiate (Skill, position, direction, Children ? transform : null);
        }
    }

    void OnSkillSet () {
        playerState = PlayerState.Normal;
    }

    void IfDamage () {
        Life--;
        AkSoundEngine.PostEvent ("pl_die", gameObject);
        Instantiate (Damage, new Vector3 (transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        if (Life > 0) {
            int randomPoint = Random.Range (0, MaxRandomPoint);
            ActualRespawnPoint = new Vector3 (RespawnPoint[randomPoint].position.x, RespawnPoint[randomPoint].position.y, 0.0f);
            transform.position = ActualRespawnPoint;
        }

        TraumaInducer.Shake ();

        lifeImage[Life].SetActive (false);
    }

    void Walk (float velocity, float rotation) {
        timeWalkSound += Time.deltaTime;
        if (timeWalkSound >= LimitTimeWalfSound) {
            timeWalkSound = 0;
            if (InFloor)
                AkSoundEngine.PostEvent ("pl_walk", gameObject);
        }
        m_Animator.SetBool ("Caminata", true);
        if (transform.rotation.y != rotation) {
            transform.rotation = Quaternion.Euler (0, rotation, 0);
        }
        Vector2 direction = new Vector2 (velocity, 0);
        rig.AddForce (direction * speed, ForceMode2D.Force);
    }

    void OnTriggerEnter2D (Collider2D coll) {
        collision = coll.transform;
        if (collision.CompareTag ("Floor") || collision.CompareTag ("Player")) {
            InFloor = true;
            m_Animator.SetBool ("Jump", false);
        } else {
            InFloor = false;
        }
        if (!immune) {
            if (collision.CompareTag ("EnemyObject") || collision.CompareTag ("Lava")) {
                IfDamage ();
                playerState = PlayerState.Normal;
                StartCoroutine (ImmunePlayer ());
            }
            if (collision.CompareTag ("Ice")) {
                AkSoundEngine.PostEvent ("pl_frozen", gameObject);
                playerState = PlayerState.OnIce;
            }
            if (collision.CompareTag ("Mele")) {
                AkSoundEngine.PostEvent ("pl_deathbybat", gameObject);
                TraumaInducer.Shake ();
                rotationEnemy = Enemy.transform.rotation;
                playerState = PlayerState.OnImpulse;
            }
        }
    }

    void OnTriggerStay2D (Collider2D coll) {
        collision = coll.transform;
        if (!immune) {
            if ((collision.CompareTag ("EnemyObject") || collision.CompareTag ("Lava"))) {
                IfDamage ();
                playerState = PlayerState.Normal;
                StartCoroutine (ImmunePlayer ());
            }
        }
    }

    void OnCollisionEnter2D (Collision2D coll) {
        collision = coll.transform;
        if (playerState != PlayerState.OnSkill) {
            if (collision.CompareTag ("IcePowerUp")) {
                AkSoundEngine.PostEvent ("pl_powerup", gameObject);
                PowerUpUI.sprite = PowerUpIce;
                ActiveSkill = Skill.ice;
            }
            if (collision.CompareTag ("EmpujePowerUp")) {
                AkSoundEngine.PostEvent ("pl_bat", gameObject);
                PowerUpUI.sprite = PowerUpPush;
                ActiveSkill = Skill.mele;
            }
        }
    }

    void ImpulseDamage (Vector2 direction) {
        if (!circleCollider.isTrigger) {
            circleCollider.isTrigger = true;
        }
        rig.AddForce (direction * 2, ForceMode2D.Impulse);
        timeSkill += 1 * Time.deltaTime;
        if (playerState != PlayerState.OnImpulse) {
            circleCollider.isTrigger = false;
        }

        if (timeSkill >= 1.0f) {
            circleCollider.isTrigger = false;
            playerState = PlayerState.Normal;
            playerState = PlayerState.OnSkill;
            timeSkill = 0;
        }
    }

    void ImpulseDash (Vector2 direction) {
        rig.velocity = direction * ForceImpulseDash;
        timeSkill += 1 * Time.deltaTime;
        if (timeSkill >= 0.5f) {
            playerState = PlayerState.Normal;
            timeSkill = 0;
        }
    }

    public void PauseInFalse (float v) {
        cantMove = false;
    }

    public void SetPause (float v, bool state) {
        cantMove = state;
    }

    public void OnDestroy () {
        GameManager.OnPause -= PauseInFalse;
        PauseManager.OnPause -= SetPause;
    }
    #endregion

    #region SwitchStatesFunctios
    void NormalState () {
        pos = new Vector2 (transform.position.x, transform.position.y);
        if (control.Jump ()) {
            if (InFloor) {
                m_Animator.SetBool ("Jump", true);
                AkSoundEngine.PostEvent ("pl_jump", gameObject);
                StartCoroutine (Impulse ());
            }
        }
        if (control.Right ()) {
            Walk (Movement, RotationRight);
        } else if (control.Left ()) {
            Walk (-Movement, RotationLeft);
        } else {
            timeWalkSound = LimitTimeWalfSound;
            m_Animator.SetBool ("Caminata", false);
        }
        velocity = rig.velocity;
        if (rig.velocity.x >= MaxVelocity) {
            rig.velocity = new Vector2 (MaxVelocity, rig.velocity.y);
        }
        if (rig.velocity.x <= MinVelocity) {
            rig.velocity = new Vector2 (MinVelocity, rig.velocity.y);
        }
        if (control.Skill ()) {
            switch (ActiveSkill) {
                case Skill.ice:
                    m_Animator.SetTrigger ("IceSkill");
                    playerState = PlayerState.OnSkill;
                    ActiveSkill = Skill.none;
                    PowerUpUI.sprite = nothing;
                    break;
                case Skill.mele:
                    m_Animator.SetTrigger ("MeleSkill");
                    ActiveSkill = Skill.none;
                    PowerUpUI.sprite = nothing;
                    break;
                default:

                    break;
            }
        }
        if (stamina > 0 && stamina >= DashCost) {
            if (control.Dash ()) {
                AkSoundEngine.PostEvent ("pl_dash", gameObject);
                myRotation = new Quaternion (transform.rotation.x, transform.rotation.y, transform.rotation.z, 1.0f);
                m_Animator.SetTrigger ("Dash");
                stamina -= DashCost;
                playerState = PlayerState.OnDash;
            }
        }
        if (stamina > MaxStamina) {
            stamina = MaxStamina;
        }
        if (stamina < MaxStamina) {
            stamina += RegenStamina * Time.deltaTime;
        }
        StaminaObj.size = stamina / MaxStamina;
    }

    void DashState () {
        myRotation = new Quaternion (transform.rotation.x, transform.rotation.y, transform.rotation.z, 1.0f);
        Vector2 direction = myRotation.y == 0 ? Vector2.right : Vector2.left;
        ImpulseDash (direction * ForceDash);
    }

    void ImpulseState () {
        float direction = rotationEnemy.y == LookToTheLeft ? -1 : 1;
        ImpulseDamage (new Vector2 (direction, ImpulseDirectionY));
        m_Animator.SetBool ("Damage", true);
    }
    void DefeatPlayer (string player) {
        if (WhoDefeat != null) {
            float amount = 1;
            WhoDefeat (amount, player);
        }
    }
    #endregion

}