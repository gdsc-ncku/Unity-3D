using System.Collections;
using System.Collections.Generic;
using UnityEditor.MPE;
using UnityEngine;

public enum Character
{
    Law
}
//Storage battle information
[CreateAssetMenu(fileName = "PlayerBattleInformation", menuName = "PlayerInformation/Player/PlayerBattleInformation", order = 2)]
public class PlayerBattleValueScriptable : ScriptableObject
{
    #region BasicBattleValue
    [Header("BasicBattleValue")]
    public Character Role = Character.Law;
    public float Health = 100;
    public float Magic = 300;
    public float AttackDamage = 20;
    public float WalkSpeed = 10;
    public float JumpForce = 10;
    public float AttackSpeed = 2;
    public float ReloadingSpeed = 2;
    public float Q_SkillDamage = 50;
    public ParticleSystem Q_Skill = null;
    public float E_SkillDamage = 100;
    public ParticleSystem E_Skill = null;
    #endregion

    #region Scroll
    //Storage scrolls
    #endregion

    #region Awakening
    //Storage awakening
    #endregion
}
