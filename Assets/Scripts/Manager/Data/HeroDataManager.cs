using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeroDataManager : BasicDataManager
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForDataManager());
    }

    IEnumerator WaitForDataManager()
    {
        yield return new WaitUntil(() => dataManager.initData);
        playerBasicInformation.Law.Unlocked = PlayerPrefs.HasKey("LawStudent");

        Catched.Invoke(true);
    }
}
