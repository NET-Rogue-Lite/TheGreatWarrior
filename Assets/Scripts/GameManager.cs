using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int Diff;
    public GameObject[] Stages;
    public int stageIndex;
    public bool Next;
    public GameObject Player;
    // Start is called before the first frame update
    public void IsNext(bool isNext)
    {
        Next = isNext;
    }

    public void NextStage()
    {
        if (stageIndex + 1 < Stages.Length)
        {
            Stages[stageIndex].SetActive(false);
            Stages[++stageIndex].SetActive(true);
            Player.transform.position = Vector2.zero;
        }
        else
        {
            Time.timeScale = 0;
            Debug.Log("게임클리어!");
        }
    }
}
