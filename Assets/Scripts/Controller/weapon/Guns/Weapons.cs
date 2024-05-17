using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapons : MonoBehaviour
{
    [SerializeField] PlayerBasicInformationScriptable informationScriptable;
    [SerializeField] GameObject MainWeapon, SecondWeapon;

    private void OnEnable()
    {
        if (informationScriptable == null || informationScriptable.playerControl == null)
        {
            Debug.Log("GunBasic: Setting informationScriptable and playerControl first");
            StartCoroutine("DebugPlayerControl");
            return;
        }

        informationScriptable.playerControl.Player.Weapon1.performed += changeToMainWeapon;
        informationScriptable.playerControl.Player.Weapon2.performed += changeToSecondWeapon;
    }

    private void OnDisable()
    {
        informationScriptable.playerControl.Player.Weapon1.performed -= changeToMainWeapon;
        informationScriptable.playerControl.Player.Weapon2.performed -= changeToSecondWeapon;
    }

    private void OnDestroy()
    {
        informationScriptable.playerControl.Player.Weapon1.performed -= changeToMainWeapon;
        informationScriptable.playerControl.Player.Weapon2.performed -= changeToSecondWeapon;
    }

    void changeToMainWeapon(InputAction.CallbackContext context)
    {
        MainWeapon.SetActive(true);
        SecondWeapon.SetActive(false);
    }

    void changeToSecondWeapon(InputAction.CallbackContext context)
    {
        MainWeapon.SetActive(false);
        SecondWeapon.SetActive(true);
    }

    IEnumerator DebugPlayerControl()
    {
        yield return new WaitForSeconds(1);
        informationScriptable.playerControl.Player.Weapon1.performed += changeToMainWeapon;
        informationScriptable.playerControl.Player.Weapon2.performed += changeToSecondWeapon;
    }
}
