using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBoss2 : MonoBehaviour
{
    Animator anim;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capuslecollider;
    private GameObject Player;
    GameObject pushPlayer;
    
    public StatManager statManager;
    public GameObject fireball;
    public GameObject firefloor;
    public GameObject fireBear;

    public float Hp;
    public float skillattack;
    public float attack;
    public int CurType;

    float skillattack_cool;
    float attack_cool;
    


    float playerDistance;
    bool is_skilling = false;
    bool is_attacking = false;

    int StrongType;
    int WeakType;
    //물1 > 불2 > 나무3 > 흙4 > 번개5 > 물 무속성은 6물

    void Awake(){
        anim = GetComponent<Animator>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Player = GameObject.FindGameObjectWithTag("Player");
        pushPlayer = transform.GetChild(0).gameObject;
        skillattack_cool = skillattack;
        attack_cool = attack;
        StrongType = (CurType + 1) % 5;
        WeakType = (CurType - 1) % 5;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        playerDistance = Player.transform.position.x - transform.position.x;
        spriteRenderer.flipX = (playerDistance < 0);
        Attack();
        
    } 

    void Attack(){
        skillattack_cool -= 0.02f;
        if (attack_cool <= 0)
            attack_cool = 0;
        if (skillattack_cool < 0 && !is_attacking ){
            anim.SetTrigger("SkillAttack");
            is_skilling = true;
            int skill_num = Random.Range(0, 3);
            switch (skill_num)
            {
                case 0:
                    Skill1();
                    break;
                case 1:
                    Invoke("Skill2", 1);
                    break;
                case 2:
                    Skill3();
                    break;
            } 
            skill_recool();
        }
        if (Mathf.Abs(playerDistance) < 4){
                attack_cool -= 0.02f;
                if (attack_cool <= 0 && !is_skilling){
                    anim.SetTrigger("Attack");
                    pushPlayer.SetActive(true);
                    is_attacking = true;
                    Invoke("attack_recool", 1);
                }
            }
        else{
            is_attacking = false;
            attack_cool = attack;
        }
    }

    void Skill1(){
        for(int i = 0; i < 40*DiffControl.Diff; i++){
            Destroy(Instantiate(fireball, new Vector3(Random.Range(transform.position.x-20f, transform.position.x+20f), Random.Range(transform.position.y + 15, transform.position.y + 30), 0), Quaternion.identity), 10);
        }
    }

    void Skill2(){
        Destroy(Instantiate(firefloor, new Vector3(transform.position.x + 0.5f, transform.position.y-6.1f, 0), Quaternion.identity), 0.5f);
    }

    void Skill3(){
        for (int i = 0; i < 1*DiffControl.Diff; i++){
            Instantiate(fireBear, new Vector3(Random.Range(transform.position.x - 5, transform.position.x +5), transform.position.y-3f, 0), Quaternion.identity);
        }
    }

    void skill_recool()
    {
        is_skilling = false;
        skillattack_cool = skillattack;
    }

    void attack_recool()
    {
        is_attacking = false;
        attack_cool = attack;
        pushPlayer.SetActive(false);
    }

        public void OnDamaged(float damage)
    {
        Debug.Log("OnDamaged");
        Hp -= damage;
        if (Hp <= 0)
        {
            GetComponent<PolygonCollider2D>().enabled = false;
            anim.SetBool("IsDied", true);
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
            OnDamaged(PlayerDamage(other.gameObject.tag)); //콜라이더가 박스랑 캡슐 두개라서 나누기2
            statManager.IsFighting = 5;
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
