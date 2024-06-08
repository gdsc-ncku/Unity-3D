using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "ChemistryStudent", menuName = "Character/ChemistryStudent")]
public class ChemistryStudent : CharacterBaseData
{
    public override float HealthRate
    {
        get
        {
            return healthRate;
        }
        set
        {
            healthRate = value;
            PlayerPrefs.SetFloat("ChemistryHealthRate", value);
        }
    }
    public override float AttackRate
    {
        get
        {
            return attackRate;
        }
        set
        {
            attackRate = value;
            PlayerPrefs.SetFloat("ChemistryAttackRate", value);
        }
    }
    public override float WalkSpeedRate
    {
        get
        {
            return walkSpeedRate;
        }
        set
        {
            walkSpeedRate = value;
            PlayerPrefs.SetFloat("ChemistryWalkSpeedRate", value);
        }
    }
    public override float DefenseRate
    {
        get
        {
            return defenseRate;
        }
        set
        {
            defenseRate = value;
            PlayerPrefs.SetFloat("ChemistryDefenseRate", value);
        }
    }
    public override float Q_SkillDamageRate
    {
        get
        {
            return q_SkillDamageRate;
        }
        set
        {
            q_SkillDamageRate = value;
            PlayerPrefs.SetFloat("ChemistryQ_SkillDamageRate", value);
        }
    }
    public override float E_SkillDamageRate
    {
        get
        {
            return e_SkillDamageRate;
        }
        set
        {
            e_SkillDamageRate = value;
            PlayerPrefs.SetFloat("ChemistryE_SkillDamageRate", value);
        }
    }
    //You can Create specific strengthening below.
    #region Hero_Q_Skill
    #endregion

    #region Hero_E_Skill
    #endregion

    //Hero skills effect need to write in the functions below.
    public override void UseingQ_Skill()
    {
        // instantiate object to throw
        GameObject projectile = Instantiate(Q_Skill, Camera.main.transform.position + Camera.main.transform.forward * 1f, Quaternion.identity);

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        projectileRb.AddForce(Camera.main.transform.forward * 30f, ForceMode.Impulse);
    }

    public override void UseingE_Skill()
    {
        // instantiate object to throw
        GameObject projectile = Instantiate(E_Skill, playerBattleInfo.Player.transform.position + 2 * playerBattleInfo.Player.transform.forward, Quaternion.identity);

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        projectileRb.AddForce(Camera.main.transform.forward * 30f, ForceMode.Impulse);
    }

    private void OnEnable()
    {
        HealthRate = PlayerPrefs.GetFloat("ChemistryHealthRate");
        AttackRate = PlayerPrefs.GetFloat("ChemistryAttackRate");
        WalkSpeedRate = PlayerPrefs.GetFloat("ChemistryWalkSpeedRate");
        DefenseRate = PlayerPrefs.GetFloat("ChemistryDefenseRate");
        Q_SkillDamageRate = PlayerPrefs.GetFloat("ChemistryQ_SkillDamageRate");
        E_SkillDamageRate = PlayerPrefs.GetFloat("ChemistryE_SkillDamageRate");
    }
}
