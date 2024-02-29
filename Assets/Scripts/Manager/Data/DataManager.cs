using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] GameStatus gameStatus;
    [SerializeField] CommonTalentDataManager commonTalentDataManager;
    [SerializeField] HeroDataManager heroDataManager;
    [SerializeField] PlayerInformationDataManager playerInformationDataManager;
    [SerializeField] SettingDataManager settingDataManager;

    public bool initData = false;
    private bool TalentData = false, HeroData = false, PlayerData = false, SettingData = false;
    // Start is called before the first frame update
    void Start()
    {
        //If player firstly open game, init some base player information in PlayerPrefs System;
        if(!PlayerPrefs.HasKey("IsFirstTime"))
        {
            //Setting the Key that represent the game has already inited the basic informations. 
            PlayerPrefs.SetInt("IsFirstTime", 1);

            PlayerPrefs.SetString("Name", "Admin");
            PlayerPrefs.SetInt("Level", 1);
            PlayerPrefs.SetInt("Soul", 0);

            PlayerPrefs.SetInt("Width", 1920);
            PlayerPrefs.SetInt("Height", 1080);

            PlayerPrefs.SetInt("Jump", (int)KeyCode.Space);
            PlayerPrefs.SetInt("WalkForward", (int)KeyCode.W);
            PlayerPrefs.SetInt("WalkBackward", (int)KeyCode.S);
            PlayerPrefs.SetInt("WalkLeft", (int)KeyCode.A);
            PlayerPrefs.SetInt("WalkRight", (int)KeyCode.D);

            PlayerPrefs.SetInt("Attack", (int)KeyCode.Mouse0);
            PlayerPrefs.SetInt("Aim", (int)KeyCode.Mouse1);
            PlayerPrefs.SetInt("E_Skill", (int)KeyCode.E);
            PlayerPrefs.SetInt("Q_Skill", (int)KeyCode.Q);

            PlayerPrefs.SetFloat("MouseSensitivity", 800);

            PlayerPrefs.SetFloat("MovingMusic", 100);
            PlayerPrefs.SetFloat("EnemyMusic", 100);
            PlayerPrefs.SetFloat("BackgroundMusic", 100);

            PlayerPrefs.SetInt("LawStudent", 1);

            PlayerPrefs.SetFloat("HealthRate", 1);
            PlayerPrefs.SetFloat("AttackRate", 1);
            PlayerPrefs.SetFloat("WalkSpeedRate", 1);
            PlayerPrefs.SetFloat("AttackSpeedRate", 1);
            PlayerPrefs.SetFloat("Q_SkillDamageRate", 1);
            PlayerPrefs.SetFloat("E_SkillDamageRate", 1);

            PlayerPrefs.Save();
        }

        commonTalentDataManager.Catched.AddListener((bool Catched) => FinishTalentData());
        heroDataManager.Catched.AddListener((bool Catched) => FinishHeroData());
        playerInformationDataManager.Catched.AddListener((bool Catched) => FinishPlayerData());
        settingDataManager.Catched.AddListener((bool Catched) => FinishSettingData());

        initData = true;
        StartCoroutine(WaitForAllDataLoading());
    }

    public void FinishTalentData()
    {
        TalentData = true;
    }

    public void FinishHeroData()
    {
        HeroData = true;
    }

    public void FinishPlayerData()
    {
        PlayerData = true;
    }

    public void FinishSettingData()
    {
        SettingData = true;
    }
    IEnumerator WaitForAllDataLoading()
    {
        yield return new WaitUntil(() => TalentData && HeroData && PlayerData && SettingData);
        gameStatus.CatchData = true;
    }
}
