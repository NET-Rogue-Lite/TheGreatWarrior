using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    GameManager gameManager;
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").gameObject.GetComponent<GameManager>();
    }
    public bool IsItemStagePortal = false;
    public bool IsSkillStagePortal = false;
    public bool IsEventStagePortal = false;
    public bool IsBossClearPortal = false;
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {   
            gameManager.IsItemStagePortal = false;
            gameManager.IsSkillStagePortal = false;
            gameManager.IsEventStagePortal = false;
            gameManager.IsBossClearPortal = false;
            gameManager.IsNext(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.IsNext(true);
            gameManager.IsItemStagePortal = IsItemStagePortal;
            gameManager.IsSkillStagePortal = IsSkillStagePortal;
            gameManager.IsEventStagePortal = IsEventStagePortal;
            gameManager.IsBossClearPortal = IsBossClearPortal;
        }
    }
}
