using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiffControl : MonoBehaviour
{
    public static int Diff; // GameManager에서 가져가야함
    public static string Class; // GameManager에서 가져가야함
    public static int[] SkillLevel; // SkillManager에서 가져가야함
    public static int Passive;//1아니면 2겠죠 SkillManager에서 가져가야함
    public Image Normal;
    public Image Hard;
    public Image Hell;
    public Image Warrior;
    public Image Archer;
    public static int SkillPoint;
    void Awake(){
        Passive = 1;
        SkillLevel = new int[5]{1,1,1,1,1};
        SkillPoint = 5;// <- 'SkillPoint를 업적에서 가져온다'의 코드로 변경요망
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
                Archer.color = new Color(0,0,0);
                break;
            case "Archer":
                Archer.color = new Color(255,255,255);
                Warrior.color = new Color(0,0,0);
                break;
        }
    }
}
