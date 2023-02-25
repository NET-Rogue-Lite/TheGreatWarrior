using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    EquipManager equipManager;
    void Awake(){
        equipManager = GameObject.Find("EquipManager").GetComponent<EquipManager>();
        if(gameObject.tag == "Rune"){
            transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text 
            = gameObject.name
            +"\n공격력 : "+equipManager.RuneList[gameObject.name][0].ToString()
            +"\n속성 : "+equipManager.RuneList[gameObject.name][1].ToString();
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
