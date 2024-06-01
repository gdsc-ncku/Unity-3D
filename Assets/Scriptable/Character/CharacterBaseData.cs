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
    protected float healthRate;
    protected float attackRate;
    protected float walkSpeedRate;
    protected float defenseRate;
    protected float q_SkillDamageRate;
    protected float e_SkillDamageRate;
    public virtual float HealthRate
    {
        get
        {
            return healthRate;
        }
        set
        {
            healthRate = value;
        }
    }
    public virtual float AttackRate
    {
        get
        {
            return attackRate;
        }
        set
        {
            attackRate = value;
        }
    }
    public virtual float WalkSpeedRate
    {
        get
        {
            return walkSpeedRate;
        }
        set
        {
            walkSpeedRate = value;
        }
    }
    public virtual float DefenseRate
    {
        get
        {
            return defenseRate;
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
            return q_SkillDamageRate;
        }
        set
        {
            q_SkillDamageRate = value;
        }
    }
    public virtual  float E_SkillDamageRate
    {
        get
        {
            return e_SkillDamageRate;
        }
        set
        {
            e_SkillDamageRate = value;
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
}
