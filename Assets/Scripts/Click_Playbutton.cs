using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Click_Playbutton : MonoBehaviour
{
    public GameObject StartUI;
    public GameObject SkillSelectUI;
    public GameObject PassiveSkill;
    public GameObject Window;
    public GameObject WindowUpper;
    public GameObject Information;
    // public DiffControl Diff;
    public void ClassSelect()
    {
        StartUI.gameObject.SetActive(false);
        SkillSelectUI.gameObject.SetActive(true);
        Window.gameObject.SetActive(true);
        WindowUpper.gameObject.SetActive(true);
        // SceneManager.LoadScene("SampleScene");
        // DontDestroyOnLoad(Diff); 이제 static이라 괜찮음
    }
    public void GameStart()
    {
        SceneManager.LoadScene("Game_Scene");
        // DontDestroyOnLoad(Diff); 이제 static이라 괜찮음
    }
    public void ToNormal()
    {
        DiffControl.Diff = 1;
    }
    public void ToHard()
    {
        DiffControl.Diff = 2;
    }
    public void ToHell()
    {
        DiffControl.Diff = 4;
    }
    public void ToWarrior()
    {
        DiffControl.Class = "Warrior";
    }
    public void ToArcher()
    {
        DiffControl.Class = "Archer";
    }
    public void Quit()
    {
        Quit();
    }
    public void LevelUp(int i)
    {
        if (DiffControl.SkillPoint > 0)
        {
            if(DiffControl.SkillLevel[i] < 3){
                DiffControl.SkillLevel[i] += 1;
                DiffControl.SkillPoint -= 1;//SkillPoint 떨구는 코드.
            }
        }
    }
    public void Passive()
    {
        if (DiffControl.Passive == 1)
        { 
            DiffControl.Passive = 2;
            PassiveSkill.transform.GetChild(0).GetComponent<Text>().text = "Passive1 선택됨";
        }
        else
        {
            DiffControl.Passive = 1;
            PassiveSkill.transform.GetChild(0).GetComponent<Text>().text = "Passive2 선택됨";
        }

    }
    public void ShowInformation()
    {
        Information.transform.GetChild(1).gameObject.SetActive(true);
        Information.transform.GetChild(2).gameObject.SetActive(true);
        Information.transform.GetChild(3).gameObject.SetActive(true);
        Information.transform.GetChild(4).gameObject.SetActive(true);
        Information.transform.GetChild(2).gameObject.GetComponent<Text>().text = "1. 전사 (밸런스 형)\n"+
    
    "패시브 스킬1 : 공격력이 10 증가한다. Lv.none\n"+
    
    "패시브 스킬2 : 방어력이 50 증가한다. Lv.none\n"+
    
    "기본공격 : 상하 베기(100)% Lv.none\n"+
    "기본스킬 : 앞으로 찔러내는 검기를 소환해 다수의 적을 밀어내며 피해(200)%를 입힌다. (5s) Lv.1~3\n"+
    
    "\n스킬 1: 최대체력 50%의 쉴드를 얻고 영구적으로 최대체력이 1 증가한다. (10s) Lv.1~3\n\n"+
    
    "스킬 2: 검에 마력을 담아 앞으로 뿜어 다수의 적에게 피해(400%) 를 입힌다.(10s)Lv.1~3\n\n"+
    
    "스킬 3: 적 하나에게 큰 피해(1000)%를 입히는 검을 하늘에서 소환한다.(20s)Lv.1~3\n\n"+
    
    "스킬 4: 20초간 자신의 주변을 따라다니는 영역을 전개해 영역에 들어오는 적에게 1회 피해(100)%를 입히고, 영역에 적이 들어올 수록 공격력이 증가한다. 원거리공격을 반사한다.(100)%최대체력 100%의 쉴드를 얻는다. 공격력 증가량 (5) (60s)Lv.1~3\n"+
    
    "\n스킬 5: 잠시 무적이 되며 보이지 않는 속도로 적을 베어 다수의 적들에게 큰 피해(3000)%를 입힌다. 이후 최대체력 50%의 쉴드를 얻는다.(60s) // 화면 가르는 모션 Lv.1~3\n"+
    
    "\n스킬 6: 기본공격처럼 사용 가능하다. 큰 검이 나와서 휘두른다. (150)%(0.5s)Lv.1~3";
    }
    public void NextInformation(){
        Information.transform.GetChild(2).gameObject.GetComponent<Text>().text = "3. 궁수 (단일 공격 형)\n"+
    
    "패시브 스킬1 : 기본공격이 50%확률로 2발 나간다. Lv.none\n"+
    
    "패시브 스킬2 : 크리티컬 확률이 (30)%증가한다. (크리티컬 데미지는 200%) Lv.none\n"+
    
    "기본공격 : 화살쏘기(100)% Lv.none\n"+
    
    "기본스킬 : (50)% 피해의 화살을 5발 쏜다. (5s) Lv.1~3\n\n"+
    
    "스킬 1: 강력한 화살 한발을 발사한다. (800)% (10s)\n\n"+
    
    "스킬 2: 5초간 자동으로 화살을 발사하는 화살 발사대를 설치한다. 한 발당 (50)% (10s)Lv.1~3\n\n"+
    
    "스킬 3: 화살을 하늘위로 발사해 앞의 적들에게 화살을 맞춘다. 24x2발, 한발당(50)% (20s) Lv.1~3\n\n"+
    
    "스킬 4: 활을 앞으로 휘둘러 적들을 밀어 멀리 떨어트린다. (200)% (20s)Lv.1~3\n\n"+
    
    "스킬 5: 20초간 크리티컬확률이 100%가 되며 공격력이 30% 증가한다. (60s)// Lv.1~3\n\n"+
    
    "스킬 6: 기본공격처럼 사용 가능하다. 빠르게 화살을 연속 발사한다. (20)% (0.1s) Lv.1~3";
    }
    public void ShutInformation()
    {
        Information.transform.GetChild(1).gameObject.SetActive(false);
        Information.transform.GetChild(2).gameObject.SetActive(false);
        Information.transform.GetChild(3).gameObject.SetActive(false);
        Information.transform.GetChild(4).gameObject.SetActive(false);
    }
}
