using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    public bool SkillCasting = false;
    GameObject Player;
    public int Monsternum;
    public float AttackDamage;
    public int SkillLevel;
    SkillManager skillManager;

    void Awake()
    {
        Monsternum = 0;
        skillManager = GameObject.Find("SkillManager").GetComponent<SkillManager>();
        anim = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
        if (gameObject.name == "WarriorSkill2(Clone)" && !SkillCasting){
            gameObject.transform.position = new Vector2(gameObject.transform.position.x + (Player.GetComponent<SpriteRenderer>().flipX == true ? -3 : 3) , gameObject.transform.position.y + 5);
        }
        if (gameObject.name == "WarriorSkill3(Clone)" && !SkillCasting){
            gameObject.transform.position = new Vector2(gameObject.transform.position.x + (Player.GetComponent<SpriteRenderer>().flipX == true ? -3 : 3) , gameObject.transform.position.y + 1);
            gameObject.transform.rotation = Quaternion.Euler(0, (Player.GetComponent<SpriteRenderer>().flipX == true ? 180 : 0) , 0);
        }
        if (gameObject.name == "WarriorSkill4(Clone)" && !SkillCasting){
            gameObject.transform.position = new Vector2(gameObject.transform.position.x , gameObject.transform.position.y);
        }
        if (gameObject.name == "WarriorSkill5(Clone)" && !SkillCasting){
            gameObject.transform.position = new Vector2(gameObject.transform.position.x , gameObject.transform.position.y);
        }
        if (gameObject.name == "WarriorSkill6(Clone)" && !SkillCasting){
            gameObject.transform.position = new Vector2(gameObject.transform.position.x , gameObject.transform.position.y);
            gameObject.transform.rotation = Quaternion.Euler(0, (Player.GetComponent<SpriteRenderer>().flipX == true ? 180 : 0) , 0);
        }
    }

    public float GetSkillDamage() {
        SkillLevel = skillManager.GetSkillLevel(gameObject.name);
        return AttackDamage * Mathf.Sqrt(SkillLevel); 
    }
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (gameObject.name == "WarriorBasicSkill(Clone)" && !SkillCasting){
            SkillCasting = true;
            Invoke("AfterSkill",2.5f);
            Debug.Log("Skill0 beforerSkill");
        }
        else if (gameObject.name == "WarriorSkill1(Clone)" && !SkillCasting){
            SkillCasting = true;
            Invoke("AfterSkill",2.5f);
            Debug.Log("Skill1 beforerSkill");
        }
        else if (gameObject.name == "WarriorSkill2(Clone)" && !SkillCasting){
            SkillCasting = true;
            Invoke("AfterSkill",1.0f);
            Debug.Log("Skill2 beforerSkill");
        }
        else if (gameObject.name == "WarriorSkill3(Clone)" && !SkillCasting){
            SkillCasting = true;
            Invoke("AfterSkill",1.0f);
            Debug.Log("Skill3 beforerSkill");
        }
        else if (gameObject.name == "WarriorSkill4(Clone)" && !SkillCasting){
            SkillCasting = true;
            Player.GetComponent<Animator>().SetBool("IsCasting",false);
            Invoke("AfterSkill",20f);
            Debug.Log("Skill4 beforerSkill");
        }
        else if (gameObject.name == "WarriorSkill5(Clone)" && !SkillCasting){
            SkillCasting = true;
            Invoke("AfterSkill",2.5f);
            Debug.Log("Skill5 beforerSkill");
        }
        else if (gameObject.name == "WarriorSkill6(Clone)" && !SkillCasting){
            SkillCasting = true;
            Invoke("AfterSkill",0.5f);
            Debug.Log("Skill6 beforerSkill");
        }
        else if (gameObject.name == "BasicAttack" && gameObject.GetComponent<CircleCollider2D>().enabled == true && !anim.GetBool("IsAttacking")){
            anim.SetBool("IsAttacking", true);
            Invoke("DisAppear", 0.3f);
        }


        if (gameObject.name == "WarriorSkill2(Clone)" && SkillCasting){
            gameObject.transform.position = new Vector2(gameObject.transform.position.x , gameObject.transform.position.y - 0.1f);
        }
        if (gameObject.name == "WarriorSkill4(Clone)" && SkillCasting){
            gameObject.GetComponent<SpriteRenderer>().flipX = Player.GetComponent<SpriteRenderer>().flipX;
            gameObject.transform.position = Player.transform.position;
        }
    }
    void DisAppear()
    {
        Debug.Log("DisAppear");
        anim.SetBool("IsAttacking", false);
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        // CancelInvoke();
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
        if (gameObject.name == "WarriorSkill2(Clone)"){
            
            if (other.gameObject.tag == "Monster" || other.gameObject.tag == "BossMonster" )
            {
                Monsternum++;
                Debug.Log("데마시아 사라져라");
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                Player.GetComponent<Animator>().SetBool("IsCasting", false);
                Destroy(gameObject);
            }
        }
        if (gameObject.name == "WarriorSkill4(Clone)"){
            if(other.gameObject.tag == "Monster")
            {
                // statManager.Ad += 5;
                Monsternum++;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        
    }
}
