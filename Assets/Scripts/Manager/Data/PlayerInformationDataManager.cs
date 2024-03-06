using System.Collections;
using UnityEngine;

public class PlayerInformationDataManager : BasicDataManager
{
    void Start()
    {
        StartCoroutine(WaitForDataManager());
    }

    IEnumerator WaitForDataManager()
    {
        yield return new WaitUntil(() => dataManager.initData);
        playerBasicInformation.Name = PlayerPrefs.GetString("Name");
        playerBasicInformation.Level = PlayerPrefs.GetInt("Level");
        playerBasicInformation.Soul = PlayerPrefs.GetInt("Soul");

        Catched.Invoke(true);
    }
}
