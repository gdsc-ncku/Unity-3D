using System;
using TMPro;
using UnityEngine;

[Serializable]
public class keyboard
{
    public String key;
    public String display;
    public TextMeshProUGUI value;
}

public enum KeyIndex { Weapon1, Weapon2, Q_Skill, E_Skill, Backpack, Reload, WeaponSkill, Forward, Backward, Left, Right, Jump, Fire, PickUp };

//Storage inheritable information
[CreateAssetMenu(fileName = "PlayerBasicInformation", menuName = "PlayerInformation/Player/PlayerBasicInformation", order = 1)]
public class PlayerBasicInformationScriptable : ScriptableObject
{
    #region PlayerInformation
    [Header("PlayerInformation")]
    public string Name = "Admin";
    public float Level = 1;
    public float Soul = 0;
    #endregion

    #region ScreenResolution
    [Header("ScreenResolution")]
    public float Width = 1920;
    public float Height = 1080;
    #endregion

    #region ButtonSetting
    //Input System
    public PlayerControl playerControl = null;
    #endregion

    #region MouseSensitivity
    [Header("MouseSensitivity")]
    public float MouseDPI = 800;
    public float edpi = 800;
    #endregion

    #region Music
    [Header("GameMusic")]
    public float MovingMusic = 100;
    public float EnemyMusic = 100;
    public float BackgroundMusic = 100;
    #endregion

    //Just record what character we have
    #region Character
    public LawStudent Law;
    #endregion

    #region Talent
    public float HealthRate = 1;
    public float AttackRate = 1;
    public float WalkSpeedRate = 1;
    public float AttackSpeedRate = 1;
    public float Q_SkillDamageRate = 1;
    public float E_SkillDamageRate = 1;
    #endregion
}
