using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherSkill2 : MonoBehaviour
{
    int i;
    public GameObject Arrow;
    void Awake(){
        Destroy(gameObject,5);
        i=0;
    }
    void FixedUpdate(){
        if ( i++ == 10){
            Instantiate(Arrow, gameObject.transform.position, Quaternion.identity);
            i = 0;
        }
    }
}
