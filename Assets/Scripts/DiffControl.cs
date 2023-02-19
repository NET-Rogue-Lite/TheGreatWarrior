using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiffControl : MonoBehaviour
{
    public static int Diff;
    public static string Class;
    public static int[] SkillLevel;
    public Image Normal;
    public Image Hard;
    public Image Hell;
    public Image Warrior;
    public Image Archor;
    void Awake(){
        SkillLevel = new int[5]{1,1,1,1,1};
    }
    
    void FixedUpdate() {
        switch(Diff){
            case 1:
                Normal.color = new Color(255,255,255);
                Hard.color = new Color(0,0,0);  
                Hell.color = new Color(0,0,0);  

                break;
            case 2:
                Hard.color = new Color(255,255,255);
                Normal.color = new Color(0,0,0);
                Hell.color = new Color(0,0,0);  
                break;
            case 4:
                Hell.color = new Color(255,255,255);    
                Normal.color = new Color(0,0,0);     
                Hard.color = new Color(0,0,0);   
                break;
        }
        switch(Class){
            case "Warrior":
                Warrior.color = new Color(255,255,255);
                Archor.color = new Color(0,0,0);
                break;
            case "Archor":
                Archor.color = new Color(255,255,255);
                Warrior.color = new Color(0,0,0);
                break;
        }
    }
}
