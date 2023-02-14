using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public HPManager hPManager;
    public GameManager gameManager;
    Rigidbody2D rigid;
    public SpriteRenderer spriteRenderer;
    Animator anim;
    public float jumpPower;
    BoxCollider2D boxcollider;
    AudioSource audioSource;
    public float maxSpeedx;
    public string Class;
    float time = 0;
    public float DashCoolTime = 2;
    public bool CanClimb;
    float maxSpeedy;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        boxcollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        maxSpeedy = maxSpeedx * 5;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float h = spriteRenderer.flipX == false ? 1 : -1;
        //점프에 관한 코드
        if (Input.GetButtonDown("Jump") && !(anim.GetBool("IsJumping")))
        {
            if (anim.GetBool("IsClimb"))
            {
                rigid.AddForce(Vector2.right * h * maxSpeedx * 0.5f + Vector2.up * 0.1f * jumpPower
                , ForceMode2D.Impulse);
            }
            else
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("IsJumping", true);
            // PlaySound("JUMP");
        }
        //키를 떼면 잘 멈추게 하는 코드
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            if (!anim.GetBool("IsJumping") && rigid.velocity.y > 0)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y*0.1f);
            }
            // rigid.velocity = new Vector2(
            //     0.1f * rigid.velocity.normalized.x
            //  , rigid.velocity.y);
        }
        //쳐다보는 방향에 관한 코드
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = (Input.GetAxisRaw("Horizontal") == -1);
        }
        //애니메이션 제어를 위한 코드
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
            anim.SetBool("IsWalking", false);
        else
            anim.SetBool("IsWalking", true);
        //기본공격
        if (Input.GetKeyDown(KeyCode.LeftControl) && !anim.GetBool("IsClimb"))
        {
            if (anim.GetBool("IsAttacking") == false)
            {

                anim.SetBool("IsAttacking", true);
                GameObject BasicAttack = transform.Find(Class).gameObject.transform
                .Find("BasicAttack").gameObject;
                BasicAttack.GetComponent<CircleCollider2D>().enabled = true;
                //자신의 직업 자식의 기본공격을 ON시킴.
                if (spriteRenderer.flipX == true)
                {
                    BasicAttack.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else
                {
                    BasicAttack.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }
        // if (transform.Find(Class).gameObject.transform
        //         .Find("BasicAttack").gameObject.GetComponent<CircleCollider2D>().enabled == false)
        // {
        //     anim.SetBool("IsAttacking", false);
        // }
        //대쉬
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (DashCoolTime > 0) return;
            maxSpeedx = 12;
            DashCoolTime = 2;
            anim.SetBool("IsDashing", true);
            rigid.AddForce(Vector2.right * h * maxSpeedx * 5, ForceMode2D.Impulse);
            gameObject.layer = LayerMask.NameToLayer("Imortal");
            CancelInvoke();
            Invoke("BeBack", 0.35f);
        }
        //밧줄 타는 코드
        float v = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (CanClimb)
            {
                anim.SetBool("IsJumping", false);
                rigid.velocity = Vector2.zero;
                anim.SetBool("IsClimb", true);
                transform.position = new Vector2(transform.position.x,
                transform.position.y + 0.02f);
            }
            if (gameManager.Next)
            {
                gameManager.NextStage();
            }
        }
        //밧줄에서 내려가는 코드
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (CanClimb)
            {
                anim.SetBool("IsJumping", false);
                rigid.velocity = Vector2.zero;
                anim.SetBool("IsClimb", true);
                transform.position = new Vector2(transform.position.x,
                transform.position.y - 0.02f);
            }
        }
        if (anim.GetBool("IsClimb"))
        {
            rigid.gravityScale = 0;
        }
        else
        {
            rigid.gravityScale = 1;
        }
        //쿨타임 줄이기 - 대쉬
        DashCoolTime -= Time.deltaTime;


    }
    void BeBack()
    {
        anim.SetBool("IsDashing", false);
        gameObject.layer = LayerMask.NameToLayer("Default");
    }
    void AttckingTurn()
    {
        anim.SetBool("IsAttacking", false);
    }
    void FixedUpdate()
    {
        if (maxSpeedx > 4)
            maxSpeedx -= 0.5f;
        //move speed
        float h = Input.GetAxisRaw("Horizontal");

        if (anim.GetBool("IsClimb"))
        {

        }
        else
            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        //maxspeed
        if (rigid.velocity.x > maxSpeedx)
            rigid.velocity = new Vector2(maxSpeedx, rigid.velocity.y);
        else if (rigid.velocity.x < -maxSpeedx)
            rigid.velocity = new Vector2(-maxSpeedx, rigid.velocity.y);

        if (rigid.velocity.y > maxSpeedy)
            rigid.velocity = new Vector2(rigid.velocity.x, maxSpeedy);
        else if (rigid.velocity.y < -maxSpeedy)
            rigid.velocity = new Vector2(rigid.velocity.x, -maxSpeedy);

        //landing platform
        if (rigid.velocity.y <= 0) //내려가고 있을때만
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast
            (rigid.position, Vector3.down, 1, LayerMask.GetMask("Map"));
            if (rayHit.collider != null)
            {
                Debug.Log(rayHit.collider.name);
                if (rayHit.distance < 1.0f)
                    anim.SetBool("IsJumping", false);
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Rope")
        {
            CanClimb = false;
            anim.SetBool("IsClimb", false);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Map"))
        {
            boxcollider.isTrigger = false;
            GetComponent<CapsuleCollider2D>().isTrigger = false;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        GameObject Other = other.gameObject;
        string Tag = other.gameObject.tag;
        int Layer = other.gameObject.layer;
        if (Layer == LayerMask.NameToLayer("Map"))
        {
            anim.SetBool("IsJumping", true);
        }
        if (Tag == "Monster")
        {
            if (gameObject.layer != LayerMask.NameToLayer("Imortal"))
            {
                hPManager.OnDamaged(FindMonsterAd(Other.name));
                //맞으면 플레이어가 밀려나기
                int dirc = transform.position.x - other.transform.position.x > 0 ? 1 : -1;
                // rigid.AddForce(new Vector2(dirc, 0.2f) * 5, ForceMode2D.Impulse);
                rigid.velocity = new Vector2(dirc, 0.6f) * 5;
                anim.SetBool("IsJumping", true);
                CancelInvoke();
                gameObject.layer = LayerMask.NameToLayer("Imortal");
                Invoke("BeBack", 0.8f);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject Other = other.gameObject;
        string Tag = other.gameObject.tag;
        int Layer = other.gameObject.layer;
        if (Tag == "Rope")
        {
            CanClimb = true;
        }
        if (Layer == LayerMask.NameToLayer("Map"))
        {
            boxcollider.isTrigger = true;
            GetComponent<CapsuleCollider2D>().isTrigger = true;
        }

    }
    void OnCollisionStay2D(Collision2D other)
    {
        GameObject Other = other.gameObject;
        string Tag = other.gameObject.tag;
        int Layer = other.gameObject.layer;
        if (Tag == "Monster")
        {
            if (gameObject.layer != LayerMask.NameToLayer("Imortal"))
            {
                hPManager.OnDamaged(FindMonsterAd(Other.name));
                //맞으면 플레이어가 밀려나기
                int dirc = transform.position.x - other.transform.position.x > 0 ? 1 : -1;
                rigid.velocity = new Vector2(dirc, 0.6f) * 5; //, ForceMode2D.Impulse);
                anim.SetBool("IsJumping", true);
                CancelInvoke();
                gameObject.layer = LayerMask.NameToLayer("Imortal");
                Invoke("BeBack", 0.8f);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        GameObject Other = other.gameObject;
        string Tag = other.gameObject.tag;
        int Layer = other.gameObject.layer;

    }
    float FindMonsterAd(string name)
    {
        if (name == "Bat")
        {
            return 3;
        }
        return 1;
    }
}
