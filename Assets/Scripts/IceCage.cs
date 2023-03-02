using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCage : MonoBehaviour
{
    public float Hp;
    bool Hit;
    StatManager statManager;
    
    void Start()
    {
        statManager = GameObject.Find("StatManager").GetComponent<StatManager>();
        Hit = false;
        Hp *= DiffControl.Diff;
    }
    public void OnDamaged(float damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }

    }
    void HitFalse()
    {
        Hit = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Arrow"){
            if (other.gameObject.GetComponent<ArrowMove>().IsHit){
                return;
            }
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            if (other.gameObject.tag == "Arrow"){
                other.gameObject.GetComponent<ArrowMove>().IsHit = true;
            }
        
            if (!Hit)
            {
                Invoke("HitFalse", 1f);
            }
            Hit = true;
            OnDamaged(PlayerDamage(other.gameObject.GetComponent<BasicAttack>().GetSkillDamage())); //콜라이더가 박스랑 캡슐 두개라서 나누기2
            statManager.IsFighting = 5;
        }
    }
    float PlayerDamage(float Dmg)
    {
        Debug.Log("데미지는 : " + Dmg);
        float Damage = Dmg * statManager.Ad;

        if (statManager.Type == 2) // 약점타입
            return Damage * 2;
        else if (statManager.Type == 5) // 강점타입
            return Damage / 2;
        return Damage; // 일반타입
    }
}
