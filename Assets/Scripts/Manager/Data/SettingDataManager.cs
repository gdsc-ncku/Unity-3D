using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SettingDataManager : BasicDataManager
{
    void Start()
    {
        StartCoroutine(WaitForDataManager());
    }

    IEnumerator WaitForDataManager()
    {
        yield return new WaitUntil(() => dataManager.initData);
        playerBasicInformation.Width = PlayerPrefs.GetInt("Width");
        playerBasicInformation.Height = PlayerPrefs.GetInt("Height");

        playerBasicInformation.playerControl = new PlayerControl();
        if (playerBasicInformation.playerControl != null)
        {
            Debug.Log("playerControl Setting Complete");
        }
        else
        {
            Debug.Log("playerControl Setting miss");
        }
        playerBasicInformation.playerControl.LoadBindingOverridesFromJson(PlayerPrefs.GetString("Rebinds"));

        playerBasicInformation.edpi = PlayerPrefs.GetFloat("MouseSensitivity");

        playerBasicInformation.MovingMusic = PlayerPrefs.GetFloat("MovingMusic");
        playerBasicInformation.EnemyMusic = PlayerPrefs.GetFloat("EnemyMusic");
        playerBasicInformation.BackgroundMusic = PlayerPrefs.GetFloat("BackgroundMusic");

        Catched.Invoke(true);
    }
}
