using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillText : MonoBehaviour
{
    public int skillnum;

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.GetComponent<Text>().text = DiffControl.SkillLevel[skillnum].ToString();
    }
}
