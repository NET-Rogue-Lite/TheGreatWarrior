using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageBoss3 : MonoBehaviour
{
    Animator anim;
    SpriteRenderer spriteRenderer;
    private GameObject Player;
    GameObject pushPlayer;

    [SerializeField]
    public Slider hpBar;
    public StatManager statManager;
    public AudioManager audioManager;
    public GameObject iceball;
    GameObject icefloor;
    public GameObject icecage;
    public EventDrop eventDrop;
    public GameObject portal;

    public float maxHp;
    public float Hp;
    public float skillattack;
    public float attack;
    public int CurType;
    float skillattack_cool;
    float attack_cool;
    float playerDistance;
    bool is_skilling = false;
    bool is_attacking = false;
    bool skill1 = false;
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
        icefloor = transform.Find("IceFloor").gameObject;
        Hp = Hp * DiffControl.Diff;
        maxHp = Hp;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        playerDistance = Player.transform.position.x - transform.position.x;
        spriteRenderer.flipX = (playerDistance < 0);
        Attack();
        if (!skill1)
            StartCoroutine(Skill1());
    } 

    void Attack(){
        skillattack_cool -= 0.02f;
        if (attack_cool <= 0)
            attack_cool = 0;
        if (skillattack_cool < 0 && !is_attacking ){
            anim.SetTrigger("SkillAttack");
            is_skilling = true;
            int skill_num = Random.Range(1, 3);
            switch (skill_num)
            {
                case 1:
                    StartCoroutine(Skill2());
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

    IEnumerator Skill1(){
        Debug.Log("Skill1");
        skill1 = true;
        yield return new WaitForSeconds(1.3f);
        for(int i = 0; i < 12; i++){
            var ice = Instantiate(iceball, new Vector3(transform.position.x + (spriteRenderer.flipX ? -1 : 1), transform.position.y, 0), Quaternion.identity);
            ice.GetComponent<Rigidbody2D>().velocity = (Player.transform.position - ice.transform.position)*1.4f;
            Destroy(ice,10);
            yield return new WaitForSeconds(0.6f);
        }
        skill1 = false;
    }

    IEnumerator Skill2(){
        Debug.Log("Skill2");
        yield return new WaitForSeconds(0.8f);
        icefloor.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        icefloor.SetActive(false);
    }

    void Skill3(){
        Debug.Log("Skill3");
        for (int i = 0; i < 2*DiffControl.Diff; i++){
            Debug.Log("IceCage Instantiate");
            Destroy(Instantiate(icecage, new Vector3(Random.Range(Player.transform.position.x-12f, Player.transform.position.x+12f), Random.Range(Player.transform.position.y+ 8, Player.transform.position.y + 10), 0), Quaternion.identity), 20);
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
        hpBar.value = Hp / maxHp;
        if (Hp <= 0)
        {
            audioManager.boss3Sound("Die");
            GetComponent<PolygonCollider2D>().enabled = false;
            eventDrop.Drop(gameObject.name, gameObject.transform.position);
            anim.SetBool("IsDied", true);
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
            OnDamaged(PlayerDamage(other.gameObject.GetComponent<BasicAttack>().GetSkillDamage()) ); //콜라이더가 박스랑 캡슐 두개라서 나누기2
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
