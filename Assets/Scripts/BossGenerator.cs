using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGenerator : MonoBehaviour
{
    public GameObject[] Boss;
    void Start()
    {
        GameObject temp = Instantiate(Boss[Random.Range(0, 1)], gameObject.transform.position, Quaternion.identity);//나중에 (0,1)에서 1 을 4로
        int index = temp.name.IndexOf("(Clone)");
        if (index > 0)
            temp.name = temp.name.Substring(0, index);

    }
}
