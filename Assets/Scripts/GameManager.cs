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
    public GameObject[] BossStage;
    public int i;

    public bool IsItemStagePortal;
    public bool IsSkillStagePortal;
    public bool IsEventStagePortal;
    Vector2 StartPosition;
    // Start is called before the first frame update

    private void Awake()
    {
        StartPosition = new Vector2(25,3);
        GameStart();
        statManager.MaxHp = hPManager.HP*2;
        RandomStage = Random.Range(1, 2);
        i = 0;
        IsItemStagePortal = false;
        IsSkillStagePortal = false;
        IsEventStagePortal = false;
    }
    public void IsNext(bool isNext)
    {
        Next = isNext;
    }
    // public void BossOutPortal(int n){
    //     BossStage[n].SetActive(false);//1탄 깨면 4로 가니까 맞음
    //     Stages[n*4].SetActive(true);
    // }
    public void NextStage()
    {
        int CurStage = stageIndex;
        int NextStage = stageIndex+1;
        BossStage[i==0?0:i-1].SetActive(false);
        if (IsItemStagePortal)
        {
            ToItemStage();
        }
        else if (IsSkillStagePortal)
        {
            ToSkillStage();
        }
        else if (IsEventStagePortal)
        {
            SkillStage[i-1].SetActive(false);
            ItemStage[i-1].SetActive(false);
            Stages[CurStage].SetActive(true);
            return;
        }
        else if (CurStage == i*4 + RandomStage)
        {
            Stages[CurStage].SetActive(false);
            ChooseStage[i].SetActive(true);
            RandomStage = Random.Range(0, 2);
        }
        else if(NextStage%4 == 0){
            BossStage[CurStage/4].SetActive(true);
            Stages[CurStage].SetActive(false);//0,1,2,3 -> 보스 -> 4,5,6,7-> 보스 니까 맞음
            stageIndex++;
            Player.transform.position = StartPosition;
            return;
        }
        else if (NextStage < Stages.Length)
        {
            Stages[CurStage].SetActive(false);
            Stages[NextStage].SetActive(true);
        }
        else
        {
            Time.timeScale = 0;
            Debug.Log("게임클리어!");
        }
        stageIndex++;
        Player.transform.position = StartPosition;
    }
    public void ToItemStage()
    {
        ItemStage[i].SetActive(true);
        ChooseStage[i++].SetActive(false);
        Player.transform.position = StartPosition;
    }
    public void ToSkillStage()
    {
        SkillStage[i].SetActive(true);
        ChooseStage[i++].SetActive(false);
        Player.transform.position = StartPosition;
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
        Player.transform.position = StartPosition;
        equipManager.EquipRune("BasicRune");
    }
    // }
    // void Start() { //나중에 필요없음
    //     statManager.Class = "Warrior";//이거 나중에 지우삼
    //     PM.SetClass();
    // }
}
