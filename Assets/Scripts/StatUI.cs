using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    public StatManager statManager;
    public HPManager hPManager;
    public EquipManager equipManager;
    public SkillManager skillManager;
    public Text Ad;
    public Text Type;
    public Text Hp;
    public Text MaxHp;
    public Text Shield;
    public Text Def;
    public Text PassiveSkill;
    public Text SkillLevel;
    public Text CurItem;
    void OnEnable() {
        Ad.text = statManager.Ad.ToString();
        Type.text = ShowType(statManager.Type);
        Hp.text = hPManager.HP.ToString();
        MaxHp.text = statManager.MaxHp.ToString();
        Shield.text = hPManager.Shield.ToString();
        Def.text = statManager.Def.ToString();
        PassiveSkill.text = "발동 중인 패시브 스킬 : "  + skillManager.Passive.ToString();
        SkillLevel.text = "-스킬 레벨-\n"+
        "기본스킬 : Lv" + skillManager.SkillLevel[0].ToString()+
        "\n스킬 1 : Lv" + skillManager.SkillLevel[1].ToString()+
        "\t스킬 2 : Lv" + skillManager.SkillLevel[2].ToString()+
        "\n스킬 3 : Lv" + skillManager.SkillLevel[3].ToString()+
        "\t스킬 4 : Lv" + skillManager.SkillLevel[4].ToString()+
        "\n스킬 5 : Lv" + skillManager.SkillLevel[5].ToString()+
        "\t스킬 6 : Lv" + skillManager.SkillLevel[6].ToString();
        CurItem.text = "-착용 중인 아이템-" +
        "\n룬 : " + equipManager.CurRune +
        "\n모자 : " + equipManager.CurHat +
        "\n갑옷 : " + equipManager.CurArmor +
        "\n장갑 : " + equipManager.CurGlove;
    }
    string ShowType(float type) {
        
        Color color;
        switch (type){
            case 1:
                ColorUtility.TryParseHtmlString("#8DB0FF", out color); 
                Type.color = color;
                return "물";
            case 2:
                ColorUtility.TryParseHtmlString("#B44D4D", out color); 
                Type.color = color;
                return "불";
            case 3:
                ColorUtility.TryParseHtmlString("#A0F1A5", out color); 
                Type.color = color;
                return "나무";
            case 4:
                ColorUtility.TryParseHtmlString("#9F6952", out color); 
                Type.color = color;
                return "흙";
            case 5:
                ColorUtility.TryParseHtmlString("#DBCB77", out color); 
                Type.color = color;
                return "번개";
            case 0:
                ColorUtility.TryParseHtmlString("#FFFFFF", out color); 
                Type.color = color;
                return "일반";
            case 4444:
                ColorUtility.TryParseHtmlString("#2F0033", out color); 
                Type.color = color;
                return "어둠";
            default:
                return "일반";    
        }
    }
}
