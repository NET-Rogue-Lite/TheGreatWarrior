using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursedKing : MonoBehaviour
{
    Animator anim;
    SpriteRenderer spriteRenderer;
    private GameObject Player;
    Rigidbody2D rigid;
    public Slider hpBar;
    public StatManager statManager;
    public AudioManager audioManager;
    public EventDrop eventDrop;
    BoxCollider2D boxCollider2D;
    public float maxHp;
    public float Hp;
    public float skillattack;
    public int CurType;
    float skillattack_cool;
    float playerDistance;
    bool is_skilling = false;
    bool is_Walking = false;
    bool isGround = false;
    float warpcool;

    //물1 > 불2 > 나무3 > 흙4 > 번개5 > 물 무속성은 6물

    void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Player = GameObject.FindGameObjectWithTag("Player");
        boxCollider2D = GetComponent<BoxCollider2D>();
        skillattack_cool = skillattack;
        rigid = GetComponent<Rigidbody2D>();
        Hp = Hp * DiffControl.Diff;
        maxHp = Hp;
        warpcool = Random.Range(10f, 20f);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isGround){
            RaycastHit2D rayHit1 = Physics2D.Raycast(rigid.position, Vector3.down, 5, LayerMask.GetMask("Map"));
            if (rayHit1.collider != null){
                isGround = true;
                anim.SetBool("isGround", true);
            }
        }
        else{
            playerDistance = Player.transform.position.x - transform.position.x;
            transform.rotation = Quaternion.Euler(0, (playerDistance < 0) ? 180 : 0, 0);
            Attack();
            Move();
        }
    }
    
    void Move(){
        warpcool -= 0.2f;
        if (!is_skilling && warpcool <= 0){
            anim.SetTrigger("Warp");
        }
        if(playerDistance > 6 && !is_skilling){
            is_Walking = true;
            anim.SetBool("IsWalk", true);
            rigid.velocity = new Vector2((Player.transform.position - transform.position).normalized.x * 3, 0);
        }
        else{
            anim.SetBool("IsWalk", false);
            is_Walking = false;
            rigid.velocity = Vector2.zero;
        }
    }

    void warp(){
        transform.position = new Vector2(Player.transform.position.x + Random.Range(-5, 5), 0);
        warpcool = Random.Range(10f, 20f);
    }

    void Attack()
    {
        skillattack_cool -= 0.02f;
        if (skillattack_cool < 0 && !is_skilling)
        {
            is_skilling = true;
            int skill_num = Random.Range(0, 3);
            boxCollider2D.enabled = false;
            switch (skill_num)
            {
                case 0:
                    anim.SetTrigger("Skill1");
                    StartCoroutine(Skill1());
                    break;
                case 1:

                    Skill2();
                    break;
                case 2:
                    Skill3();
                    break;
            }
            skill_recool();
        }
    }

    IEnumerator Skill1()
    {
        yield return new WaitForSeconds(0.5f);
        transform.Find("Skill1").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        transform.Find("Skill1").gameObject.SetActive(false);
    }

    IEnumerator Skill2()
    {
        yield return new WaitForSeconds(0.5f);
        transform.Find("Skill2").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        transform.Find("Skill2").gameObject.SetActive(false);
    }

    IEnumerator Skill3()
    {
        yield return new WaitForSeconds(0.5f);
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
        audioManager.boss2Sound("Damaged");
        if (Hp <= 0)
        {
            audioManager.boss2Sound("Die");
            GetComponent<PolygonCollider2D>().enabled = false;
            eventDrop.Drop(gameObject.name, gameObject.transform.position);
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
            OnDamaged(PlayerDamage(other.gameObject.GetComponent<BasicAttack>().GetSkillDamage())/2); 
            statManager.IsFighting = 5;
        }
    }
    float PlayerDamage(float Dmg)
    {
        float Damage = Dmg * statManager.Ad;

        if (statManager.Type != CurType) // 약점타입
            return Damage * 2 / 3;
        return Damage; // 일반타입
    }

}
