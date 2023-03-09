using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBoss4 : MonoBehaviour
{
    Animator anim;
    CapsuleCollider2D capuslecollider;
    private GameObject Player;
    public AudioManager audioManager;
    GameObject Bite;
    GameObject Breath;
    GameObject Tornado;
    GameObject thunder_floor;
    public GameObject thunder;
    Queue<int> skillQueue;
    public StatManager statManager;
    public EventDrop eventDrop;
    public GameObject portal;
    public float Hp;
    public float skill0;
    public float skill1;
    public float skill2;
    public float skill3;
    public int CurType;
    float skill0_cool;
    float skill1_cool;
    float skill2_cool;
    float skill3_cool;
    float playerDistance;
    bool is_skilling = false;
    int StrongType;
    int WeakType;
    //물1 > 불2 > 나무3 > 흙4 > 번개5 > 물 무속성은 6물

    void Awake()
    {
        anim = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
        skillQueue = new Queue<int>();
        Bite = transform.Find("StageBoss4_BiteHit").gameObject;
        Breath = transform.Find("StageBoss4_Breath").gameObject;
        Tornado = transform.Find("StageBoss4_Tornado").gameObject;
        thunder_floor = transform.Find("StageBoss4_ThunderFloor").gameObject;
        skill0_cool = skill0 + Random.Range(-0.2f, 0.2f);
        skill1_cool = skill1 + Random.Range(-0.5f, 0.5f);
        skill2_cool = skill2 + Random.Range(-0.5f, 0.5f);
        skill3_cool = skill3 + Random.Range(-0.5f, 0.5f);
        StrongType = (CurType + 1) % 5;
        WeakType = (CurType - 1) % 5;
        Hp *= DiffControl.Diff;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        playerDistance = Player.transform.position.x - transform.position.x;
        skill0_cool -= 0.2f + Random.Range(-0.02f, 0.02f);
        skill1_cool -= 0.2f + Random.Range(-0.02f, 0.02f);
        skill2_cool -= 0.2f + Random.Range(-0.02f, 0.02f);
        skill3_cool -= 0.2f + Random.Range(-0.02f, 0.02f);
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
        if (skill3_cool <= 0)
        {
            skillQueue.Enqueue(3);
            skill3_cool = skill3;
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
                    anim.SetTrigger("Bite");
                    StartCoroutine(Skill0());
                    Invoke("skillCancel", 3);
                    break;
                case 1:
                    //노란색으로 먼저 물드는 애니메이션
                    // anim.SetTrigger("Breath"); 이거는 아래로 내려
                    anim.SetTrigger("Fear");
                    StartCoroutine(Skill1());
                    Invoke("skillCancel", 4.5f);
                    break;
                case 2:
                    anim.SetTrigger("Fly");
                    StartCoroutine(Skill2());
                    Invoke("skillCancel", 3);
                    break;
                case 3:
                    anim.SetTrigger("Fear");
                    StartCoroutine(Skill3());
                    Invoke("skillCancel", 3);
                    break;
            }
            // Invoke("skillCancel", 3);
        }
    }
    IEnumerator Skill0()
    {
        audioManager.boss4Sound("Bite");
        yield return new WaitForSeconds(0.4f);
        Bite.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        Bite.SetActive(false);
    }
    IEnumerator Skill1()
    {
        yield return new WaitForSeconds(1.5f);
        anim.SetTrigger("Breath");
        yield return new WaitForSeconds(0.8f);
        Breath.SetActive(true);
        audioManager.boss4Sound("Fire");
        yield return new WaitForSeconds(1.5f);
        Breath.SetActive(false);
    }

    IEnumerator Skill2()
    {
        yield return new WaitForSeconds(1.8f);
        Tornado.SetActive(true);
        int count = 0;
        float cur_x = Tornado.transform.position.x;
        while (count < 20)
        {
            audioManager.boss4Sound("Wing");
            Tornado.transform.position = new Vector2(Mathf.Lerp(Tornado.transform.position.x, cur_x - 20, 0.15f), Tornado.transform.position.y);
            count++;
            yield return new WaitForSeconds(0.1f);
        }
        Tornado.SetActive(false);
        Tornado.transform.position = new Vector2(cur_x, Tornado.transform.position.y);
    }

    IEnumerator Skill3()
    {
        audioManager.boss4Sound("Thunder");
        yield return new WaitForSeconds(0.8f);
        thunder_floor.SetActive(true);
        for (int i = 0; i < 15; i++)
        {
            Debug.Log("Thunder");
            Instantiate(thunder, new Vector3(Random.Range(transform.position.x - 3f, transform.position.x - 7f), Random.Range(transform.position.y + 10, transform.position.y + 20), 0), Quaternion.identity);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(2.5f);
        thunder_floor.SetActive(false);
    }


    void skillCancel()
    {
        is_skilling = false;
    }

    void skill_recool()
    {
        is_skilling = false;
    }

    public void OnDamaged(float damage)
    {
        audioManager.boss4Sound("Damaged");
        Debug.Log("OnDamaged");
        Hp -= damage;
        if (Hp <= 0)
        {
            audioManager.boss4Sound("Die");
            GetComponent<BoxCollider2D>().enabled = false;
            anim.SetBool("IsDied", true);
            eventDrop.Drop(gameObject.name,gameObject.transform.position);
            portal.SetActive(true);
            Destroy(gameObject, 1);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log(other.gameObject.tag);
        if (other.gameObject.name == "WarriorSkill4(Clone)")
        {
            anim.SetTrigger("Hitted");
            if (Hp > 0)
            {
                statManager.Ad += 5 / 2f;
                statManager.Stack += 1;
            }
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            anim.SetTrigger("Hitted");
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
