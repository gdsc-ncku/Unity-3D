using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

        playerBasicInformation.Jump = (KeyCode)PlayerPrefs.GetInt("Jump");
        playerBasicInformation.WalkForward = (KeyCode)PlayerPrefs.GetInt("WalkForward");
        playerBasicInformation.WalkBackward = (KeyCode)PlayerPrefs.GetInt("WalkBackward");
        playerBasicInformation.WalkLeft = (KeyCode)PlayerPrefs.GetInt("WalkLeft");
        playerBasicInformation.WalkRight = (KeyCode)PlayerPrefs.GetInt("WalkRight");

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
