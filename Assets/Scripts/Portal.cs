using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameManager gameManager;

    public bool IsItemStagePortal = false;
    public bool IsSkillStagePortal = false;
    public bool IsEventStagePortal = false;
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {   
            gameManager.IsItemStagePortal = false;
            gameManager.IsSkillStagePortal = false;
            gameManager.IsEventStagePortal = false;
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
        }
    }
}
