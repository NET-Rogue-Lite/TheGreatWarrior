using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDrop : MonoBehaviour
{
    public GameObject HPportion;
    public float probability;
    MonsterController monsterController;
    void Awake() {
        probability = Random.Range(0f,100f);
        monsterController = gameObject.GetComponent<MonsterController>();
    }
    private void FixedUpdate() {
        if (monsterController.Hp <= 0) {
            Debug.Log(probability);
            if(probability<=5.0f){
                Instantiate(HPportion, transform.position, Quaternion.identity);
            }
        }
    }    
}
