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
    public HPManager hPManager;

    public int RandomStage;
    public GameObject[] ChooseStage;
    public GameObject[] SkillStage;
    public GameObject[] ItemStage;
    public int i;

    public bool IsItemStagePortal;
    public bool IsSkillStagePortal;
    public bool IsEventStagePortal;
    // Start is called before the first frame update

    private void Awake()
    {
        GameStart();
        statManager.MaxHp = hPManager.HP*2;
        RandomStage = Random.Range(0, 4);
        i = 0;
        IsItemStagePortal = false;
        IsSkillStagePortal = false;
        IsEventStagePortal = false;
    }
    public void IsNext(bool isNext)
    {
        Next = isNext;
    }

    public void NextStage()
    {
        
        if (IsEventStagePortal)
        {
            SkillStage[i-1].SetActive(false);
            ItemStage[i-1].SetActive(false);
        }
        if (IsItemStagePortal)
        {
            ToItemStage();
        }
        else if (IsSkillStagePortal)
        {
            ToSkillStage();
        }
        else if (stageIndex == RandomStage)
        {
            Stages[stageIndex++].SetActive(false);
            ChooseStage[i].SetActive(true);
            Player.transform.position = new Vector2(20, 25);
        }
        else if (stageIndex + 1 < Stages.Length)
        {
            Stages[stageIndex].SetActive(false);
            Stages[++stageIndex].SetActive(true);
            Player.transform.position = new Vector2(20, 25);

        }
        else
        {
            Time.timeScale = 0;
            Debug.Log("게임클리어!");
        }
    }
    public void ToItemStage()
    {
        ItemStage[i].SetActive(true);
        ChooseStage[i++].SetActive(false);
        Player.transform.position = new Vector2(20, 25);
    }
    public void ToSkillStage()
    {
        SkillStage[i].SetActive(true);
        ChooseStage[i++].SetActive(false);
        Player.transform.position = new Vector2(20, 25);
    }
    // public void ReturnStage()
    // {
    //     SkillStage[i].SetActive(false);
    //     ItemStage[i].SetActive(false);
    //     Stages[++stageIndex].SetActive(true);
    //     Player.transform.position = new Vector2(20, 5);
    // }
    //겜시작할때 버튼으로 클래스 선택하면 발동됨
    public void ClassSelect(string Class)
    {
        statManager.Class = Class;
    }
    //겜시작버튼 누르면 발동
    public void GameStart()
    {
        statManager.Class = "Warrior";
        // Diff = GameObject.Find("DiffController").GetComponent<DiffControl>().Diff;
        Diff = DiffControl.Diff;
        ClassSelect(DiffControl.Class);
        PM.SetClass();
        stageIndex = 0;
        Stages[stageIndex].SetActive(true);
        Player.transform.position = new Vector2(20, 5);
        equipManager.EquipRune("BasicRune");
    }
    // }
    // void Start() { //나중에 필요없음
    //     statManager.Class = "Warrior";//이거 나중에 지우삼
    //     PM.SetClass();
    // }
}
