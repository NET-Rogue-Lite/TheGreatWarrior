using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Click_Playbutton : MonoBehaviour
{
    // public DiffControl Diff;
    public void ClassSelect(){
        SceneManager.LoadScene("SampleScene");
        // DontDestroyOnLoad(Diff);
    }
    public void GameStart(){
        
    }
    public void ToNormal(){
        DiffControl.Diff = 1;
    }
    public void ToHard(){
        DiffControl.Diff = 2;
    }
    public void ToHell(){
        DiffControl.Diff = 4;
    }
    public void ToWarrior(){
        DiffControl.Class = "Warrior";
    }
    public void ToArchor(){
        DiffControl.Class = "Archor";
    }
    public void Quit(){
        Quit();
    }
    public void LevelUp(int i){
        DiffControl.SkillLevel[i] += 1;
    }
}
