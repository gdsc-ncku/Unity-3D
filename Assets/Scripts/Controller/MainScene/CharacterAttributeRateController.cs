using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum whichRate { Health, Defense, AttackRate, WalkSpeed, MainDamage, SecondDamage };
public class CharacterAttributeRateController : MonoBehaviour
{
    [SerializeField] GameObject department, minus;
    [SerializeField] CharacterAttributeDisplay display;
    [SerializeField] whichRate rate;
    private float originRate, nowRate;
    private CharacterBaseData characterBaseData;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if(department != null)
        {
            characterBaseData = department.GetComponent<StudentDataManager>().studentData;
        }
        else
        {
            Debug.LogWarning("Bug");
            return;
        }

        if (rate == whichRate.Health)
        {
            originRate = characterBaseData.HealthRate;
        }
        else if (rate == whichRate.Defense)
        {
            originRate = characterBaseData.Defense;
        }
        else if (rate == whichRate.AttackRate)
        {
            originRate = characterBaseData.AttackRate;
        }
        else if (rate == whichRate.WalkSpeed)
        {
            originRate = characterBaseData.WalkSpeedRate;
        }
        else if (rate == whichRate.MainDamage)
        {
            originRate = characterBaseData.Q_SkillDamageRate;
        }
        else if (rate == whichRate.SecondDamage)
        {
            originRate = characterBaseData.E_SkillDamageRate;
        }
        nowRate = originRate;
    }

    private void Update()
    {
        if(nowRate > originRate)
        {
            minus.SetActive(true);
        }
        else
        {
            minus.SetActive(false);
        }
    }

    public void Plus()
    {
        if (rate == whichRate.Health)
        {
            nowRate++;
            characterBaseData.HealthRate += 0.1f;
        }
        else if (rate == whichRate.Defense)
        {
            nowRate++;
            characterBaseData.DefenseRate += 0.1f;
        }
        else if (rate == whichRate.AttackRate)
        {
            nowRate++;
            characterBaseData.AttackRate += 0.1f;
        }
        else if (rate == whichRate.WalkSpeed)
        {
            nowRate++;
            characterBaseData.WalkSpeedRate += 0.1f;
        }
        else if (rate == whichRate.MainDamage)
        {
            nowRate++;
            characterBaseData.Q_SkillDamageRate += 0.1f;
        }
        else if (rate == whichRate.SecondDamage)
        {
            nowRate++;
            characterBaseData.E_SkillDamageRate += 0.1f;
        }
        display.UpdateDisplay();
    }

    public void Minus()
    {
        if(nowRate <= originRate)
        {
            return;
        }

        if (rate == whichRate.Health)
        {
            nowRate--;
            characterBaseData.HealthRate -= 0.1f;
        }
        else if (rate == whichRate.Defense)
        {
            nowRate--;
            characterBaseData.DefenseRate -= 0.1f;
        }
        else if (rate == whichRate.AttackRate)
        {
            nowRate--;
            characterBaseData.AttackRate -= 0.1f;
        }
        else if (rate == whichRate.WalkSpeed)
        {
            nowRate--;
            characterBaseData.WalkSpeedRate -= 0.1f;
        }
        else if (rate == whichRate.MainDamage)
        {
            nowRate--;
            characterBaseData.Q_SkillDamageRate -= 0.1f;
        }
        else if (rate == whichRate.SecondDamage)
        {
            nowRate--;
            characterBaseData.E_SkillDamageRate -= 0.1f;
        }
        display.UpdateDisplay();
    }
}
