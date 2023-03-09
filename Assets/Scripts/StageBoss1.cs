using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageBoss1 : MonoBehaviour
{
    Animator anim;
    SpriteRenderer spriteRenderer;
    GameObject Player;
    Rigidbody2D rigid;

    [SerializeField]
    public Slider hpBar;

    public StatManager statManager;
    public AudioManager audioManager;
    public GameObject spit;
    public EventDrop eventDrop;
    public GameObject portal;

    public float maxHp;
    public float Hp;
    public float skill0;
    public float skill1;
    public float skill2;
    public int CurType;
    Queue<int> skillQueue;
    float skill0_cool;
    float skill1_cool;
    float skill2_cool;
    bool is_skilling = false;
    float playerDistance;
    int StrongType;
    int WeakType;
    //물1 > 불2 > 나무3 > 흙4 > 번개5 > 물 무속성은 6물

    void Awake()
    {
        Debug.Log(Mathf.Round(-0.5f));
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Player = GameObject.FindGameObjectWithTag("Player");
        rigid = GetComponent<Rigidbody2D>();
        skillQueue = new Queue<int>();

        skill0_cool = skill0;
        skill1_cool = skill1;
        skill2_cool = skill2;
        StrongType = (CurType + 1) % 5;
        WeakType = (CurType - 1) % 5;

        Hp = Hp * DiffControl.Diff;
        maxHp = Hp;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        playerDistance = Player.transform.position.x - transform.position.x;
        spriteRenderer.flipX = (playerDistance < 0);
        skill0_cool -= 0.2f;
        skill1_cool -= 0.2f;
        skill2_cool -= 0.2f;
        if (skill0_cool <= 0)
        {
            skillQueue.Enqueue(0);
            skill0_cool = skill0;
        }
        if (skill1_cool <= 0)
        {
            skillQueue.Enqueue(1);
            skill1_cool = skill1;
        }
        if (skill2_cool <= 0)
        {
            skillQueue.Enqueue(2);
            skill2_cool = skill2;
        }
        Attack();

    }

    void Attack()
    {
        if (skillQueue.Count > 0 && !is_skilling)
        {
            is_skilling = true;
            int skill_num = skillQueue.Dequeue();
            switch (skill_num)
            {
                case 0:
                    anim.SetTrigger("SkillAttack");
                    StartCoroutine(Skill0());
                    break;
                case 1:
                    anim.SetTrigger("Spin");
                    var direc = 1;
                    if (spriteRenderer.flipX)
                        direc = -1;
                    StartCoroutine(Skill1(direc));
                    break;
                case 2:
                    anim.SetTrigger("Jump");
                    Skill2();
                    break;
            }
            Invoke("skillCancel", 3);
        }
    }

    void skillCancel()
    {
        is_skilling = false;
    }

    IEnumerator Skill0()
    {
        for (int i = 0; i < 4 * DiffControl.Diff; i++)
        {
            audioManager.boss1Sound("Ball");
            GameObject spits = Instantiate(spit, transform.position, Quaternion.identity);
            spits.GetComponent<SpriteRenderer>().flipX = spriteRenderer.flipX;
            if (spriteRenderer.flipX)
                spits.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-7f, -20f), Random.Range(-1f, 1f));
            else
                spits.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(7f, 20f), Random.Range(-1f, 1f));
            Destroy(spits, 8);
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator Skill1(int direc)
    {
        audioManager.boss1Sound("Tornado");
        yield return new WaitForSeconds(0.25f);
        rigid.velocity = new Vector2(direc * 8.5f, 0);
        yield return new WaitForSeconds(2.3f);
        rigid.velocity = Vector2.zero;
    }
    void Skill2()
    {
        transform.position = new Vector2(Player.transform.position.x, Player.transform.position.y+5);
    }

    public void OnDamaged(float damage)
    {
        Debug.Log("OnDamaged");
        Hp -= damage;
        hpBar.value = Hp / maxHp;
        audioManager.boss1Sound("Damaged");
        if (Hp <= 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            anim.SetBool("IsDied", true);
            portal.SetActive(true);
            eventDrop.Drop(gameObject.name,gameObject.transform.position);
            Destroy(gameObject, 1);
            audioManager.boss1Sound("Die");
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
            OnDamaged(PlayerDamage(other.gameObject.GetComponent<BasicAttack>().GetSkillDamage())); //콜라이더가 박스랑 캡슐 두개라서 나누기2
            statManager.IsFighting = 5;
        }
    }
    float PlayerDamage(float Dmg)
    {
        float Damage = Dmg * statManager.Ad;
        if(statManager.Type == 4444)
            return Damage * 1.5f;
        else if (statManager.Type == WeakType) // 약점타입
            return Damage * 2;
        else if (statManager.Type == StrongType) // 강점타입
            return Damage / 2;
        return Damage; // 일반타입
    }
}
