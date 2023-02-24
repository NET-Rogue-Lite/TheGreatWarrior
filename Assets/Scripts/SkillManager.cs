using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SkillManager : MonoBehaviour
{
    public StatManager statManager;
    public AudioManager audioManager;
    public GameObject Player;
    public GameObject[] SkillBoard;//Q,W,E,R
    public float[] SkillMaxCool;//Q,W,E,R
    public float[] SkillCool;
    public GameObject[] WarriorSkillList;
    public GameObject[] ArcherSkillList;
    public GameObject[] SkillList;
    public Sprite[] WarriorSkillImg;
    public Sprite[] ArcherSkillImg;
    public Sprite[] SkillImg;
    public float[] WarriorSkillCoolList;
    public float[] ArcherSkillCoolList;
    float[] SkillCoolList;
    Dictionary<string, GameObject> SkillDict;
    Dictionary<string, float> SkillCoolDict;
    Dictionary<string, Sprite> SkillImgDict;
    public Image[] SkillUI;
    public TextMeshProUGUI[] SkillCoolTime;
    public int[] SkillLevel;
    void Start()
    {
        if (statManager.Class == "Warrior")
        {
            SkillList = WarriorSkillList;
            SkillImg = WarriorSkillImg;
            SkillCoolList = WarriorSkillCoolList;
            if (DiffControl.Passive==2) 
                statManager.Ad+= 10;
            else
                statManager.Def += 50;
        }
        else if (statManager.Class == "Archer")
        {
            SkillList = ArcherSkillList;
            SkillImg = ArcherSkillImg;
            SkillCoolList = ArcherSkillCoolList;
            if (DiffControl.Passive==2) 
                statManager.CirticalP+=0.3f;
            else
                GameObject.Find("BasicAttack").GetComponent<ArcherAttack>().ArcherBonusAttack = true;
        }
        SkillDict = new Dictionary<string, GameObject>();
        SkillCoolDict = new Dictionary<string, float>();
        SkillImgDict = new Dictionary<string, Sprite>();
        SkillMaxCool = new float[4];
        SkillCool = new float[4];
        SkillBoard = new GameObject[4];
        SkillLevel = DiffControl.SkillLevel;
        for (int i = 0; i < SkillList.Length; i++)
        {
            SkillDict.Add(SkillList[i].name, SkillList[i]);
            SkillCoolDict.Add(SkillList[i].name, SkillCoolList[i]);
            SkillImgDict.Add(SkillList[i].name, SkillImg[i]);
        }
        SkillEquip(DiffControl.Class + "Skill0Parent");
    }
    public int GetSkillLevel(string sname){
        for (int i = 0 ; i < SkillList.Length; i++){
            if(SkillList[i].name+"(Clone)" == sname){
                return SkillLevel[i];
            }
        }
        return 1;
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
            if (SkillCool[Button] <= 0.01f)
            {
                audioManager.skillSound(SkillBoard[Button].name);
                if(SkillBoard[Button].name == "ArcherSkill5"){
                    statManager.Ad = statManager.Ad * 1.3f;
                    statManager.CirticalP += 1;
                    Invoke("TurnOffArcherBuff",20);
                }
                if (SkillBoard[Button].name == "WarriorSkill1")
                {
                    statManager.ShieldOn(0.5f);
                    statManager.MaxHp+=1;
                    return true;
                }
                GameObject CastSkill = Instantiate(SkillBoard[Button], Player.transform.position, Quaternion.identity);
                SkillCool[Button] = SkillMaxCool[Button];
                if (CastSkill.name == "WarriorSkill4(Clone)")
                {
                    statManager.ShieldOn(1);
                    Invoke("TurnOffBuff", 20f);
                }
                else if (CastSkill.name == "WarriorSkill5(Clone)")
                {
                    statManager.ShieldOn(0.5f);
                }
                return true;
            }
        }
        return false;
    }
    void TurnOffArcherBuff(){
        statManager.Ad = statManager.Ad * 10 / 13;
        statManager.CirticalP -= 1;
    }
    void TurnOffBuff()
    {
        statManager.Ad -= statManager.Stack * 2.5f;
        statManager.Stack = 0;
    }
    void FixedUpdate()
    {
        //스킬 쿨타임 감소 1초당 1씩감소 - 1초에 50회 함수호출임.
        statManager.IsFighting -= 0.02f;
        if (statManager.IsFighting > -0.04f)
        {
            for (int i = 0; i < 4; i++)
            {
                if (SkillCool[i] > 0)
                    SkillCool[i] -= 0.02f;
            }
        }
        for (int i = 0; i < 4; i++)
        {
            SkillCoolTime[i].text = Mathf.Ceil(SkillCool[i]).ToString();
            if (SkillCoolTime[i].text == "0")
            {
                SkillCoolTime[i].text = "";
            }
        }
    }
}
