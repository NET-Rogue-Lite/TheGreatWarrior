using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HPManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject Player;
    public StatManager Stat;
    public float maxHp;
    public float HP;
    [SerializeField]
    public Text textHp;
    [SerializeField]
    public Slider hpBar;
    public float Shield;
    public void OnDamaged(float Ad){
        Ad = Ad * (150 / Stat.Def) * gameManager.Diff;
        if(Shield > 0){
            Ad -= Shield;
            Shield -= Ad; 
        }
        if (Ad<=0)
            return;
        HP -= Ad;
        Debug.Log("curHp " + HP);
        IsDie();
    }
    void FixedUpdate()
    {
        maxHp = Stat.MaxHp;
        hpBar.value = HP / maxHp;
        
        if (Shield > 0)
        {
            if (Shield * 0.1f < maxHp * 0.05f)
            {
                Shield -= maxHp * 0.001f;
            }
            else
                Shield = Shield * 0.998f;
        }
        else {
            Shield = 0;
        }
    }
    void IsDie(){
        if(HP <= 0){
            //암튼 죽는 코드
        }
    }
    public void Onslider()
    {
        textHp.GetComponent<Text>().text = HP.ToString();
    }
}
