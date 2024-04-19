using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SettingDataManager : BasicDataManager
{
    [SerializeField] PlayerBasicInformationScriptable playerBasicInformationScriptable;
    PlayerControl playerControl;
    void Start()
    {
        playerControl = new PlayerControl();
        StartCoroutine(WaitForDataManager());
    }

    IEnumerator WaitForDataManager()
    {
        yield return new WaitUntil(() => dataManager.initData);
        playerBasicInformation.Width = PlayerPrefs.GetInt("Width");
        playerBasicInformation.Height = PlayerPrefs.GetInt("Height");

        playerBasicInformationScriptable.playerControl = playerControl;
        playerControl.LoadBindingOverridesFromJson(PlayerPrefs.GetString("Rebinds"));

        playerBasicInformation.Attack = (KeyCode)PlayerPrefs.GetInt("Attack");
        playerBasicInformation.Aim = (KeyCode)PlayerPrefs.GetInt("Aim");
        playerBasicInformation.E_Skill = (KeyCode)PlayerPrefs.GetInt("E_Skill");
        playerBasicInformation.Q_Skill = (KeyCode)PlayerPrefs.GetInt("Q_Skill");

        playerBasicInformation.MouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");

        playerBasicInformation.MovingMusic = PlayerPrefs.GetFloat("MovingMusic");
        playerBasicInformation.EnemyMusic = PlayerPrefs.GetFloat("EnemyMusic");
        playerBasicInformation.BackgroundMusic = PlayerPrefs.GetFloat("BackgroundMusic");

        Catched.Invoke(true);
    }
}
