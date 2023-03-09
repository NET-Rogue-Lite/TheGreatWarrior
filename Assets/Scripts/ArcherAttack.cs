using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttack : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    public bool SkillCasting = false;
    public bool Casting;
    GameObject Player;
    public int Monsternum;
    public float AttackDamage;
    public int SkillLevel;
    SkillManager skillManager;
    public GameObject BasicAttack;
    public GameObject ArcherSkill1;
    public GameObject ArcherSkill5;
    public GameObject ArcherSkill6;
    public bool ArcherBonusAttack;
    StatManager statManager;
    void Awake()
    {
        Monsternum = 0;
        statManager = GameObject.Find("StatManager").GetComponent<StatManager>();
        skillManager = GameObject.Find("SkillManager").GetComponent<SkillManager>();
        anim = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
        // if (gameObject.name == "WarriorSkill2(Clone)" && !SkillCasting){
        //     gameObject.transform.position = new Vector2(gameObject.transform.position.x + (Player.GetComponent<SpriteRenderer>().flipX == true ? -3 : 3) , gameObject.transform.position.y + 5);
        // }
        
    }

    public float GetSkillDamage() {
        SkillLevel = skillManager.GetSkillLevel(gameObject.name);
        if(statManager.CirticalP>0){
            if(Random.Range(0,1f)<statManager.CirticalP){ //크리티컬 적용
                Debug.Log("크리티컬적용!");
                return AttackDamage * Mathf.Sqrt(SkillLevel) * 2; 
            }
        }
        return AttackDamage * Mathf.Sqrt(SkillLevel);
    }
    void Update() {
        
        Casting = Player.GetComponent<Animator>().GetBool("IsCasting");
    }
    void FixedUpdate()
    {

        if (gameObject.name == "BasicAttack" && anim.GetBool("IsAttacking") && !SkillCasting && !Casting){
            SkillCasting = true;
            if(ArcherBonusAttack){
                if(Random.Range(0,100)>= 50){
                    Instantiate(BasicAttack, new Vector2(gameObject.transform.position.x+ (Player.GetComponent<SpriteRenderer>().flipX == true ? -0.3f : 0.3f) , gameObject.transform.position.y+0.2f) , Quaternion.identity);
                }
            }
            Instantiate(BasicAttack, new Vector2(gameObject.transform.position.x+ (Player.GetComponent<SpriteRenderer>().flipX == true ? -0.3f : 0.3f) , gameObject.transform.position.y) , Quaternion.identity);
            // anim.SetBool("IsAttacking", true);
            Invoke("DisAppear", 0.52f);
        }
        if (gameObject.name == "BasicAttack" ){
            gameObject.GetComponent<SpriteRenderer>().flipX = Player.GetComponent<SpriteRenderer>().flipX;
        }
        
    }
    void DisAppear()
    {
        Debug.Log("DisAppear");
        anim.SetBool("IsAttacking", false);
        // gameObject.GetComponent<CircleCollider2D>().enabled = false;
        // CancelInvoke();
        // Player.gameObject.GetComponent<PlayerMove>().AttackingTurn();
        SkillCasting = false;
    }
    void AfterSkill()
    {
        Debug.Log("AfterSkill");
        Player.GetComponent<Animator>().SetBool("IsCasting", false);
        SkillCasting = false;
        Destroy(gameObject);
        if (gameObject.name == "WarriorSkill4(Clone)"){
                // statManager.Ad -= 5 * Monsternum;
                Monsternum = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if (gameObject.name == "BasicArrow"){
            
        //     if (other.gameObject.tag == "Monster" || other.gameObject.tag == "BossMonster" )
        //     {
        //         Monsternum++;
        //         Debug.Log("데마시아 사라져라");
        //         gameObject.GetComponent<CircleCollider2D>().enabled = false;
        //         Player.GetComponent<Animator>().SetBool("IsCasting", false);
        //         Destroy(gameObject);
        //     }
        // }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        
    }
}
