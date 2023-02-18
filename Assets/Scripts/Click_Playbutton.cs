using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Click_Playbutton : MonoBehaviour
{
    public void SceneChange(){
        SceneManager.LoadScene("SampleScene");
    }

}
