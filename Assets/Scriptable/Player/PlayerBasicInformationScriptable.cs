using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class KeyCodeToImage
{
    public KeyCode key;
    public Sprite image;
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
    public List<KeyCodeToImage> keyboard = new();

    [Header("Move Setting")]
    private KeyCode jump = KeyCode.Space;
    public KeyCode Jump
    {
        get => jump;
        set
        {
            jump = value;
        }
    }

    private KeyCode walkForward = KeyCode.W;
    public KeyCode WalkForward
    {
        get => walkForward;
        set
        {
            walkForward = value;
        }
    }

    private KeyCode walkBackward = KeyCode.S;
    public KeyCode WalkBackward
    {
        get => walkBackward;
        set
        {
            walkBackward = value;
        }
    }

    private KeyCode walkLeft = KeyCode.A;
    public KeyCode WalkLeft
    {
        get => walkLeft;
        set
        {
            walkLeft = value;
        }
    }

    private KeyCode walkRight = KeyCode.D;
    public KeyCode WalkRight
    {
        get => walkRight;
        set
        {
            walkRight = value;
        }
    }

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
