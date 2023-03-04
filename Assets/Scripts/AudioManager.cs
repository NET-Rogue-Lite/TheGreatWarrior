using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] audioSources;
    public AudioSource BGMSources;

    public AudioClip[] monsterAudios;
    public AudioClip[] boss1Audios;
    public AudioClip[] boss2Audios;
    public AudioClip[] boss3Audios;
    public AudioClip[] boss4Audios;
    public AudioClip[] playerAudios;
    public AudioClip[] skillAudios;
    public AudioClip itemAudio;

    public AudioClip[] BGMAudios;

    int i;

    void Start()
    {
        i = 0;
    }

    public void playerSound(string action)
    {
        switch (action)
        {
            case "Attack":
                audioSources[i].clip = playerAudios[0];
                break;
            case "Dash":
                audioSources[i].clip = playerAudios[1];
                break;
            case "Jump":
                audioSources[i].clip = playerAudios[2];
                break;
        }
        audioSources[i].Play();
        i = (i + 1) % 10;
    }

    public void monsterSound(string action)
    {
        switch (action)
        {
            case "Damaged":
                audioSources[i].clip = monsterAudios[0];
                break;
            case "Bat":
                audioSources[i].clip = monsterAudios[1];
                break;
            case "Bear":
                audioSources[i].clip = monsterAudios[2];
                break;
            case "Skull":
                audioSources[i].clip = monsterAudios[3];
                break;
            case "Golem":
                audioSources[i].clip = monsterAudios[4];
                break;
            case "Rat":
                audioSources[i].clip = monsterAudios[5];
                break;
            case "Slime":
                audioSources[i].clip = monsterAudios[6];
                break;
            case "GreenPig":
                audioSources[i].clip = monsterAudios[7];
                break;
            case "Chicken":
                audioSources[i].clip = monsterAudios[8];
                break;
            case "Eagle":
                audioSources[i].clip = monsterAudios[9];
                break;
            case "Turtle":
                audioSources[i].clip = monsterAudios[10];
                break;
            case "Boss1_attack":
                audioSources[i].clip = monsterAudios[11];
                break;
            case "Boss1":
                audioSources[i].clip = monsterAudios[12];
                break;
            case "Boss2_attack":
                audioSources[i].clip = monsterAudios[13];
                break;
            case "Boss2":
                audioSources[i].clip = monsterAudios[14];
                break;
            case "Boss3_attack":
                audioSources[i].clip = monsterAudios[15];
                break;
            case "Boss3":
                audioSources[i].clip = monsterAudios[16];
                break;
        }
        audioSources[i].Play();
        i = (i + 1) % 10;
    }

    public void skillSound(string action)
    {
   
        switch (action)
        {
            case "WarriorSkill0":
                audioSources[i].clip = skillAudios[0];
                break;
            case "WarriorSkill1":
                audioSources[i].clip = skillAudios[1];
                break;
            case "WarriorSkill2":
                audioSources[i].clip = skillAudios[2];
                break;
            case "WarriorSkill3":
                audioSources[i].clip = skillAudios[3];
                break;
            case "WarriorSkill4":
                audioSources[i].clip = skillAudios[4];
                break;
            case "WarriorSkill5":
                audioSources[i].clip = skillAudios[5];
                break;
            case "WarriorSkill6":
                audioSources[i].clip = skillAudios[6];
                break;

            case "ArcherSkill0":
                audioSources[i].clip = skillAudios[7];
                break;
            case "ArcherSkill1":
                audioSources[i].clip = skillAudios[8];
                break;
            case "ArcherSkill2":
                audioSources[i].clip = skillAudios[9];
                break;
            case "ArcherSkill3":
                audioSources[i].clip = skillAudios[10];
                break;
            case "ArcherSkill4":
                audioSources[i].clip = skillAudios[11];
                break;
            case "ArcherSkill5":
                audioSources[i].clip = skillAudios[12];
                break;
            case "ArcherSkill6":
                audioSources[i].clip = skillAudios[13];
                break;

        }
        audioSources[i].Play();
        i = (i + 1) % 10;
    }

    public void boss1Sound(string action)
    {
        switch (action)
        {
            case "Damaged":
                audioSources[i].clip = boss1Audios[0];
                break;
            case "Ball":
                audioSources[i].clip = boss1Audios[1];
                break;
            case "Tornado":
                audioSources[i].clip = boss1Audios[2];
                break;
            case "Die":
                audioSources[i].clip = boss1Audios[3];
                break;
        }
        audioSources[i].Play();
        i = (i + 1) % 10;
    }
    public void boss2Sound(string action)
    {
        switch (action)
        {
            case "Damaged":
                audioSources[i].clip = boss2Audios[0];
                break;
            case "Fireball":
                audioSources[i].clip = boss2Audios[1];
                break;
            case "Floor":
                audioSources[i].clip = boss2Audios[2];
                break;
            case "Die":
                audioSources[i].clip = boss2Audios[3];
                break;
        }
        audioSources[i].Play();
        i = (i + 1) % 10;
    }
    public void boss3Sound(string action)
    {
        switch (action)
        {
            case "Damaged":
                audioSources[i].clip = boss3Audios[0];
                break;
            case "Iceball":
                audioSources[i].clip = boss3Audios[1];
                break;
            case "Icefall":
                audioSources[i].clip = boss3Audios[2];
                break;
            case "Icefloor":
                audioSources[i].clip = boss3Audios[3];
                break;
            case "Push":
                audioSources[i].clip = boss3Audios[4];
                break;
            case "Die":
                audioSources[i].clip = boss3Audios[5];
                break;
        }
        audioSources[i].Play();
        i = (i + 1) % 10;
    }
    public void boss4Sound(string action)
    {
        switch (action)
        {
            case "Damaged":
                audioSources[i].clip = boss4Audios[0];
                break;
            case "Bite":
                audioSources[i].clip = boss4Audios[1];
                break;
            case "Fire":
                audioSources[i].clip = boss4Audios[2];
                break;
            case "Thunder":
                audioSources[i].clip = boss4Audios[3];
                break;
            case "Wing":
                audioSources[i].clip = boss4Audios[4];
                break;
            case "Die":
                audioSources[i].clip = boss4Audios[5];
                break;
        }
        audioSources[i].Play();
        i = (i + 1) % 10;
    }

    public void finalBossSound(string action)
    {

    }

    public void itemSound()
    {
        audioSources[i].clip = itemAudio;
        audioSources[i].Play();
        i = (i + 1) % 10;
    }

    public void BGMSound(string action)
    {
        switch (action)
        {
            case "Stage1":
                BGMSources.clip = BGMAudios[0];
                break;
            case "Stage2":
                BGMSources.clip = BGMAudios[1];
                break;
            case "Stage3":
                BGMSources.clip = BGMAudios[2];
                break;
            case "Stage4":
                BGMSources.clip = BGMAudios[3];
                break;
            case "Boss":
                BGMSources.clip = BGMAudios[4];
                break;
            case "StageBoss":
                BGMSources.clip = BGMAudios[5];
                break;
        }
        BGMSources.Play();
    }

    
}
