using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUnpack : MonoBehaviour
{
    public SkillManager skillManager;
    public void Unpack(int Button){
        skillManager.SkillUnpack(Button);
    }
}
