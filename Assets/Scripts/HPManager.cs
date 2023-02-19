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
        // if(gameManager.stageIndex == 25)
            //퀘스트매니저의 무슨무슨 도전과제 = false;
            //도전과제의 조건이 2개
            //하나는 보스가 클리어 되었는가? -> 원래 false
            //or게이트
            //하나는 보스에게서 공격을 안받았는가? ->원래 true
        IsDie();
    }

    void IsDie(){
        if(HP <= 0){
            //암튼 죽는 코드
        }
    }
}
