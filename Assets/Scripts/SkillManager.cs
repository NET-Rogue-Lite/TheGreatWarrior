using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] SkillBoard;//Q,W,E,R
    public GameObject[] SkillList;
    Dictionary<string, GameObject> SkillDict;
    void Start()
    {
        SkillDict = new Dictionary<string, GameObject>();
        SkillBoard = new GameObject[4];
        Debug.Log(SkillList[0]);
        for (int i = 0; i < SkillList.Length; i++)
        {
            SkillDict.Add(SkillList[i].name, SkillList[i]);
        }
    }
    public bool SkillEquip(string name)
    {
        Debug.Log(name);
        for (int i = 0; i < SkillBoard.Length; i++)
        {
            if (SkillBoard[i] == null)
            {
                SkillBoard[i] = SkillDict[name];
                return true;
            }
        }
        return false;
    }
    public bool SkillCast(int Button)
    {
        if (SkillBoard[Button] != null)
        {
            GameObject CastSkill = Instantiate(SkillBoard[Button]);
            return true;
        }
        return false;
    }
}
