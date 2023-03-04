using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boss3 : MonoBehaviour
{
    Animator anim;
    public float Hp;
    public float maxHp;
    public float Ad;
    private float Speed;
    public float Idle_speed;
    public float Chase_speed;

    public float Attack_range;
    public float Close_range;

    private bool isPlayer_close = false;
    private bool isAttack = false;

    [SerializeField]
    public Slider hpBar;

    private float playerDistance;
    private GameObject Player;
    StatManager statManager;
    AudioManager audioManager;

    Rigidbody2D rigid;
    public float nextMove;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capuslecollider;
    public bool Hit;
    public int CurType;
    int StrongType;
    int WeakType;
    public int CanFly;
    public GameObject HPportion;
    public bool Die = false;
    //물1 > 불2 > 나무3 > 흙4 > 번개5 > 물 무속성은 6물
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capuslecollider = GetComponent<CapsuleCollider2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        statManager = GameObject.Find("StatManager").GetComponent<StatManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        spriteRenderer.flipX = (nextMove == -1);
        Think();
        StrongType = (CurType + 1) % 5;
        WeakType = (CurType - 1) % 5;
        Hp = Hp * DiffControl.Diff;
        maxHp = Hp;
        Speed = 0;
    }

    void FixedUpdate()
    {
        if (Die)
        {

        }
        else
        {
            playerDistance = Vector3.Distance(transform.position, Player.transform.position);
            if (Hit)
            {
                anim.SetTrigger("Hurt");
                rigid.velocity = Vector2.zero;
            }
            CheckPlayerClose();
            Attack();
            Move();
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
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            if (isPlayer_close)
            {
                Invoke("Think", 1);
            }
            Speed = Idle_speed;
            isPlayer_close = false;
            GetComponent<BoxCollider2D>().enabled = true;
            isAttack = false;
        }
    }

    private void Attack()
    {
        anim.SetBool("IsAttack", isAttack);
        if (isAttack)
        {
            Invoke("Rest", 0.8f);
            GetComponent<PolygonCollider2D>().enabled = true;
            Invoke("RestBack", 5f);
        }
        else
        {
            GetComponent<PolygonCollider2D>().enabled = false;
        }
    }

    void Rest(){
        Attack_range = 0;
        Close_range = 0;
    }

    void RestBack(){
        Attack_range = 5;
        Close_range = 6;
    }

    private void Move()
    {
        if (!isAttack && nextMove != 0)
            anim.SetBool("IsWalk", true);
        else
            anim.SetBool("IsWalk", false);

        if (!isPlayer_close)
        {
            rigid.velocity = new Vector2(Speed * nextMove, rigid.velocity.y);
            if (nextMove != 0)
            MapCheck();
        }
        else if (!isAttack)
        {
            rigid.velocity = new Vector2((Player.transform.position - transform.position).normalized.x * Speed,
            (Player.transform.position - transform.position).normalized.y * Speed * CanFly +  rigid.velocity.y);
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
            // CancelInvoke();
            Invoke("Think", 1);
            nextMove = -nextMove;

            rigid.velocity = new Vector2(nextMove * Speed, rigid.velocity.y);
            spriteRenderer.flipX = (nextMove == -1);
            
        }
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);

        //쳐다보는 방향에 대한 코드
        if (!isPlayer_close)
        {
            spriteRenderer.flipX = (nextMove == -1);
        }

        float nextThinkTime = Random.Range(3f, 5f);
        Invoke("Think", nextThinkTime);
    }

    public void OnDamaged(float damage)
    {
        anim.SetTrigger("Hurt");
        Hp -= damage;
        hpBar.value = Hp / maxHp;
        
        if (Hp <= 0 && !Die)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<PolygonCollider2D>().enabled = false;
            anim.SetBool("IsDied", true);
            Destroy(gameObject, 1);
            Die = true;

            string name = gameObject.name;
            if (name == "Bat")
                audioManager.monsterSound("Bat");
            else if (name == "Bear")
                audioManager.monsterSound("Bear");
            else if (name == "Bone")
                audioManager.monsterSound("Bone");
            else if (name == "Rat")
                audioManager.monsterSound("Rat");
            else if (name == "Slime")
                audioManager.monsterSound("Slime");

        }
        audioManager.monsterSound("Damaged");
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
       
        if (other.gameObject.name == "WarriorSkill2(Clone)")
        {
            if (other.gameObject.GetComponent<BasicAttack>().Monsternum > 1){
                return;
            }
        }
        if (other.gameObject.name == "WarriorSkill4(Clone)")
        {
            if (Hp > 0)
            {
                statManager.Ad += 5 / 2f;
                statManager.Stack += 1;
            }
        }
        if (other.gameObject.tag == "Arrow"){
            if (other.gameObject.GetComponent<ArrowMove>().IsHit){
                return;
            }
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            if (other.gameObject.tag == "Arrow"){
                other.gameObject.GetComponent<ArrowMove>().IsHit = true;
            }
        
            if (!Hit)
            {
                Invoke("HitFalse", 1f);
            }
            Hit = true;
            OnDamaged(PlayerDamage(other.gameObject.GetComponent<BasicAttack>().GetSkillDamage()) / 2); //콜라이더가 박스랑 캡슐 두개라서 나누기2
            statManager.IsFighting = 5;
        }
    }
    float PlayerDamage(float Dmg)
    {
        Debug.Log("데미지는 : " + Dmg);
        float Damage = Dmg * statManager.Ad;

        if (statManager.Type == WeakType) // 약점타입
            return Damage * 2;
        else if (statManager.Type == StrongType) // 강점타입
            return Damage / 2;
        return Damage; // 일반타입
    }
}