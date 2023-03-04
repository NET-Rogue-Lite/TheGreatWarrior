using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Revive : MonoBehaviour
{

    GameManager gameManager;
    HPManager hpManager;
    GameObject Player;
    GameObject gameOver;
    GameObject gameClear;
    public UnityEngine.UI.Text RetryText;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").gameObject.GetComponent<GameManager>();
        hpManager = GameObject.Find("HPManager").gameObject.GetComponent<HPManager>();
        Player = GameObject.FindGameObjectWithTag("Player");
        gameOver = GameObject.Find("GameOverUI");
        gameClear = GameObject.Find("GameClearUI");
    }

    
    public void Retry()
    {
        if (DiffControl.Diff == 4)
        {
            RetryText.GetComponent<UnityEngine.UI.Text>().text = "Exit";
            Exit();
        }

        else
        {
            hpManager.HP = hpManager.maxHp;
            hpManager.hpBar.value = hpManager.HP / hpManager.maxHp;
            gameOver.SetActive(false);
            Player.transform.position = gameManager.StartPosition;
        }
    }   

    public void Exit()
    {
        SceneManager.LoadScene("Start_Scene");
    }

}

