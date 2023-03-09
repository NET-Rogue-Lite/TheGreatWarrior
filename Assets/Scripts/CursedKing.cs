using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CursedKing : MonoBehaviour
{
    Animator anim;
    SpriteRenderer spriteRenderer;
    private GameObject Player;
    Rigidbody2D rigid;
    public Slider hpBar;
    public StatManager statManager;
    public AudioManager audioManager;
    EquipManager equipManager;
    public EventDrop eventDrop;
    BoxCollider2D boxCollider2D;
    public float maxHp;
    public float Hp;
    public float skillattack;
    public int CurType;
    float skillattack_cool;
    float playerDistance;
    public bool is_skilling = false;
    bool is_Walking = false;
    bool isGround = false;
    public float warpcool;
    public GameObject GameClearUI;

    //물1 > 불2 > 나무3 > 흙4 > 번개5 > 물 무속성은 6물

    void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Player = GameObject.FindGameObjectWithTag("Player");
        equipManager = GameObject.Find("EquipManager").GetComponent<EquipManager>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        skillattack_cool = skillattack;
        rigid = GetComponent<Rigidbody2D>();
        Hp = Hp * DiffControl.Diff;
        maxHp = Hp;
        warpcool = 15;
        if (DiffControl.Diff == 4)
        {
            if (equipManager.CurRune == "Cursed Rune")
            {
                equipManager.EquipRune();
            }
            if (equipManager.CurRune == "CursedKing's Armor")
            {
                equipManager.EquipArmor();
            }
            if (equipManager.CurRune == "CursedKing's Helmat")
            {
                equipManager.EquipHat();
            }
            if (equipManager.CurRune == "CursedKing's Glove")
            {
                equipManager.EquipGlove();
            }
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {

        RaycastHit2D rayHit = Physics2D.Raycast
        (rigid.position, Vector3.down, 5, LayerMask.GetMask("Map"));
        if (!isGround)
        {
            if (rayHit.collider != null)
            {
                isGround = true;
                anim.SetBool("isGround", true);
            }
        }
        else
        {
            playerDistance = Player.transform.position.x - transform.position.x;
            transform.rotation = Quaternion.Euler(0, (playerDistance < 0) ? 180 : 0, 0);
            Attack();
            Move();
        }
    }

    void Move()
    {
        warpcool -= 0.02f;
        if (!is_skilling && warpcool <= 0)
        {
            anim.SetTrigger("Warp");
        }
        if (Mathf.Abs(playerDistance) > 4 && !is_skilling)
        {
            is_Walking = true;
            anim.SetBool("IsWalk", true);
            rigid.velocity = new Vector2((Player.transform.position - transform.position).normalized.x * 3, rigid.velocity.y);
        }
        else
        {
            anim.SetBool("IsWalk", false);
            is_Walking = false;
            rigid.velocity = new Vector2(0, rigid.velocity.y);

        }
    }

    void warp()
    {
        audioManager.finalBossSound("Warp");
        transform.position = new Vector2(Player.transform.position.x + Random.Range(-5, 5), transform.position.y);
        warpcool = Random.Range(7f, 11f);
    }

    void Attack()
    {
        skillattack_cool -= 0.02f;
        if (skillattack_cool < 0 && !is_skilling)
        {
            is_skilling = true;
            boxCollider2D.enabled = false;
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            SkillCast();
            Invoke("SkillCast", 1.0f);
            Invoke("skill_recool", 2.0f);
        }
    }
    void SkillCast()
    {
        int skill_num = Random.Range(0, 3);
        switch (skill_num)
        {
            case 0:
                anim.SetTrigger("Skill1");
                StartCoroutine(Skill1());
                break;
            case 1:
                anim.SetTrigger("Skill2");
                StartCoroutine(Skill2());
                break;
            case 2:
                anim.SetTrigger("Skill3");
                StartCoroutine(Skill3());
                break;
        }
    }

    IEnumerator Skill1()
    {
        yield return new WaitForSeconds(0.7f);
                audioManager.finalBossSound("Attack1");
        transform.Find("Skill1").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        transform.Find("Skill1").gameObject.SetActive(false);
    }

    IEnumerator Skill2()
    {
        yield return new WaitForSeconds(0.7f);
                audioManager.finalBossSound("Attack2");
        transform.Find("Skill2").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        transform.Find("Skill2").gameObject.SetActive(false);
    }

    IEnumerator Skill3()
    {
        yield return new WaitForSeconds(0.7f);
                audioManager.finalBossSound("Attack3");
        transform.Find("Skill3").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        transform.Find("Skill3").gameObject.SetActive(false);
    }

    void skill_recool()
    {
        is_skilling = false;
        skillattack_cool = skillattack;
        boxCollider2D.enabled = true;
    }

    public void OnDamaged(float damage)
    {
        Debug.Log("OnDamaged");
        Hp -= damage;
        hpBar.value = Hp / maxHp;
        audioManager.finalBossSound("Damaged");
        if (Hp <= 0)
        {
            audioManager.finalBossSound("Die");
            // GetComponent<PolygonCollider2D>().enabled = false;
            eventDrop.Drop(gameObject.name, gameObject.transform.position);
            anim.SetBool("IsDied", true);
            Destroy(gameObject, 1);
            GameClearUI.SetActive(true);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log(other.gameObject.tag);
        if (other.gameObject.name == "WarriorSkill4(Clone)")
        {
            if (Hp > 0)
            {
                statManager.Ad += 5 / 2f;
                statManager.Stack += 1;
            }
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            OnDamaged(PlayerDamage(other.gameObject.GetComponent<BasicAttack>().GetSkillDamage()) / 2);
            statManager.IsFighting = 5;
        }
    }
    float PlayerDamage(float Dmg)
    {
        float Damage = Dmg * statManager.Ad;

        if (statManager.Type != CurType) // 어둠 공격 아니면
            return Damage * 2 / 3;
        return Damage; // 일반타입
    }
}
