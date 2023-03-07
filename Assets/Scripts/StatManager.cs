using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public float IsFighting;
    public float Def;
    public float Ad;
    public float MaxHp;
    public string Class;
    public int Type;
    public int Stack = 0;
    public float CirticalP;
    //물1 > 불2 > 나무3 > 흙4 > 번개5 > 물 , 무속성은 0
    // Start is called before the first frame update
    public void ShieldOn(float amount){
        GameObject.Find("HPManager").GetComponent<HPManager>().ShieldOn(amount);
    }
    void Awake()
    {
        Type = 0;
        switch(Class){
            case "Warrior":
                Def = 100;
                break;
            case "Archer":
                Def = 70;
                break;
        }
    }
    // Update is called once per frame
}
