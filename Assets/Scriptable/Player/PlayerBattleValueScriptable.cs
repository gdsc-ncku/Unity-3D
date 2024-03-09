using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Storage battle information
[CreateAssetMenu(fileName = "PlayerBattleInformation", menuName = "PlayerInformation/Player/PlayerBattleInformation", order = 2)]
public class PlayerBattleValueScriptable : ScriptableObject
{
    #region BasicBattleValue
    [Header("BasicBattleValue")]
    public CharacterBaseData Role;
    #endregion

    #region Scroll
    //Storage scrolls
    #endregion

    #region Awakening
    //Storage awakening
    #endregion
}
