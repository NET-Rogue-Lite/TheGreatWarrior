using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    EquipManager equipManager;
    void Awake(){
        equipManager = GameObject.Find("EquipManager").GetComponent<EquipManager>();
    }
    void FixedUpdate(){
        if(gameObject.tag == "Rune"){
            transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text 
            = gameObject.name
            +"\n공격력 : "+equipManager.RuneList[gameObject.name][0].ToString()
            +"\n속성 : "+ ShowType(equipManager.RuneList[gameObject.name][1]);
        } else if( gameObject.tag == "Armor"){
            transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text 
            = gameObject.name
            +"방어력 : "+equipManager.ArmorList[gameObject.name].ToString();
        } else if (gameObject.tag == "Hat"){
            transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text 
            = gameObject.name
            +"방어력 : "+equipManager.HatList[gameObject.name].ToString();
        } else if (gameObject.tag == "Glove"){
            transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text 
            = "이름 : "+gameObject.name
            +"\n공격력 : "+equipManager.GloveList[gameObject.name][0].ToString()
            +"\n방어력 : "+equipManager.GloveList[gameObject.name][1].ToString();
        }
    }
    string ShowType(float type) {
        switch (type){
            case 1:
                return "물";
            case 2:
                return "불";
            case 3:
                return "나무";
            case 4:
                return "흙";
            case 5:
                return "번개";
            case 0:
                return "일반";
            case 4444:
                return "어둠";
            default:
                return "일반";    
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        GameObject Other = other.gameObject;
        Debug.Log(Other.name);
        if (Other.layer == LayerMask.NameToLayer("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        GameObject Other = other.gameObject;
        if (Other.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log(Other.name);
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
