using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDrop : MonoBehaviour
{
    public GameObject[] WarriorSkillQube;
    public GameObject[] ArcherSkillQube;
    float probability;
    int RandomNumber;
    GameObject[] SkillQube;
    EquipManager equipManager;
    public GameObject TempItemQube;
    Sprite[] RuneItemImage;
    Sprite[] ArmorItemImage;
    Sprite[] HatItemImage;
    Sprite[] GloveItemImage;
    ItemManager itemManager;
    void Awake()
    {
        itemManager = GameObject.Find("ItemManager").gameObject.GetComponent<ItemManager>();
        RuneItemImage = itemManager.RuneItemImage;
        ArmorItemImage = itemManager.ArmorItemImage;
        HatItemImage = itemManager.HatItemImage;
        GloveItemImage = itemManager.GloveItemImage;
        
        equipManager = GameObject.Find("EquipManager").GetComponent<EquipManager>();
        switch (DiffControl.Class)
        {
            case "Warrior":
                SkillQube = WarriorSkillQube;
                break;
            case "Archer":
                SkillQube = ArcherSkillQube;
                break;
        }
    }
    public void Drop(string MonsterName , Vector2 Position)
    {   //0,1은 패시브스킬, 2,3은 짧쿨, 4,5는 긴쿨, 6,7은 궁극스킬
        transform.position = Position;
        if (MonsterName == "Boss1")
        {
            GameObject tempskill = Instantiate(SkillQube[0], transform.position, Quaternion.identity);
            
            int index = tempskill.name.IndexOf("(Clone)");
            if (index > 0) 
                tempskill.name = tempskill.name.Substring(0, index);

            transform.position = new Vector2(transform.position.x + 1 , transform.position.y);
            
            tempskill = Instantiate(SkillQube[2], transform.position, Quaternion.identity);
            
            index = tempskill.name.IndexOf("(Clone)");
            if (index > 0) 
                tempskill.name = tempskill.name.Substring(0, index);
        }
        if (MonsterName == "Boss2")
        {
             GameObject tempskill = Instantiate(SkillQube[1], transform.position, Quaternion.identity);
            
            int index = tempskill.name.IndexOf("(Clone)");
            if (index > 0) 
                tempskill.name = tempskill.name.Substring(0, index);

            transform.position = new Vector2(transform.position.x + 1 , transform.position.y);
            
            tempskill = Instantiate(SkillQube[3], transform.position, Quaternion.identity);
            
            index = tempskill.name.IndexOf("(Clone)");
            if (index > 0) 
                tempskill.name = tempskill.name.Substring(0, index);
        }
        if (MonsterName == "Boss3")
        {
             GameObject tempskill = Instantiate(SkillQube[4], transform.position, Quaternion.identity);
            
            int index = tempskill.name.IndexOf("(Clone)");
            if (index > 0) 
                tempskill.name = tempskill.name.Substring(0, index);

            transform.position = new Vector2(transform.position.x + 1 , transform.position.y);
            
            tempskill = Instantiate(SkillQube[6], transform.position, Quaternion.identity);
            
            index = tempskill.name.IndexOf("(Clone)");
            if (index > 0) 
                tempskill.name = tempskill.name.Substring(0, index);
        }
        if (MonsterName == "Boss4")
        {
             GameObject tempskill = Instantiate(SkillQube[5], transform.position, Quaternion.identity);
            
            int index = tempskill.name.IndexOf("(Clone)");
            if (index > 0) 
                tempskill.name = tempskill.name.Substring(0, index);

            transform.position = new Vector2(transform.position.x + 1 , transform.position.y);
            
            tempskill = Instantiate(SkillQube[7], transform.position, Quaternion.identity);
            
            index = tempskill.name.IndexOf("(Clone)");
            if (index > 0) 
                tempskill.name = tempskill.name.Substring(0, index);
        }
        if (MonsterName == "StageBoss1")
        {
            StageBossDrop(1);
        }
        if (MonsterName == "StageBoss2")
        {
            StageBossDrop(2);
        }
        if (MonsterName == "StageBoss3")
        {
            StageBossDrop(3);
        }
        if (MonsterName == "StageBoss4")
        {
            StageBossDrop(4);
        }
        if (MonsterName == "ItemBox")
        {
            SetProbability(5, 2);//2개에서 4개사이 드랍
            ItemBoxDrop(RandomNumber);
        }
        if (MonsterName == "HiddenItemBox")
        {
            //1개 드랍
            ItemBoxDrop(1);
        }
    }
    float SetProbability(int num = 0, int min = 0)
    {
        RandomNumber = Random.Range(min, num);
        return Random.Range(0f, 100f);
    }
    GameObject ItemInstantiate(int num, int WhatItem)
    {
        int i = 0;
        switch (WhatItem)
        {
            case 1:
                foreach (string RuneName in equipManager.RuneList.Keys)
                {
                    if (i == RandomNumber)
                    {
                        GameObject temp = Instantiate(TempItemQube,
                        transform.position + Vector3.up * 2, Quaternion.identity);
                        temp.tag = "Rune";
                        temp.GetComponent<SpriteRenderer>().sprite = RuneItemImage[i];
                        // temp.layer = LayerMask.NameToLayer("Item");
                        temp.name = RuneName;
                        return temp;
                    }
                    i++;
                }
                break;
            case 2:
                foreach (string ArmorName in equipManager.ArmorList.Keys)
                {
                    if (i == RandomNumber)
                    {
                        GameObject temp = Instantiate(TempItemQube,
                        transform.position + Vector3.up * 2, Quaternion.identity);
                        temp.tag = "Armor";
                        temp.GetComponent<SpriteRenderer>().sprite = ArmorItemImage[i];
                        // temp.layer = LayerMask.NameToLayer("Item");
                        temp.name = ArmorName;
                        return temp;
                    }
                    i++;
                }
                break;
            case 3:
                foreach (string HatName in equipManager.HatList.Keys)
                {
                    if (i == RandomNumber)
                    {
                        GameObject temp = Instantiate(TempItemQube,
                        transform.position + Vector3.up * 2, Quaternion.identity);
                        temp.tag = "Hat";
                        temp.GetComponent<SpriteRenderer>().sprite = HatItemImage[i];
                        // temp.layer = LayerMask.NameToLayer("Item");
                        temp.name = HatName;
                        return temp;
                    }
                    i++;
                }
                break;
            case 4:
                foreach (string GloveName in equipManager.GloveList.Keys)
                {
                    if (i == RandomNumber)
                    {
                        GameObject temp = Instantiate(TempItemQube,
                        transform.position + Vector3.up * 2, Quaternion.identity);
                        temp.tag = "Glove";
                        temp.GetComponent<SpriteRenderer>().sprite = GloveItemImage[i];
                        // temp.layer = LayerMask.NameToLayer("Item");
                        temp.name = GloveName;
                        return temp;
                    }
                    i++;
                }
                break;
        }
        return null;
    }
    void ItemBoxDrop(int Stage)
    {
        for (int i = 0; i < Stage; i++) //스테이지 만큼 드랍 여러번 해줌
        {
            transform.position = new Vector2(transform.position.x + 0.8f , transform.position.y);
            /*랜덤 장비아이템 1개 드랍*/
            probability = SetProbability();
            if (probability <= 25)
            {
                SetProbability(equipManager.RuneList.Count);
                ItemInstantiate(RandomNumber, 1).transform.position = transform.position;
            }
            else if (probability <= 50)
            {
                SetProbability(equipManager.ArmorList.Count);
                ItemInstantiate(RandomNumber, 2).transform.position = transform.position;
            }
            else if (probability <= 75)
            {
                SetProbability(equipManager.HatList.Count);
                ItemInstantiate(RandomNumber, 3).transform.position = transform.position;
            }
            else
            {
                SetProbability(equipManager.GloveList.Count);
                ItemInstantiate(RandomNumber, 4).transform.position = transform.position;
            }
        }
    }
    void StageBossDrop(int Stage)
    {
        float temp = SetProbability(SkillQube.Length);
        Debug.Log("Drop : " + temp.ToString());
        /*10퍼확률로 랜덤스킬 1개 드랍*/
        if (temp <= 10f)
        {
            GameObject tempskill = Instantiate(SkillQube[RandomNumber], transform.position, Quaternion.identity);
            int index = tempskill.name.IndexOf("(Clone)");
            if (index > 0) 
                tempskill.name = tempskill.name.Substring(0, index);
        }
        //stage만큼 장비드랍
        ItemBoxDrop(Stage);
    }
}
