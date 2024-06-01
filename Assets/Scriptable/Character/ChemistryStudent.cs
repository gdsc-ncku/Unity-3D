using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChemistryStudent", menuName = "Character/ChemistryStudent")]
public class ChemistryStudent : CharacterBaseData
{
    [SerializeField] private float healthRate;
    [SerializeField] private float attackRate;
    [SerializeField] private float walkSpeedRate;
    [SerializeField] private float defenseRate;
    [SerializeField] private float q_SkillDamageRate;
    [SerializeField] private float e_SkillDamageRate;
    public new float HealthRate
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
    public new float AttackRate
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
    public new float WalkSpeedRate
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
    public new float DefenseRate
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
    public new float Q_SkillDamageRate
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
    public new float E_SkillDamageRate
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
    public new void UseingQ_Skill()
    {

    }

    public new void UseingE_Skill()
    {

    }
}
