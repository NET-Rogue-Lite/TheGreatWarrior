using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;

    void Awake() {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if ( gameObject.GetComponent<CircleCollider2D>().enabled == true)
            anim.SetBool("IsAttacking",true);
            Invoke("DisAppear",0.2f);
    }
    void DisAppear()
    {
        CancelInvoke();
        anim.SetBool("IsAttacking",false);
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }
}
