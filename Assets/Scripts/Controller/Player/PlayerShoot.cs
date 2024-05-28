using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] PlayerBasicInformationScriptable informationScriptable;
    [SerializeField] PlayerBattleValueScriptable BattleInfo;
    //bools
    public bool reloading;
    bool CanShoot = true;

    //Reference
    Camera fpsCam; //auto fatch
    public Rigidbody playerRb;
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;
    public Animator animator;
    //audio
    public AudioSource audioSource;

    //recoil
    [SerializeField] GameObject cameraRoot;
    [SerializeField] private float snappiness;
    [SerializeField] private float returnSpeed;
    [SerializeField] private float returnRate;

    public void Awake()
    {
        reloading = false;
    }

    private void Start()
    {
        fpsCam = Camera.main;
    }

    public void OnEnable()
    {
        if (informationScriptable == null || informationScriptable.playerControl == null)
        {
            Debug.Log("GunBasic: Setting informationScriptable and playerControl first");
            StartCoroutine("DebugPlayerControl");
            return;
        }

        informationScriptable.playerControl.Player.Fire.started += startShoot;
        informationScriptable.playerControl.Player.Fire.canceled += FinishShoot;
        informationScriptable.playerControl.Player.Reload.started += Reload;
    }

    public void OnDisable()
    {
        if (informationScriptable == null)
        {
            Debug.Log("GunBasic: Setting informationScriptable first");
            return;
        }

        informationScriptable.playerControl.Player.Fire.started -= startShoot;
        informationScriptable.playerControl.Player.Fire.canceled -= FinishShoot;
        informationScriptable.playerControl.Player.Reload.started -= Reload;
    }

    public void OnDestroy()
    {
        informationScriptable.playerControl.Player.Fire.started -= startShoot;
        informationScriptable.playerControl.Player.Fire.canceled -= FinishShoot;
        informationScriptable.playerControl.Player.Reload.started -= Reload;
    }

    public void startShoot(InputAction.CallbackContext context)
    {
        InvokeRepeating("Shoot", 0f, BattleInfo.nowWeaponData.ThisWeapon.timeBetweenShooting);
    }

    public void Shoot()
    {
        if(!CanShoot)
        {
            return;
        }
        else
        {
            CanShoot = false;
            StartCoroutine(ResetShoot());
        }

        if (BattleInfo.nowWeaponData.ThisWeapon == null)
        {
            Debug.Log("Setting gun data first");
            return;
        }

        if (reloading)
        {
            return;
        }

        if (BattleInfo.nowWeaponData.bulletsLeft <= 0)
        {
            Debug.Log("Reload..");
            Reload();
            return;
        }

        if (Camera.main == null)
        {
            Debug.LogError("Camera.main is not assigned, setting camera's tag as MainCamera first");
            return;
        }
        //Find the exact hit position using a raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Just a ray through the middle of your current view
        RaycastHit hit;

        //check if ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log($"Aim: {hit.collider.gameObject.name}");
            targetPoint = hit.point;
        }
        else
            targetPoint = ray.GetPoint(75); //Just a point far away from the player

        while (BattleInfo.nowWeaponData.ThisWeapon.bulletsShot < BattleInfo.nowWeaponData.ThisWeapon.bulletsPerTap)
        {
            //Calculate direction from attackPoint to targetPoint
            Vector3 directionWithoutSpread = targetPoint - BattleInfo.nowWeaponData.weaponAttackPoint.position;

            //Calculate spread
            float x = Random.Range(-BattleInfo.nowWeaponData.ThisWeapon.spread, BattleInfo.nowWeaponData.ThisWeapon.spread);
            float y = Random.Range(-BattleInfo.nowWeaponData.ThisWeapon.spread, BattleInfo.nowWeaponData.ThisWeapon.spread);

            //Calculate new direction with spread
            Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); //Just add spread to last direction

            //Instantiate bullet/projectile
            GameObject currentBullet = Instantiate(BattleInfo.nowWeaponData.ThisWeapon.bullet, BattleInfo.nowWeaponData.weaponAttackPoint.position, Quaternion.identity); //store instantiated bullet in currentBullet
                                                                                                                                                                                     //Rotate bullet to shoot direction
            currentBullet.transform.forward = directionWithSpread.normalized;

            //Add forces to bullet
            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * BattleInfo.nowWeaponData.ThisWeapon.shootForce, ForceMode.Impulse);
            currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * BattleInfo.nowWeaponData.ThisWeapon.upwardForce, ForceMode.Impulse);
            BattleInfo.nowWeaponData.ThisWeapon.bulletsShot++;
        }
        BattleInfo.nowWeaponData.ThisWeapon.bulletsShot = 0;

        if (BattleInfo.nowWeaponData.ThisWeapon.gunSound != null)
        {
            audioSource.PlayOneShot(BattleInfo.nowWeaponData.ThisWeapon.gunSound);
        }

        //Instantiate muzzle flash, if you have one
        if (muzzleFlash != null)
        {
            Instantiate(muzzleFlash, BattleInfo.nowWeaponData.weaponAttackPoint.position, Quaternion.identity).GetComponent<MuzzleFlash>().followingAttackPoint = BattleInfo.nowWeaponData.weaponAttackPoint.gameObject;
        }

        BattleInfo.nowWeaponData.bulletsLeft--;
        animator.Play("WeaponRecoil", 0, 0f);
        Debug.Log(BattleInfo.nowWeaponData.bulletsLeft);

        if (BattleInfo.nowWeaponData.bulletsLeft <= 0)
        {
            Debug.Log("Reload..");
            Reload();
        }

        RecoilFire();
    }

    private void RecoilFire()
    {
        cameraRoot.transform.localRotation *= Quaternion.Euler(new Vector3(BattleInfo.nowWeaponData.ThisWeapon.recoilX, Random.Range(-BattleInfo.nowWeaponData.ThisWeapon.recoilY, BattleInfo.nowWeaponData.ThisWeapon.recoilY), Random.Range(-BattleInfo.nowWeaponData.ThisWeapon.recoilZ, BattleInfo.nowWeaponData.ThisWeapon.recoilZ)));
    }

    public void FinishShoot(InputAction.CallbackContext context)
    {
        Debug.Log("cancel");
        CancelInvoke("Shoot");
    }

    IEnumerator DebugPlayerControl()
    {
        yield return new WaitForSeconds(1);
        informationScriptable.playerControl.Player.Fire.started += startShoot;
        informationScriptable.playerControl.Player.Fire.canceled += FinishShoot;
        informationScriptable.playerControl.Player.Reload.started += Reload;
    }

    private void Reload(InputAction.CallbackContext context)
    {
        if(BattleInfo.nowWeaponData.bulletsLeft == BattleInfo.nowWeaponData.ThisWeapon.maxBullets)
        {
            return;
        }

        reloading = true;
        animator.SetFloat("SpeedMultiplier", 1 / BattleInfo.nowWeaponData.ThisWeapon.reloadTime);
        animator.Play("WeaponReload", 0, 0f);
        StartCoroutine(ResetBullets());
    }

    private void Reload()
    {
        if (BattleInfo.nowWeaponData.bulletsLeft == BattleInfo.nowWeaponData.ThisWeapon.maxBullets)
        {
            return;
        }

        reloading = true;
        animator.SetFloat("SpeedMultiplier", 1 / BattleInfo.nowWeaponData.ThisWeapon.reloadTime);
        animator.Play("WeaponReload", 0, 0f);
        StartCoroutine(ResetBullets());
    }
 
    private IEnumerator ResetBullets()
    {
        yield return new WaitForSeconds(BattleInfo.nowWeaponData.ThisWeapon.reloadTime);
        BattleInfo.nowWeaponData.bulletsLeft = BattleInfo.nowWeaponData.ThisWeapon.maxBullets;
        reloading = false;
    }

    private IEnumerator ResetShoot()
    {
        yield return new WaitForSeconds(BattleInfo.nowWeaponData.ThisWeapon.timeBetweenShooting);
        CanShoot = true;
    }
}
