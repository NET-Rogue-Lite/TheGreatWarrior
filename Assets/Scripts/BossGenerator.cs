using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGenerator : MonoBehaviour
{
    public GameObject[] Boss;
    void Start()
    {
        GameObject temp = Instantiate(Boss[Random.Range(0, 3)], gameObject.transform.position, Quaternion.identity);
        int index = temp.name.IndexOf("(Clone)");
        if (index > 0)
            temp.name = temp.name.Substring(0, index);

    }
}
