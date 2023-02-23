using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClassSkill : MonoBehaviour
{
    public Sprite[] WarriorSkillImg;
    public Sprite[] ArcherSkillImg;
    public Text SkillPoint;
    GameObject[] Skills;
    // Start is called before the first frame update
    private void Awake() {
        Skills = new GameObject[7];
        for (int i = 0; i < Skills.Length; i++){
            Skills[i] = transform.GetChild(i).gameObject;
        }    
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SkillPoint.text = "SKillPoint : " + DiffControl.SkillPoint.ToString();
        // Debug.Log("돌아가는중");
        switch (DiffControl.Class){
            case "Warrior":
                // Debug.Log("전사");
                for (int i = 0; i < Skills.Length; i++){
                    Skills[i].GetComponent<Image>().sprite = WarriorSkillImg[i];
                    }
                break;
            case "Archer":
                // Debug.Log("궁수");
                for (int i = 0; i < Skills.Length; i++){
                    Skills[i].GetComponent<Image>().sprite = ArcherSkillImg[i];
                }
                break;
        }
    }
}
