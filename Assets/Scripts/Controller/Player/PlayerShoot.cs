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
            //Debug.Log("Reload..");
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
            //Debug.Log($"Aim: {hit.collider.gameObject.name}");
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(1000); //Just a point far away from the player
        }

        while (BattleInfo.nowWeaponData.ThisWeapon.bulletsShot < BattleInfo.nowWeaponData.ThisWeapon.bulletsPerTap)
        {
            //For Shotgun
            Vector3 offsetShotgunBullet = new Vector3(0, 0, 0);
            if(BattleInfo.nowWeaponData.ThisWeapon.bulletsShot > 0)
            {
                offsetShotgunBullet = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            }

            //Calculate direction from attackPoint to targetPoint
            Vector3 directionWithoutSpread = targetPoint - BattleInfo.nowWeaponData.weaponAttackPoint.position;

            //Instantiate bullet/projectile
            GameObject currentBullet;
            RaycastHit attackPointHit;
            float offset = 0f;
            while (Physics.Raycast(new Ray(BattleInfo.nowWeaponData.weaponAttackPoint.position + directionWithoutSpread.normalized * offset, directionWithoutSpread), out attackPointHit) && attackPointHit.collider.gameObject != hit.collider.gameObject)
            {
                offset += 0.1f;
            }

            currentBullet = Instantiate(BattleInfo.nowWeaponData.ThisWeapon.bullet, BattleInfo.nowWeaponData.weaponAttackPoint.position + directionWithoutSpread.normalized * (offset + 1) + offsetShotgunBullet, Quaternion.identity);
            currentBullet.transform.forward = directionWithoutSpread.normalized;
            currentBullet.GetComponent<FPSCustomBullet>().AttackWeapon = BattleInfo.nowWeaponData;

            //Add forces to bullet
            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * BattleInfo.nowWeaponData.ThisWeapon.shootForce, ForceMode.Impulse);
            //currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * BattleInfo.nowWeaponData.ThisWeapon.upwardForce, ForceMode.Impulse);
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
            //Debug.Log("Reload..");
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
