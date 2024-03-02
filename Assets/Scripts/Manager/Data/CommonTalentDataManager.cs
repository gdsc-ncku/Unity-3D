using System.Collections;
using UnityEngine;

public class CommonTalentDataManager : BasicDataManager
{
    void Start()
    {
        StartCoroutine(WaitForDataManager());
    }
    IEnumerator WaitForDataManager()
    {
        yield return new WaitUntil(() => dataManager.initData);
        playerBasicInformation.HealthRate = PlayerPrefs.GetFloat("HealthRate");
        playerBasicInformation.AttackRate = PlayerPrefs.GetFloat("AttackRate");
        playerBasicInformation.WalkSpeedRate = PlayerPrefs.GetFloat("WalkSpeedRate");
        playerBasicInformation.AttackSpeedRate = PlayerPrefs.GetFloat("AttackSpeedRate");
        playerBasicInformation.Q_SkillDamageRate = PlayerPrefs.GetFloat("Q_SkillDamageRate");
        playerBasicInformation.E_SkillDamageRate = PlayerPrefs.GetFloat("E_SkillDamageRate");

        Catched.Invoke(true);
    }
}
