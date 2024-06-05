using System;
using UnityEngine;

public class CharacterBaseData : ScriptableObject
{
    public bool Unlocked = false;

    #region BasicData
    public float Health;
    public float Defense;
    public float AttackDamage;
    public float WalkSpeed;
    public float JumpForce;
    public float Q_SkillDamage;
    public ParticleSystem Q_Skill;
    public float E_SkillDamage;
    public ParticleSystem E_Skill;
    [NonSerialized] protected float healthRate;
    [NonSerialized] protected float attackRate;
    [NonSerialized] protected float walkSpeedRate;
    [NonSerialized] protected float defenseRate;
    [NonSerialized] protected float q_SkillDamageRate;
    [NonSerialized] protected float e_SkillDamageRate;
    public virtual float HealthRate
    {
        get
        {
            Debug.Log("Bug");
            return 0;
        }
        set
        {
            Debug.Log("Bug");
            return;
        }
    }
    public virtual float AttackRate
    {
        get
        {
            Debug.Log("Bug");
            return 0;
        }
        set
        {
            Debug.Log("Bug");
            return;
        }
    }
    public virtual float WalkSpeedRate
    {
        get
        {
            Debug.Log("Bug");
            return 0;
        }
        set
        {
            Debug.Log("Bug");
            return;
        }
    }
    public virtual float DefenseRate
    {
        get
        {
            Debug.Log("Bug");
            return 0;
        }
        set
        {
            defenseRate = value;
        }
    }
    public virtual float Q_SkillDamageRate
    {
        get
        {
            Debug.Log("Bug");
            return 0;
        }
        set
        {
            Debug.Log("Bug");
            return;
        }
    }
    public virtual  float E_SkillDamageRate
    {
        get
        {
            Debug.Log("Bug");
            return 0;
        }
        set
        {
            Debug.Log("Bug");
            return;
        }
    }
    #endregion

    //Hero_Q_Skill and Hero_E_Skill are the basic strengthening direction for every hero and you can define 
    //other specific strengthening method in Hero exclusive scriptableObject such as "LawStudent"
    #region Hero_Q_Skill
    public float Q_MagicConsumption = 1;
    public float Q_DamageRate = 1;
    public float Q_SkillRange = 1;
    #endregion

    #region Hero_E_Skill
    public float E_MagicConsumption = 1;
    public float E_DamageRate = 1;
    public float E_SkillRange = 1;
    #endregion
    public void UseingQ_Skill()
    {

    }

    public void UseingE_Skill()
    {

    }

    protected void OnDisable()
    {
        
    }
}
