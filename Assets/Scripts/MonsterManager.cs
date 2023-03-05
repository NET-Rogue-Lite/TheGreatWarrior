using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private Dictionary<string, float> Monsters = new Dictionary<string, float>(){
        { "Bat", 5 },
        { "Rat", 4 },
        { "Golem", 15 },
        { "Bear", 9 },
        {"Turtle", 3},
        {"Eagle", 6},
        {"Slime", 5},
        {"Slime1",2},
        {"Slime3",2},
        {"Slime4",2},
        { "StageBoss2", 15 },
        {"StageBoss2_fireball(Clone)", 20 },
        {"StageBoss2_firefloor(Clone)", 20 },
        {"Boss1",10},
        {"Bear(Clone)", 9},
        {"StageBoss1_slimespit(Clone)", 10},
        {"StageBoss1", 15},
        {"StageBoss3_Iceball(Clone)", 30},
        {"StageBoss4_BiteHit", 30},
        {"StageBoss4_Breath", 35 },
        {"StageBoss4_Tornado", 28},
        {"StageBoss4_Thunder(Clone)", 25},
        {"StageBoss4_ThunderFloor", 25},
        {"Skill1",20},
        {"Skill2",20},
        {"Skill3",20}
    };
    
    public float GetMonsterDamage(string monsterName){
        return Monsters[monsterName];
    }
}
