using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class keyboard
{
    public String key;
    public String display;
    public TextMeshProUGUI value;
}

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
    public PlayerControl playerControl = null;
    [Header("Battle Setting")]
    private KeyCode attack = KeyCode.Mouse0;
    public KeyCode Attack
    {
        get => attack;
        set
        {
            attack = value;
        }
    }

    private KeyCode aim = KeyCode.Mouse1;
    public KeyCode Aim
    {
        get => aim;
        set
        {
            aim = value;
        }
    }

    private KeyCode e_Skill = KeyCode.E;
    public KeyCode E_Skill
    {
        get => e_Skill;
        set
        {
            e_Skill = value;
        }
    }

    private KeyCode q_Skill = KeyCode.Q;
    public KeyCode Q_Skill
    {
        get => q_Skill;
        set
        {
            q_Skill = value;
        }
    }

    private KeyCode pickUp = KeyCode.R;
    public KeyCode PickUp
    {
        get => pickUp;
        set
        {
            pickUp = value;
        }
    }
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
