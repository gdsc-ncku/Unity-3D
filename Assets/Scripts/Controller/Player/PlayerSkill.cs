using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] private PlayerBasicInformationScriptable PlayerBasicInfo;
    [SerializeField] private PlayerBattleValueScriptable PlayerBattleInfo;
    private void OnEnable()
    {
        if (PlayerBasicInfo == null || PlayerBasicInfo.playerControl == null)
        {
            Debug.Log("Null");
            return;
        }

        PlayerBasicInfo.playerControl.Player.Q_Skill.started += InvokeMainSkill;
        PlayerBasicInfo.playerControl.Player.E_Skill.started += InvokeSecondSkill;
    }

    private void OnDisable()
    {
        if(PlayerBasicInfo == null || PlayerBasicInfo.playerControl == null)
        {
            Debug.Log("Null");
            return;
        }

        PlayerBasicInfo.playerControl.Player.Q_Skill.started -= InvokeMainSkill;
        PlayerBasicInfo.playerControl.Player.E_Skill.started -= InvokeSecondSkill;
    }

    private void OnDestroy()
    {
        if (PlayerBasicInfo == null || PlayerBasicInfo.playerControl == null)
        {
            Debug.Log("Null");
            return;
        }

        PlayerBasicInfo.playerControl.Player.Q_Skill.started -= InvokeMainSkill;
        PlayerBasicInfo.playerControl.Player.E_Skill.started -= InvokeSecondSkill;
    }

    private void InvokeMainSkill(InputAction.CallbackContext context)
    {
        PlayerBattleInfo.Role.GetComponent<StudentDataManager>().studentData.UseingQ_Skill();
    }

    private void InvokeSecondSkill(InputAction.CallbackContext context)
    {
        PlayerBattleInfo.Role.GetComponent<StudentDataManager>().studentData.UseingE_Skill();
    }
}
