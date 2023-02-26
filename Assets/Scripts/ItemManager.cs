using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemManager : MonoBehaviour
{
    public EquipManager equipManager;
    public AudioManager audioManager;
    public Sprite[] RuneItemImage;
    public Sprite[] ArmorItemImage;
    public Sprite[] HatItemImage;
    public Sprite[] GloveItemImage;
    private void Awake() {
    }
    public bool GetItem(GameObject Item){
        audioManager.itemSound();
        if (Item.tag == "Rune"){
            equipManager.EquipRune(Item.name);
            return true;
        }
        if(Item.tag == "Armor"){
            equipManager.EquipArmor(Item.name);
            return true;
        }
        if(Item.tag == "Hat"){
            equipManager.EquipHat(Item.name);
            return true;
        }
        if(Item.tag == "Glove"){
            equipManager.EquipGlove(Item.name);
            return true;
        }
        return false;
    }
}
