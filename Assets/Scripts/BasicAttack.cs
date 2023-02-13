using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        Debug.Log("aa");
        Invoke("DisAppear",0.3f);
    }
    void DisAppear()
    {
        gameObject.SetActive(false);
    }
}
