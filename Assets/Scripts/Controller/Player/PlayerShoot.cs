using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] PlayerBasicInformationScriptable informationScriptable;
    [SerializeField] PlayerBattleValueScriptable BattleInfo;
    [SerializeField] PlayerUI PlayerUI;
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

    public void Awake()
    {
        reloading = false;
    }

    private void Start()
    {
        fpsCam = Camera.main;
        PlayerUI = gameObject.GetComponent<PlayerUI>();
    }

    public void OnEnable()
    {
        if(informationScriptable == null || informationScriptable.playerControl == null)
        {
            Debug.Log("Mistake");
            return;
        }

        informationScriptable.playerControl.Player.Fire.started += startShoot;
        informationScriptable.playerControl.Player.Fire.canceled += FinishShoot;
        informationScriptable.playerControl.Player.Reload.started += Reload;
    }

    public void OnDisable()
    {
        if (informationScriptable.playerControl == null)
        {
            Debug.Log("informationScriptable disappear");
            return;
        }

        informationScriptable.playerControl.Player.Fire.started -= startShoot;
        informationScriptable.playerControl.Player.Fire.canceled -= FinishShoot;
        informationScriptable.playerControl.Player.Reload.started -= Reload;
    }

    public void OnDestroy()
    {
        if (informationScriptable.playerControl == null)
        {
            Debug.Log("informationScriptable disappear");
            return;
        }

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

        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        //Find the exact hit position using a raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Just a ray through the middle of your current view
        RaycastHit hit;
        bool isHit = false;

        //check if ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            //Debug.Log($"Aim: {hit.collider.gameObject.name}");
            targetPoint = hit.point;
            isHit = true;
        }
        else
        {
            targetPoint = ray.GetPoint(1000); //Just a point far away from the player
        }

        //Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - BattleInfo.nowWeaponData.weaponAttackPoint.position;

        //Instantiate bullet/projectile
        GameObject currentBullet;
        currentBullet = Instantiate(BattleInfo.nowWeaponData.ThisWeapon.bullet, BattleInfo.nowWeaponData.weaponAttackPoint.position + directionWithoutSpread.normalized, Quaternion.identity);
        currentBullet.transform.forward = (targetPoint - currentBullet.transform.position).normalized;
        if (isHit)
        {
            currentBullet.GetComponent<FPSCustomBullet>().target = hit.collider.transform.root.gameObject;
        }
        else
        {
            currentBullet.GetComponent<FPSCustomBullet>().target = null;
        }
        currentBullet.GetComponent<FPSCustomBullet>().AttackWeapon = BattleInfo.nowWeaponData;

        //Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * BattleInfo.nowWeaponData.ThisWeapon.shootForce, ForceMode.Impulse);
        //currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * BattleInfo.nowWeaponData.ThisWeapon.upwardForce, ForceMode.Impulse);
        BattleInfo.nowWeaponData.ThisWeapon.bulletsShot++;

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
        PlayerUI.BulletLeftNumUpdate();
        animator.Play("WeaponRecoil", 0, 0f);

        //Debug.Log(BattleInfo.nowWeaponData.bulletsLeft);

        if (BattleInfo.nowWeaponData.bulletsLeft <= 0)
        {
            //Debug.Log("Reload..");
            Reload();
        }

        RecoilFire();

        if (BattleInfo.nowWeaponData.ThisWeapon.bulletsShot < BattleInfo.nowWeaponData.ThisWeapon.bulletsPerTap)
        {
            yield return new WaitForSeconds(BattleInfo.nowWeaponData.ThisWeapon.timeBetweenShots);
            StartCoroutine(Fire());
        }
        else
        {
            BattleInfo.nowWeaponData.ThisWeapon.bulletsShot = 0;
        }
    }

    private void RecoilFire()
    {
        cameraRoot.transform.localRotation *= Quaternion.Euler(new Vector3(BattleInfo.nowWeaponData.ThisWeapon.recoilX, Random.Range(-BattleInfo.nowWeaponData.ThisWeapon.recoilY, BattleInfo.nowWeaponData.ThisWeapon.recoilY), Random.Range(-BattleInfo.nowWeaponData.ThisWeapon.recoilZ, BattleInfo.nowWeaponData.ThisWeapon.recoilZ)));
    }

    public void FinishShoot(InputAction.CallbackContext context)
    {
        //Debug.Log("cancel");
        CancelInvoke("Shoot");
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
