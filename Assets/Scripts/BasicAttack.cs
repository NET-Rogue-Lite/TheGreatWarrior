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
    StatManager statManager;

    void Awake()
    {
        Monsternum = 0;
        statManager = GameObject.Find("StatManager").GetComponent<StatManager>();
        skillManager = GameObject.Find("SkillManager").GetComponent<SkillManager>();
        anim = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");

        
        if (gameObject.name == "WarriorSkill0Parent(Clone)"){
            gameObject.GetComponent<SpriteRenderer>().flipX = Player.GetComponent<SpriteRenderer>().flipX;
        }
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
        if (gameObject.name == "ArcherSkill3(Clone)"){
            gameObject.transform.position = new Vector2(gameObject.transform.position.x + (Player.GetComponent<SpriteRenderer>().flipX == true ? -3 : 3) , gameObject.transform.position.y + 5);
        }
        if (gameObject.name == "ArcherSkill4(Clone)"){
            gameObject.transform.position = new Vector2(gameObject.transform.position.x + (Player.GetComponent<SpriteRenderer>().flipX == true ? -0.5f : 0.5f) , gameObject.transform.position.y);
            SkillCasting = true;
            GameObject.Find("BasicAttack").GetComponent<Animator>().SetTrigger("KnockBack");
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(10 * (Player.GetComponent<SpriteRenderer>().flipX == true ? -1 : 1),0);
            // Player.GetComponent<Animator>().SetBool("IsCasting", false);
            Invoke("AfterSkill",0.35f);
            // anim.SetBool("IsAttacking", true);
        }
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
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (gameObject.name == "WarriorSkill0Parent(Clone)" && !SkillCasting){
            SkillCasting = true;
            Invoke("AfterSkill",0.75f);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(10 * (Player.GetComponent<SpriteRenderer>().flipX == true ? -1 : 1),0);
            Debug.Log("Skill0 beforerSkill");
        }
        else if (gameObject.name == "WarriorSkill1(Clone)" && !SkillCasting){
            Player.GetComponent<Animator>().SetBool("IsCasting", false);
            gameObject.transform.position = 
            new Vector2(Player.transform.position.x + (Player.GetComponent<SpriteRenderer>().flipX == true ? -0.1f : 0.1f) 
            , Player.transform.position.y);
        
            Invoke("AfterSkill",15.0f);
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
        else if (gameObject.name == "BasicAttack" 
         && !anim.GetBool("IsAttacking") && gameObject.GetComponent<CircleCollider2D>().enabled == true){
            
            anim.SetBool("IsAttacking", true);
            // Invoke("DisAppear", 0.5f);
        }
        if (gameObject.name == "WarriorSkill2(Clone)" && SkillCasting){
            gameObject.transform.position = new Vector2(gameObject.transform.position.x , gameObject.transform.position.y - 0.1f);
        }
        if (gameObject.name == "WarriorSkill4(Clone)" && SkillCasting){
            gameObject.GetComponent<SpriteRenderer>().flipX = Player.GetComponent<SpriteRenderer>().flipX;
            gameObject.transform.position = Player.transform.position;
        }

        if (gameObject.name == "ArcherSkill0Parent(Clone)" ){
            // SkillCasting = true;
            Player.GetComponent<Animator>().SetBool("IsCasting", false);
            Invoke("AfterSkill",0.5f);
            // anim.SetBool("IsAttacking", true);
        }
        if (gameObject.name == "ArcherSkill1(Clone)" ){
            // SkillCasting = true;
        Player.GetComponent<Animator>().SetBool("IsCasting", false);
            Invoke("AfterSkill",0.75f);
            // anim.SetBool("IsAttacking", true);
        }
        if (gameObject.name == "ArcherSkill2(Clone)" ){
            // SkillCasting = true;
            Player.GetComponent<Animator>().SetBool("IsCasting", false);
            // anim.SetBool("IsAttacking", true);
        }
        if (gameObject.name == "ArcherSkill3(Clone)" ){
            // SkillCasting = true;
            Player.GetComponent<Animator>().SetBool("IsCasting", false);
            Invoke("AfterSkill",0.85f);
            // anim.SetBool("IsAttacking", true);
        }
        if (gameObject.name == "ArcherSkill5(Clone)" ){
            gameObject.GetComponent<SpriteRenderer>().flipX = Player.GetComponent<SpriteRenderer>().flipX;
            gameObject.transform.position = Player.transform.position + 0.5f* Vector3.down;
            Player.GetComponent<Animator>().SetBool("IsCasting", false);
            Invoke("AfterSkill",20f);
        }
        if (gameObject.name == "ArcherSkill6(Clone)"){
            Player.GetComponent<Animator>().SetBool("IsCasting", false);
            Player.GetComponent<Rigidbody2D>().velocity = new Vector2 (0,  Player.GetComponent<Rigidbody2D>().velocity.y);
            Invoke("AfterSkill",0.1f);
            // anim.SetBool("IsAttacking", true);
        }
    }
    void DisAppear()
    {
        Debug.Log("DisAppear");
        anim.SetBool("IsAttacking", false);
        // Player.gameObject.GetComponent<PlayerMove>().AttackingTurn();
        if( gameObject.GetComponent<CircleCollider2D>() != null)
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        // CancelInvoke();
    }
    void AfterSkill()
    {
        Debug.Log("AfterSkill");
        SkillCasting = false;
        if( gameObject.name =="ArcherSkill6(Clone)"){
            Destroy(gameObject, 1.0f);
            
        } else {
            Player.GetComponent<Animator>().SetBool("IsCasting", false);
            Destroy(gameObject);
        }
        if (gameObject.name == "WarriorSkill4(Clone)"){
                // statManager.Ad -= 5 * Monsternum;
                Monsternum = 0;
        }
    }
    void CastingFalse(){
        Player.GetComponent<Animator>().SetBool("IsCasting", false);
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
