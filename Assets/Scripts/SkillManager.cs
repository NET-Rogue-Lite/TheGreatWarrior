using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillManager : MonoBehaviour
{
    public StatManager statManager;
    public GameObject Player;
    public GameObject[] SkillBoard;//Q,W,E,R
    public float[] SkillMaxCool;//Q,W,E,R
    public float[] SkillCool;
    public GameObject[] SkillList;
    public Sprite[] SkillImg;
    public float[] SkillCoolList;
    Dictionary<string, GameObject> SkillDict;
    Dictionary<string, float> SkillCoolDict;
    Dictionary<string, Sprite> SkillImgDict;
    public Image[] SkillUI;
    void Start()
    {
        SkillDict = new Dictionary<string, GameObject>();
        SkillCoolDict = new Dictionary<string, float>();
        SkillImgDict = new Dictionary<string, Sprite>();
        SkillMaxCool = new float[4];
        SkillCool = new float[4];
        SkillBoard = new GameObject[4];
        Debug.Log(SkillList[0]);
        for (int i = 0; i < SkillList.Length; i++)
        {
            SkillDict.Add(SkillList[i].name, SkillList[i]);
            SkillCoolDict.Add(SkillList[i].name, SkillCoolList[i]);
            SkillImgDict.Add(SkillList[i].name, SkillImg[i]);
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
                SkillMaxCool[i] = SkillCoolDict[name];
                SkillCool[i] = 0;
                SkillUI[i].sprite = SkillImgDict[name];
                return true;
            }
        }
        return false;
    }
    public bool SkillCast(int Button)
    {
        if (SkillBoard[Button] != null)
        {
            if(SkillCool[Button] <= 0.01f){
                GameObject CastSkill = Instantiate(SkillBoard[Button], Player.transform.position, Quaternion.identity);
                SkillCool[Button] = SkillMaxCool[Button];
                if(CastSkill.name == "WarriorSkill4(Clone)"){
                    Invoke("TurnOffBuff",20f);
                }
                return true;
            }
        }
        return false;
    }
    void TurnOffBuff(){
        statManager.Ad -= statManager.Stack * 2.5f;
        statManager.Stack = 0;
    }
    void FixedUpdate()
    {
        //스킬 쿨타임 감소 1초당 1씩감소 - 1초에 50회 함수호출임.
        statManager.IsFighting -= 0.02f;
        if(statManager.IsFighting > 0){
            for (int i = 0; i<4; i++){
                SkillCool[i] -= 0.02f;
            }
        }
    }
}
