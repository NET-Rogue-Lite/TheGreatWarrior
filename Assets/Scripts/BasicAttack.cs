using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    bool SkillCasting = false;
    void Awake() {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        Debug.Log(gameObject.name);
        if (gameObject.name == "WarriorSkill5(Clone)" && !SkillCasting){
            SkillCasting = true;
            Invoke("AfterSkill",2.5f);
            Debug.Log("Skill5 beforerSkill");
        }
        else if ( gameObject.GetComponent<CircleCollider2D>().enabled == true && !SkillCasting)
            anim.SetBool("IsAttacking",true);
            Invoke("DisAppear",0.3f);
        
    }
    void DisAppear()
    {
        // CancelInvoke();
        anim.SetBool("IsAttacking",false);
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }
    void AfterSkill()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<Animator>().SetBool("IsCasting",false);
        SkillCasting = false;
        Debug.Log("Skill5 AfterSkill");
        Destroy(gameObject);
    }
}
