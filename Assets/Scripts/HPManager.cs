using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HPManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject Player;
    public GameObject GameOver;
    public StatManager Stat;

    public float maxHp;
    public float HP;
    public Text textHp;
    public Slider hpBar;

    public Slider shieldBar;
    public float Shield;

    public void OnDamaged(float Ad){
        Ad = Ad * (150 / Stat.Def) * gameManager.Diff;
        if(Shield > 0){
            float tmp;
            tmp = Ad;
            Ad -= Shield;
            Shield -= tmp; 
        }
        if (Ad<=0)
            return;
        HP -= Ad;
        Debug.Log("curHp " + HP);
        IsDie();
    }
    void FixedUpdate()
    {
        if(HP>maxHp)
            HP -= (HP%maxHp);
        maxHp = Stat.MaxHp;
        hpBar.value = HP / maxHp;
        shieldBar.value = Shield / maxHp;
        
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
            Gameover();
            Player.transform.position = new Vector2(1000,-1000);
        }
    }
    public void Gameover()
    {
        GameOver.SetActive(true);
    }
    public void Onslider()
    {
        textHp.GetComponent<Text>().text = HP.ToString();
     
    }
    
    public void ShieldOn(float amount){
        Shield += maxHp * amount;
    }
}
