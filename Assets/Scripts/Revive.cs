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
    public Text GameOverText;
    public int CanRevive;
    void Awake()
    {
        CanRevive = 4;
        gameManager = GameObject.Find("GameManager").gameObject.GetComponent<GameManager>();
        hpManager = GameObject.Find("HPManager").gameObject.GetComponent<HPManager>();
        Player = GameObject.FindGameObjectWithTag("Player");
        gameOver = GameObject.Find("GameOverUI");
        gameClear = GameObject.Find("GameClearUI");
    }

    private void OnEnable() {
        GameOverText.text = "Game Over\n" + "CanRevive : " + CanRevive.ToString();    
        Time.timeScale = 0;
    }
    public void Retry()
    {
        if (DiffControl.Diff == 4)
        {
            CanRevive = 0;
            RetryText.GetComponent<UnityEngine.UI.Text>().text = "Exit";
            // Exit();
        }

        else
        {
            Time.timeScale = 1;
            hpManager.HP = hpManager.maxHp;
            hpManager.hpBar.value = hpManager.HP / hpManager.maxHp;
            gameOver.SetActive(false);
            Player.transform.position = gameManager.StartPosition;
            Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            CanRevive--;
        }
    }   

    public void Exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start_Scene");
    }

}

