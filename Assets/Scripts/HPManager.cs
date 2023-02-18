using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject Player;
    public float HP;
    public StatManager Stat;
    
    public void OnDamaged(float Ad){
        HP -= Ad * (150 / Stat.Def) * gameManager.Diff; 
        IsDie();
    }

    void IsDie(){
        if(HP <= 0){
            //암튼 죽는 코드
        }
    }
}
