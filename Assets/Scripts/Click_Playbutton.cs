using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Click_Playbutton : MonoBehaviour
{
    public GameObject StartUI;
    public GameObject SkillSelectUI;
    public GameObject PassiveSkill;
    // public DiffControl Diff;
    public void ClassSelect()
    {
        StartUI.gameObject.SetActive(false);
        SkillSelectUI.gameObject.SetActive(true);

        // SceneManager.LoadScene("SampleScene");
        // DontDestroyOnLoad(Diff); 이제 static이라 괜찮음
    }
    public void GameStart()
    {
        SceneManager.LoadScene("SampleScene");
        // DontDestroyOnLoad(Diff); 이제 static이라 괜찮음
    }
    public void ToNormal()
    {
        DiffControl.Diff = 1;
    }
    public void ToHard()
    {
        DiffControl.Diff = 2;
    }
    public void ToHell()
    {
        DiffControl.Diff = 4;
    }
    public void ToWarrior()
    {
        DiffControl.Class = "Warrior";
    }
    public void ToArcher()
    {
        DiffControl.Class = "Archer";
    }
    public void Quit()
    {
        Quit();
    }
    public void LevelUp(int i)
    {
        if (DiffControl.SkillPoint > 0)
        {
            DiffControl.SkillLevel[i] += 1;
            DiffControl.SkillPoint -= 1;//SkillPoint 떨구는 코드.
        }
    }
    public void Passive()
    {
        if (DiffControl.Passive == 1)
        {
            DiffControl.Passive = 2;
            PassiveSkill.transform.GetChild(0).GetComponent<Text>().text = "Passive1 선택됨";
        }
        else
        {
            DiffControl.Passive = 1;
            PassiveSkill.transform.GetChild(0).GetComponent<Text>().text = "Passive2 선택됨";
        }

    }
}
