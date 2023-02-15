using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemController : MonoBehaviour
{
    Animator anim;
    public float Hp;
    public float Ad;
    private float Speed;
    public float Idle_speed;
    public float Chase_speed;

    public float Attack_range;
    public float Close_range;

    private bool isPlayer_close = false;
    private bool isAttack = false;

    private float playerDistance;
    private GameObject Player;
    public StatManager statManager;
    Rigidbody2D rigid;
    public float nextMove;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capuslecollider;
    public bool Hit;
    public int CurType;
    int StrongType;
    int WeakType;
    //물1 > 불2 > 나무3 > 흙4 > 번개5 > 물 무속성은 6
    bool IsAwake = false;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capuslecollider = GetComponent<CapsuleCollider2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer.flipX = (nextMove == -1);
        Think();

        StrongType = (CurType + 1) % 5;
        WeakType = (CurType - 1) % 5;
        //Invoke("Think", 5);
    }

    void FixedUpdate()
    {
        playerDistance = Vector3.Distance(transform.position, Player.transform.position);
        CheckPlayerClose();
        Attack();
        if (IsAwake)
        {
            Move();
        }
        if (Hit)
        {
            rigid.velocity = Vector2.zero;
        }
    }

    private void CheckPlayerClose()
    {
        if (playerDistance <= Attack_range)
        {
            // CancelInvoke();
            isPlayer_close = true;
            isAttack = true;
        }
        else if (playerDistance <= Close_range)
        {
            // CancelInvoke();
            Speed = Chase_speed;
            isPlayer_close = true;
            isAttack = false;
            if (!IsAwake)
            {
                anim.SetBool("IsPlayerClose", true);
                Invoke("g_awake", 1);
            }
        }
        else
        {
            if (isPlayer_close)
            {
                Invoke("Think", 1);
            }
            Speed = Idle_speed;
            isPlayer_close = false;
            isAttack = false;
        }
    }

    void g_awake()
    {
        IsAwake = true;
    }

    private void Attack()
    {
        anim.SetBool("PlayerClosetoAttack", isAttack);
        if (isAttack)
        {
            GetComponent<PolygonCollider2D>().enabled = true;
        }
        else
        {
            GetComponent<PolygonCollider2D>().enabled = false;
        }
    }

    private void Move()
    {
        if (!isPlayer_close)
        {
            anim.SetBool("IsRun", false);
            rigid.velocity = new Vector2(Speed * nextMove, rigid.velocity.y);
            MapCheck();
        }
        else if (!isAttack)
        {
            anim.SetBool("IsRun", true);
            rigid.velocity = (Player.transform.position - transform.position).normalized * Speed;
            nextMove = rigid.velocity.x / Mathf.Abs(rigid.velocity.x);
            spriteRenderer.flipX = (rigid.velocity.x < 0);
            MapCheck();
        }
        else
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            // CancelInvoke();
        }
    }

    private void MapCheck()
    {
        Debug.DrawRay(rigid.position + Vector2.right * (nextMove) * 0.5f + Vector2.down, Vector3.down * 2, new Color(0, 1, 0));
        RaycastHit2D rayHit1 = Physics2D.Raycast(rigid.position + Vector2.right * (nextMove) * 0.5f + Vector2.down * 0.5f, Vector3.down, 3, LayerMask.GetMask("Map"));
        Debug.DrawRay(rigid.position + Vector2.right * (nextMove) * 2.7f + Vector2.down, Vector3.down * 2, new Color(0, 1, 0));
        RaycastHit2D rayHit2 = Physics2D.Raycast(rigid.position + Vector2.right * (nextMove) * 2.7f + Vector2.down * 0.5f, Vector3.down, 3, LayerMask.GetMask("Map"));

        if (rayHit1.collider == null)
        {
            if (isPlayer_close && !isAttack)
            {
                if (rayHit2.collider != null)
                {
                    return;
                }
            }
            CancelInvoke();
            Invoke("Think", 1);
            nextMove = -nextMove;

            rigid.velocity = new Vector2(nextMove * Speed, rigid.velocity.y);
            spriteRenderer.flipX = (nextMove == -1);
            Debug.Log("Warning");
        }
    }

    void Think()
    {
        Debug.Log("Thinking...");
        if (IsAwake)
            nextMove = Random.Range(-1, 2);
        else
            nextMove = 0;

        //쳐다보는 방향에 대한 코드
        if (!isPlayer_close)
        {
            spriteRenderer.flipX = (nextMove == -1);
        }

        float nextThinkTime = Random.Range(0.5f, 2f);
        Invoke("Think", nextThinkTime);
    }

    public void OnDamaged(float damage)
    {
        Debug.Log("OnDamaged");
        Hp -= damage;
        if (Hp <= 0)
        {
            anim.SetBool("IsDied", true);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<PolygonCollider2D>().enabled = false;
            Destroy(gameObject, 1);
        }
        nextMove = spriteRenderer.flipX == true ? -1 : 1;
        // rigid.AddForce(Vector2.left* nextMove*3+ Vector2.up * 3, ForceMode2D.Impulse);
        // spriteRenderer.color = new Color(1, 1, 1, 0.4f);


        // capuslecollider.enabled = false;

        // Invoke("DeActive", 2.0f);
    }
    void HitFalse()
    {
        Hit = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            if (!Hit)
            {
                Invoke("HitFalse", 1f);
                Debug.Log("HitFalse");
            }
            Hit = true;
            OnDamaged(PlayerDamage(other.gameObject.tag) / 2); //콜라이더가 박스랑 캡슐 두개라서 나누기2
        }
    }
    float PlayerDamage(string tag)
    {
        float Damage = float.Parse(tag) * statManager.Ad;

        if (statManager.Type == WeakType) // 약점타입
            return Damage * 2;
        else if (statManager.Type == StrongType) // 강점타입
            return Damage / 2;
        return Damage; // 일반타입

    }
}
