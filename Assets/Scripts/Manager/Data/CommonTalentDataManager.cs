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
        playerBasicInformation.Law.GetComponent<StudentDataManager>().studentData.HealthRate = PlayerPrefs.GetFloat("LawHealthRate");
        playerBasicInformation.Law.GetComponent<StudentDataManager>().studentData.AttackRate = PlayerPrefs.GetFloat("LawAttackRate");
        playerBasicInformation.Law.GetComponent<StudentDataManager>().studentData.WalkSpeedRate = PlayerPrefs.GetFloat("LawWalkSpeedRate");
        playerBasicInformation.Law.GetComponent<StudentDataManager>().studentData.DefenseRate = PlayerPrefs.GetFloat("LawDefenseRate");
        playerBasicInformation.Law.GetComponent<StudentDataManager>().studentData.Q_SkillDamageRate = PlayerPrefs.GetFloat("LawQ_SkillDamageRate");
        playerBasicInformation.Law.GetComponent<StudentDataManager>().studentData.E_SkillDamageRate = PlayerPrefs.GetFloat("LawE_SkillDamageRate");
        playerBasicInformation.Law.GetComponent<StudentDataManager>().studentData.rateSetting = true;

        playerBasicInformation.Chemistry.GetComponent<StudentDataManager>().studentData.HealthRate = PlayerPrefs.GetFloat("ChemistryHealthRate");
        playerBasicInformation.Chemistry.GetComponent<StudentDataManager>().studentData.AttackRate = PlayerPrefs.GetFloat("ChemistryAttackRate");
        playerBasicInformation.Chemistry.GetComponent<StudentDataManager>().studentData.WalkSpeedRate = PlayerPrefs.GetFloat("ChemistryWalkSpeedRate");
        playerBasicInformation.Chemistry.GetComponent<StudentDataManager>().studentData.DefenseRate = PlayerPrefs.GetFloat("ChemistryDefenseRate");
        playerBasicInformation.Chemistry.GetComponent<StudentDataManager>().studentData.Q_SkillDamageRate = PlayerPrefs.GetFloat("ChemistryQ_SkillDamageRate");
        playerBasicInformation.Chemistry.GetComponent<StudentDataManager>().studentData.E_SkillDamageRate = PlayerPrefs.GetFloat("ChemistryE_SkillDamageRate");
        playerBasicInformation.Chemistry.GetComponent<StudentDataManager>().studentData.rateSetting = true;

        playerBasicInformation.EE.GetComponent<StudentDataManager>().studentData.HealthRate = PlayerPrefs.GetFloat("EEHealthRate");
        playerBasicInformation.EE.GetComponent<StudentDataManager>().studentData.AttackRate = PlayerPrefs.GetFloat("EEAttackRate");
        playerBasicInformation.EE.GetComponent<StudentDataManager>().studentData.WalkSpeedRate = PlayerPrefs.GetFloat("EEWalkSpeedRate");
        playerBasicInformation.EE.GetComponent<StudentDataManager>().studentData.DefenseRate = PlayerPrefs.GetFloat("EEDefenseRate");
        playerBasicInformation.EE.GetComponent<StudentDataManager>().studentData.Q_SkillDamageRate = PlayerPrefs.GetFloat("EEQ_SkillDamageRate");
        playerBasicInformation.EE.GetComponent<StudentDataManager>().studentData.E_SkillDamageRate = PlayerPrefs.GetFloat("EEE_SkillDamageRate");
        playerBasicInformation.EE.GetComponent<StudentDataManager>().studentData.rateSetting = true;

        playerBasicInformation.Mechanic.GetComponent<StudentDataManager>().studentData.HealthRate = PlayerPrefs.GetFloat("MechanicHealthRate");
        playerBasicInformation.Mechanic.GetComponent<StudentDataManager>().studentData.AttackRate = PlayerPrefs.GetFloat("MechanicAttackRate");
        playerBasicInformation.Mechanic.GetComponent<StudentDataManager>().studentData.WalkSpeedRate = PlayerPrefs.GetFloat("MechanicWalkSpeedRate");
        playerBasicInformation.Mechanic.GetComponent<StudentDataManager>().studentData.DefenseRate = PlayerPrefs.GetFloat("MechanicDefenseRate");
        playerBasicInformation.Mechanic.GetComponent<StudentDataManager>().studentData.Q_SkillDamageRate = PlayerPrefs.GetFloat("MechanicQ_SkillDamageRate");
        playerBasicInformation.Mechanic.GetComponent<StudentDataManager>().studentData.E_SkillDamageRate = PlayerPrefs.GetFloat("MechanicE_SkillDamageRate");
        playerBasicInformation.Mechanic.GetComponent<StudentDataManager>().studentData.rateSetting = true;

        Catched.Invoke(true);
    }
}
