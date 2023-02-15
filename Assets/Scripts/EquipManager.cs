using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour
{
    public string CurRune;
    public string CurArmor;
    public string CurHat;
    public string CurGlove;
    public StatManager statManager;
    Dictionary<string, float[]> RuneList= //무기는 공격력/속성
        new Dictionary<string, float[]>()
        {
            {"BasicRune", new float[] {0,0}},
            {"NormalRune1", new float[] {30,0}}
        };
    Dictionary<string, float> ArmorList = //방어구는 방어력
        new Dictionary<string, float>()
        {
            {"BasicArmor",0},{"Armor1",30}
        };
    Dictionary<string, float> HatList = //모자는 체력
        new Dictionary<string, float>()
        {
            {"BasicHat",0},{"Hat1",10}
        };
    Dictionary<string, float[]> GloveList = //글러브는 공격력,방어력
        new Dictionary<string, float[]>()
        {
            {"BasicGlove",new float[]{0,0}},{"Glove1", new float[]{5,10}}
        };
    void Awake()
    {
        CurRune = "BasicRune";
        CurArmor = "BasicArmor";
        CurHat = "BasicHat";
        CurGlove = "BasicGlove";
        EquipRune("BasicRune");
        EquipArmor("BasicArmor");
        EquipHat("BasicHat");
        EquipGlove("BasicGlove");
    }
    public void EquipRune(string name = "BasicRune")
    {
        statManager.Ad -= RuneList[CurRune][0];
        statManager.Ad += RuneList[name][0];
        statManager.Type = (int)RuneList[name][1];
        CurRune = name;
    }
    public void EquipArmor(string name = "BasicArmor")
    {
        statManager.Def -= ArmorList[CurArmor];
        statManager.Def += ArmorList[name];
        CurArmor = name;
    }
    public void EquipHat(string name = "BasicHat")
    {
        statManager.MaxHp -= HatList[CurHat];
        statManager.MaxHp += HatList[name];
        CurHat = name;
    }
    public void EquipGlove(string name = "BasicGlove")
    {
        statManager.Ad -= GloveList[CurGlove][0];
        statManager.Ad += GloveList[name][0];
        statManager.Def -= GloveList[CurGlove][1];
        statManager.Def += GloveList[name][1];
        CurGlove = name;
    }
}
