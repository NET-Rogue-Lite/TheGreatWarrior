using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDrop : MonoBehaviour
{
    public string MonsterName;   
    public GameObject[] SkillQube;
    void Awake(){
        MonsterName = gameObject.name;
    }
/*
    void Update() {
        if(MonsterName == "Boss1"){
            Instantiate(SkillQube[0],transform.position,Quaternion.identity);
            Instantiate(SkillQube[1],transform.position,Quaternion.identity);
        }
        if(MonsterName == "Boss2"){
            Instantiate(SkillQube[2],transform.position,Quaternion.identity);
            Instantiate(SkillQube[3],transform.position,Quaternion.identity);
        }
        if(MonsterName == "Boss3"){
            Instantiate(SkillQube[4],transform.position,Quaternion.identity);
            Instantiate(SkillQube[5],transform.position,Quaternion.identity);
        }
        if(MonsterName == "Boss4"){
            Instantiate(SkillQube[6],transform.position,Quaternion.identity);
            Instantiate(SkillQube[7],transform.position,Quaternion.identity);
        }
    }    */
}
