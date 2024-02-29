using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBaseData : ScriptableObject
{
    public bool Unlocked = false;

    #region BasicData
    public float Health;
    public float Magic;
    public float MagicReply;
    public float AttackDamage;
    public float WalkSpeed;
    public float AttackSpeed;
    public float Q_SkillDamage;
    public ParticleSystem Q_Skill;
    public float E_SkillDamage;
    public ParticleSystem E_Skill;
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
}
