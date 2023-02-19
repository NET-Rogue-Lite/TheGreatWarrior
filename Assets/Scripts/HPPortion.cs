using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPPortion : MonoBehaviour
{
    StatManager statManager;
    HPManager hPManager;
    bool isate = false;
    void Awake()
    {
        statManager = GameObject.Find("StatManager").GetComponent<StatManager>();
        hPManager = GameObject.Find("HPManager").GetComponent<HPManager>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.name=="Player" && !isate){
            isate = true;
            hPManager.HP += statManager.MaxHp * 0.25f;
            Destroy(gameObject);
        }
    
    }
}
