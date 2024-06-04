using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DataManager : MonoBehaviour
{
    [SerializeField] GameStatus gameStatus;
    [SerializeField] CommonTalentDataManager commonTalentDataManager;
    [SerializeField] HeroDataManager heroDataManager;
    [SerializeField] PlayerInformationDataManager playerInformationDataManager;
    [SerializeField] SettingDataManager settingDataManager;
    PlayerControl playerControl;

    public bool initData = false;
    private bool TalentData = false, HeroData = false, PlayerData = false, SettingData = false;
    private void Awake()
    {
        playerControl = new PlayerControl();
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerPrefs.HasKey("IsFirstTime"));
        //If player firstly open game, init some base player information in PlayerPrefs System;
        if (!PlayerPrefs.HasKey("IsFirstTime"))
        {
            //Setting the Key that represent the game has already inited the basic informations. 
            PlayerPrefs.SetInt("IsFirstTime", 1);

            PlayerPrefs.SetString("Name", "Admin");
            PlayerPrefs.SetInt("Level", 1);
            PlayerPrefs.SetInt("Soul", 0);

            PlayerPrefs.SetInt("Width", 1920);
            PlayerPrefs.SetInt("Height", 1080);

            PlayerPrefs.SetString("Rebinds", playerControl.SaveBindingOverridesAsJson());
            PlayerPrefs.SetString(KeyIndex.Weapon1.ToString(), "1");
            PlayerPrefs.SetString(KeyIndex.Weapon2.ToString(), "2");
            PlayerPrefs.SetString(KeyIndex.Q_Skill.ToString(), "Q");
            PlayerPrefs.SetString(KeyIndex.E_Skill.ToString(), "E");
            PlayerPrefs.SetString(KeyIndex.Backpack.ToString(), "Tab");
            PlayerPrefs.SetString(KeyIndex.Reload.ToString(), "R");
            PlayerPrefs.SetString(KeyIndex.WeaponSkill.ToString(), "R Click");
            PlayerPrefs.SetString(KeyIndex.Forward.ToString(), "W");
            PlayerPrefs.SetString(KeyIndex.Backward.ToString(), "S");
            PlayerPrefs.SetString(KeyIndex.Left.ToString(), "A");
            PlayerPrefs.SetString(KeyIndex.Right.ToString(), "D");
            PlayerPrefs.SetString(KeyIndex.Jump.ToString(), "Space");
            PlayerPrefs.SetString(KeyIndex.Fire.ToString(), "L Click");
            PlayerPrefs.SetString(KeyIndex.PickUp.ToString(), "F");

            PlayerPrefs.SetFloat("MouseSensitivity", 800);

            PlayerPrefs.SetFloat("MovingMusic", 100);
            PlayerPrefs.SetFloat("EnemyMusic", 100);
            PlayerPrefs.SetFloat("BackgroundMusic", 100);

            PlayerPrefs.SetInt("LawStudent", 1);

            PlayerPrefs.SetFloat("LawHealthRate", 1f);
            PlayerPrefs.SetFloat("LawAttackRate", 1);
            PlayerPrefs.SetFloat("LawWalkSpeedRate", 1.6f);
            PlayerPrefs.SetFloat("LawDefenseRate", 1.4f);
            PlayerPrefs.SetFloat("LawQ_SkillDamageRate", 1);
            PlayerPrefs.SetFloat("LawE_SkillDamageRate", 1);

            PlayerPrefs.SetFloat("ChemistryHealthRate", 1f);
            PlayerPrefs.SetFloat("ChemistryAttackRate", 1f);
            PlayerPrefs.SetFloat("ChemistryWalkSpeedRate", 1.4f);
            PlayerPrefs.SetFloat("ChemistryDefenseRate", 1f);
            PlayerPrefs.SetFloat("ChemistryQ_SkillDamageRate", 1.3f);
            PlayerPrefs.SetFloat("ChemistryE_SkillDamageRate", 1.3f);

            PlayerPrefs.SetFloat("EEHealthRate", 1.9f);
            PlayerPrefs.SetFloat("EEAttackRate", 1.1f);
            PlayerPrefs.SetFloat("EEWalkSpeedRate", 1f);
            PlayerPrefs.SetFloat("EEDefenseRate", 1f);
            PlayerPrefs.SetFloat("EEQ_SkillDamageRate", 1);
            PlayerPrefs.SetFloat("EEE_SkillDamageRate", 1);

            PlayerPrefs.SetFloat("MechanicHealthRate", 1.3f);
            PlayerPrefs.SetFloat("MechanicAttackRate", 1.2f);
            PlayerPrefs.SetFloat("MechanicWalkSpeedRate", 1);
            PlayerPrefs.SetFloat("MechanicDefenseRate", 1.5f);
            PlayerPrefs.SetFloat("MechanicQ_SkillDamageRate", 1);
            PlayerPrefs.SetFloat("MechanicE_SkillDamageRate", 1);

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
