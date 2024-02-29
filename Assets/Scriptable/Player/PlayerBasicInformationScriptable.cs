using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [Header("Move Setting")]
    public KeyCode Jump = KeyCode.Space;
    public KeyCode WalkForward = KeyCode.W;
    public KeyCode WalkBackward = KeyCode.S;
    public KeyCode WalkLeft = KeyCode.A;
    public KeyCode WalkRight = KeyCode.D;

    [Header("Battle Setting")]
    public KeyCode Attack = KeyCode.Mouse0;
    public KeyCode Aim = KeyCode.Mouse1;
    public KeyCode E_Skill = KeyCode.E;
    public KeyCode Q_Skill = KeyCode.Q;
    #endregion

    #region MouseSensitivity
    [Header("MouseSensitivity")]
    public float MouseSensitivity = 800;
    #endregion

    #region Music
    [Header("GameMusic")]
    public float MovingMusic = 100;
    public float EnemyMusic = 100;
    public float BackgroundMusic = 100;
    #endregion

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
