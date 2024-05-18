using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapons : MonoBehaviour
{
    [SerializeField] PlayerBasicInformationScriptable informationScriptable;
    [SerializeField] PlayerBattleValueScriptable BattleInfo;
    public GameObject MainWeapon, SecondWeapon, HoldingWeapon;
    [SerializeField] PlayerShoot playerShoot;

    private void OnValidate()
    {
        if (MainWeapon != null)
        {
            HoldingWeapon = MainWeapon;
        }
    }

    private void Start()
    {
        BattleInfo.nowWeapon = MainWeapon.transform.GetChild(0).gameObject;
    }

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

    public void changeToMainWeapon(InputAction.CallbackContext context)
    {
        BattleInfo.nowWeapon = MainWeapon.transform.GetChild(0).gameObject;
        HoldingWeapon = MainWeapon;
        GetComponent<Animator>().Play("WeaponIdle");
        playerShoot.reloading = false;
        MainWeapon.SetActive(true);
        SecondWeapon.SetActive(false);
    }

    public void changeToSecondWeapon(InputAction.CallbackContext context)
    {
        BattleInfo.nowWeapon = SecondWeapon.transform.GetChild(0).gameObject;
        HoldingWeapon = SecondWeapon;
        GetComponent<Animator>().Play("WeaponIdle");
        playerShoot.reloading = false;
        MainWeapon.SetActive(false);
        SecondWeapon.SetActive(true);
    }

    public void changeToMainWeapon()
    {
        BattleInfo.nowWeapon = MainWeapon.transform.GetChild(0).gameObject;
        HoldingWeapon = MainWeapon;
        GetComponent<Animator>().Play("WeaponIdle");
        playerShoot.reloading = false;
        MainWeapon.SetActive(true);
        SecondWeapon.SetActive(false);
    }

    public void changeToSecondWeapon()
    {
        BattleInfo.nowWeapon = SecondWeapon.transform.GetChild(0).gameObject;
        HoldingWeapon = SecondWeapon;
        GetComponent<Animator>().Play("WeaponIdle");
        playerShoot.reloading = false;
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
