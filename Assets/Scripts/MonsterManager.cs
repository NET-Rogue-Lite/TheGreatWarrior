using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private Dictionary<string, float> Monsters = new Dictionary<string, float>(){
        { "Bat", 5 },
        { "Rat", 4 },
        { "Golem", 20 },
        { "Bear", 3 }
    };
    
    public float GetMonsterDamage(string monsterName){
        return Monsters[monsterName];
    }
}
