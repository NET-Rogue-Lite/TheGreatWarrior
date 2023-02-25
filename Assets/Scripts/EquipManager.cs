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
    public float Constant;
    
    public Dictionary<string, float[]> RuneList= //무기는 공격력/속성 // 물 > 불 > 나무 > 흙 > 번개 > 물 / 무속성은 0
    //어둠속성은 4444는 무조건 모두에게 약점속성. 단, 1.5배임.
        new Dictionary<string, float[]>()
        {
            {"BasicRune", new float[] {0,0}},
            {"WaterRune", new float[] {20,1}},
            {"FireRune", new float[] {20,2}},
            {"TreeRune", new float[] {20,3}},
            {"SoliRune", new float[] {20,4}},
            {"ThunderRune", new float[] {20,5}},
            {"NormalRune", new float[] {20,0}},
            {"Cursed Rune", new float[] {30,4444}}
        };
    public Dictionary<string, float> ArmorList = //방어구는 방어력
        new Dictionary<string, float>()
        {
            {"BasicArmor",0},{"SilverArmor",30},{"Armor0",10},{"Armor1",11},
            {"Armor2",12},{"Armor3",13},{"Armor4",14},{"Armor5",15},
            {"Armor6",16},{"Armor7",17},{"Armor8",18},{"Armor9",19},
            {"Armor10",20},{"Dragon's Scale Armor", 60}
            ,{"MetalArmor",45},{"CursedKing's Armor", 250}
        };
    public Dictionary<string, float> HatList = //모자는 체력
        new Dictionary<string, float>()
        {
            {"BasicHat",0},{"Hat0",10},{"Hat1",11},{"Hat2",12},
            {"Hat3",13},{"Hat4",10},{"Hat5",15},{"Hat6",16},
            {"Hat7",17},{"Hat8",18},{"Hat9",19},{"Dragon's Hat", 40}
            ,{"Silver Helmat",20},{"Metal Helmat",30},{"CursedKing's Helmat", 250}
        };
    public Dictionary<string, float[]> GloveList = //글러브는 공격력,방어력
        new Dictionary<string, float[]>()
        {
            {"BasicGlove",new float[]{0,0}},{"Glove0",new float[]{1,2}},
            {"Glove1",new float[]{2,3}},{"Glove2",new float[]{3,4}},
            {"Glove3",new float[]{4,5}},{"Glove4",new float[]{5,6}},
            {"Glove5",new float[]{6,7}},{"Glove6",new float[]{7,8}},
            {"Glove7",new float[]{8,9}},{"Glove8",new float[]{9,10}},
            {"Glove9",new float[]{10,11}},{"Glove10",new float[]{11,12}},
            {"Glove11",new float[]{11,12}},{"Glove12",new float[]{12,13}},
            {"Glove13",new float[]{13,14}},{"Glove14",new float[]{14,15}},
            {"SilverGlove", new float[]{15,15}},
            {"MetalGlove",new float[]{15,22.5f}},
            {"Dragon's Glove",new float[]{15,30}},
            {"CursedKing's Glove",new float[]{25,100}}
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
        Constant = DiffControl.Diff;
    }
    public void EquipRune(string name = "BasicRune")
    {
        statManager.Ad -= Mathf.Sqrt(Constant)* RuneList[CurRune][0];
        statManager.Ad += Mathf.Sqrt(Constant)* RuneList[name][0];
        statManager.Type = (int)RuneList[name][1];
        CurRune = name;
    }
    public void EquipArmor(string name = "BasicArmor")
    {
        statManager.Def -= Mathf.Sqrt(Constant)* ArmorList[CurArmor];
        statManager.Def += Mathf.Sqrt(Constant)* ArmorList[name];
        CurArmor = name;
    }
    public void EquipHat(string name = "BasicHat")
    {
        statManager.MaxHp -= Mathf.Sqrt(Constant)* HatList[CurHat];
        statManager.MaxHp += Mathf.Sqrt(Constant)* HatList[name];
        CurHat = name;
    }
    public void EquipGlove(string name = "BasicGlove")
    {
        statManager.Ad -= Mathf.Sqrt(Constant)* GloveList[CurGlove][0];
        statManager.Ad += Mathf.Sqrt(Constant)* GloveList[name][0];
        statManager.Def -= Mathf.Sqrt(Constant)* GloveList[CurGlove][1];
        statManager.Def += Mathf.Sqrt(Constant)* GloveList[name][1];
        CurGlove = name;
    }
}
