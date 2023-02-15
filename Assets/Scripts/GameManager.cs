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
    public PlayerMove PM;
    public StatManager statManager;
    public EquipManager equipManager;
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
            equipManager.EquipRune("NormalRune1");//임시 장비 입는 코드
        }
        else
        {
            Time.timeScale = 0;
            Debug.Log("게임클리어!");
        }
    }
    //겜시작할때 버튼으로 클래스 선택하면 발동됨
    public void WarriorClassSelect(){
        statManager.Class = "Warrior";
    }
    //겜시작버튼 누르면 발동
    public void GameStart()
    {
        PM.SetClass();
    }
    void Start() { //나중에 필요없음
        statManager.Class = "Warrior";//이거 나중에 지우삼
        PM.SetClass();
    }
}
