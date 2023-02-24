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
        { "Skull", 6},
        {"Chameleon", 10},
        {"GreenPig", 6},
        { "StageBoss2", 15 },
        {"StageBoss2_fireball(Clone)", 20 },
        {"StageBoss2_firefloor(Clone)", 20 },
        {"Bear(Clone)", 9},
        {"StageBoss1_slimespit(Clone)", 10},
        {"StageBoss1", 15},
        {"stageBoss3", 18},
        {"StageBoss3_Iceball(Clone)", 22},
        {"StageBoss4_BiteHit", 30},
        {"StageBoss4_Breath", 35 },
        {"StageBoss4_Tornado", 28},
        {"StageBoss4_Thunder(Clone)", 25},
        {"StageBoss4_ThunderFloor", 25},
    };
    
    public float GetMonsterDamage(string monsterName){
        return Monsters[monsterName];
    }
}
