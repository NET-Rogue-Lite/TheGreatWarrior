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
    public AudioManager audioManager;

    public int RandomStage;
    public GameObject[] ChooseStage;
    public GameObject[] SkillStage;
    public GameObject[] ItemStage;
    public GameObject[] BossStage;
    public GameObject Stage;
    public GameObject FinalStage;
    public int i;

    public bool IsItemStagePortal;
    public bool IsSkillStagePortal;
    public bool IsEventStagePortal;
    public bool IsBossClearPortal;
    public Vector2 StartPosition;
    // Start is called before the first frame update

    private void Awake()
    {
        StartPosition = new Vector2(25, 3);
        GameStart();
        hPManager.HP = 100;
        statManager.MaxHp = hPManager.HP * 2;
        hPManager.HP = 100;
        RandomStage = Random.Range(1, 2);
        i = 0;
        IsItemStagePortal = false;
        IsSkillStagePortal = false;
        IsEventStagePortal = false;
        IsBossClearPortal = false;
        for (int i = 0; i < 5; i++)
        {
            GameObject CurrnetStage = Stage.transform.GetChild(i).gameObject;
            if (i == 4)
            {
                for (int j = 0; j < 4; j++)
                {
                    GameObject TempStage = CurrnetStage.transform.GetChild(j).gameObject;
                    BossStage[j] = TempStage;
                }
            }
            else
            {
                for (int j = 0; j < 7; j++)
                {
                    GameObject TempStage = CurrnetStage.transform.GetChild(j).gameObject;
                    if (j == 0)
                    {
                        ItemStage[i] = TempStage;
                    }
                    else if (j == 1)
                    {
                        SkillStage[i] = TempStage;
                    }
                    else if (j == 2)
                    {
                        ChooseStage[i] = TempStage;
                    }
                    else
                    {
                        Stages[i * 4 + j - 3] = TempStage;
                    }
                }
            }
        }
        Stages[stageIndex].SetActive(true);
        audioManager.BGMSound("Stage1");
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
        ItemDestory();
        int CurStage = stageIndex;
        int NextStage = stageIndex + 1;
        BossStage[i == 0 ? 0 : i - 1].SetActive(false);
        if(IsBossClearPortal){
            Stages[CurStage].SetActive(true);
            return;
        }
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
            SkillStage[i - 1].SetActive(false);
            ItemStage[i - 1].SetActive(false);
            Stages[CurStage].SetActive(true);
            Player.transform.position = StartPosition;
            return;
        }
        else if (CurStage == i * 4 + RandomStage)
        {
            Stages[CurStage].SetActive(false);
            ChooseStage[i].SetActive(true);
            RandomStage = Random.Range(0, 2);
            Player.transform.position = StartPosition;
            return;
        }
        else if (NextStage % 4 == 0)
        {
            BossStage[CurStage / 4].SetActive(true);
            Stages[CurStage].SetActive(false);//0,1,2,3 -> 보스 -> 4,5,6,7-> 보스 니까 맞음
            stageIndex++;
            Player.transform.position = StartPosition;
            audioManager.BGMSound("StageBoss");
            return;
        }
        else if (NextStage < Stages.Length)
        {
            Stages[CurStage].SetActive(false);
            Stages[NextStage].SetActive(true);
        }
        else
        {
            BossStage[3].SetActive(false);
            if(FinalStage.gameObject.activeSelf == true){
                Player.transform.position = StartPosition;
                FinalStage.transform.GetChild(0).gameObject.SetActive(false);
                FinalStage.transform.GetChild(1).gameObject.SetActive(true);
                return;
            }
            FinalStage.SetActive(true);
            Player.transform.position = StartPosition;
        }
        stageIndex++;
        Player.transform.position = StartPosition;

        if (stageIndex >= 4 && stageIndex <= 7)
            audioManager.BGMSound("Stage2");
        else if (stageIndex >= 8 && stageIndex <= 11)
            audioManager.BGMSound("Stage3");
        else if (stageIndex >= 12 && stageIndex <= 15)
            audioManager.BGMSound("Stage4");
       
    }
    void ItemDestory(){
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Rune")){
            Destroy(obj);
        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Armor")){
            Destroy(obj);
        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Hat")){
            Destroy(obj);
        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Glove")){
            Destroy(obj);
        }
        foreach (GameObject obj in  GameObject.FindGameObjectsWithTag("1")){
            Destroy(obj);
        }
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
        Player.transform.position = StartPosition;
        equipManager.EquipRune("BasicRune");
    }
    // }
    // void Start() { //나중에 필요없음
    //     statManager.Class = "Warrior";//이거 나중에 지우삼
    //     PM.SetClass();
    // }
}