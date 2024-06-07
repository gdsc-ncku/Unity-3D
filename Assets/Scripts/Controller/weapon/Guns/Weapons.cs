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
    [SerializeField] PlayerUI PlayerUI;

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
        PlayerUI = gameObject.transform.root.gameObject.GetComponent<PlayerUI>();
        changeToMainWeapon();
    }

    private void OnEnable()
    {
        if (informationScriptable == null || informationScriptable.playerControl == null)
        {
            Debug.Log("PlayerBasicInformationScriptable Disappear");
            return;
        }

        informationScriptable.playerControl.Player.Weapon1.performed += changeToMainWeapon;
        informationScriptable.playerControl.Player.Weapon2.performed += changeToSecondWeapon;
    }

    private void OnDisable()
    {
        if (informationScriptable == null || informationScriptable.playerControl == null)
        {
            Debug.Log("PlayerBasicInformationScriptable Disappear");
            return;
        }

        informationScriptable.playerControl.Player.Weapon1.performed -= changeToMainWeapon;
        informationScriptable.playerControl.Player.Weapon2.performed -= changeToSecondWeapon;
    }

    private void OnDestroy()
    {
        if (informationScriptable == null || informationScriptable.playerControl == null)
        {
            Debug.Log("PlayerBasicInformationScriptable Disappear");
            return;
        }

        informationScriptable.playerControl.Player.Weapon1.performed -= changeToMainWeapon;
        informationScriptable.playerControl.Player.Weapon2.performed -= changeToSecondWeapon;
    }

    public void changeToMainWeapon(InputAction.CallbackContext context)
    {
        if (MainWeapon.transform.childCount == 0)
        {
            return;
        }

        BattleInfo.nowWeapon = MainWeapon.transform.GetChild(0).gameObject;
        HoldingWeapon = MainWeapon;
        GetComponent<Animator>().Play("WeaponIdle");
        playerShoot.reloading = false;
        MainWeapon.SetActive(true);
        SecondWeapon.SetActive(false);

        //UI
        BattleInfo.nowWeaponData.BulletsLeftChange.AddListener(() => PlayerUI.BulletLeftNumUpdate());
        PlayerUI.ChangeWeaponInfo();
    }

    public void changeToSecondWeapon(InputAction.CallbackContext context)
    {
        if(SecondWeapon.transform.childCount == 0)
        {
            return;
        }

        BattleInfo.nowWeapon = SecondWeapon.transform.GetChild(0).gameObject;
        HoldingWeapon = SecondWeapon;
        GetComponent<Animator>().Play("WeaponIdle");
        playerShoot.reloading = false;
        MainWeapon.SetActive(false);
        SecondWeapon.SetActive(true);

        //UI
        BattleInfo.nowWeaponData.BulletsLeftChange.AddListener(() => PlayerUI.BulletLeftNumUpdate());
        PlayerUI.ChangeWeaponInfo();
    }

    public void changeToMainWeapon()
    {
        if (MainWeapon.transform.childCount == 0)
        {
            return;
        }

        BattleInfo.nowWeapon = MainWeapon.transform.GetChild(0).gameObject;
        HoldingWeapon = MainWeapon;
        GetComponent<Animator>().Play("WeaponIdle");
        playerShoot.reloading = false;
        MainWeapon.SetActive(true);
        SecondWeapon.SetActive(false);

        //UI
        BattleInfo.nowWeaponData.BulletsLeftChange.AddListener(() => PlayerUI.BulletLeftNumUpdate());
        PlayerUI.ChangeWeaponInfo();
    }

    public void changeToSecondWeapon()
    {
        if (SecondWeapon.transform.childCount == 0)
        {
            return;
        }

        BattleInfo.nowWeapon = SecondWeapon.transform.GetChild(0).gameObject;
        HoldingWeapon = SecondWeapon;
        GetComponent<Animator>().Play("WeaponIdle");
        playerShoot.reloading = false;
        MainWeapon.SetActive(false);
        SecondWeapon.SetActive(true);

        //UI
        BattleInfo.nowWeaponData.BulletsLeftChange.AddListener(() => PlayerUI.BulletLeftNumUpdate());
        PlayerUI.ChangeWeaponInfo();
    }
}
