using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Storage battle information
[CreateAssetMenu(fileName = "Player", menuName = "PlayerInformation/Player/PlayerBattleInformation", order = 2)]
public class PlayerBattleValueScriptable : ScriptableObject
{
    #region BasicBattleValue
    [Header("BasicBattleValue")]
    public float Health = 100;
    public float AttackDamage = 20;
    public float WalkSpeed = 10;
    public float JumpForce = 10;
    public float AttackSpeed = 10;
    public float SmallSkillDamage = 50;
    public float BigSkillDamage = 100;
    #endregion

    #region Scroll
    //Storage scrolls
    #endregion

    #region Awakening
    //Storage awakening
    #endregion
}
