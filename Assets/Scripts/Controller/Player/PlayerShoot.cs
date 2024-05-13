using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] PlayerBasicInformationScriptable informationScriptable;
    //bools
    public bool shooting, readyToShoot, reloading;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public Rigidbody playerRb;
    public WeaponData weaponData = null;
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
        shooting = false;
        readyToShoot = false;
        reloading = false;
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
    }

    public void OnDestroy()
    {
        informationScriptable.playerControl.Player.Fire.started -= startShoot;
        informationScriptable.playerControl.Player.Fire.canceled -= FinishShoot;
    }

    public void startShoot(InputAction.CallbackContext context)
    {
        InvokeRepeating("Shoot", 0f, weaponData.timeBetweenShooting);
    }

    public void Shoot()
    {
        if (weaponData == null)
        {
            Debug.Log("Setting gun data first");
            return;
        }

        if(weaponData.bulletsLeft <= 0)
        {
            Debug.Log("Reload..");
            weaponData.bulletsLeft = weaponData.maxBullets;
            return;
        }

        readyToShoot = false;

        //Find the exact hit position using a raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Just a ray through the middle of your current view
        RaycastHit hit;

        //check if ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75); //Just a point far away from the player

        //Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //Calculate spread
        float x = Random.Range(-weaponData.spread, weaponData.spread);
        float y = Random.Range(-weaponData.spread, weaponData.spread);

        //Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); //Just add spread to last direction

        //Instantiate bullet/projectile
        GameObject currentBullet = Instantiate(weaponData.bullet, attackPoint.position, Quaternion.identity); //store instantiated bullet in currentBullet
        //Rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;

        //Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * weaponData.shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * weaponData.upwardForce, ForceMode.Impulse);

        if(weaponData.gunSound != null)
        {
            audioSource.PlayOneShot(weaponData.gunSound);
        }

        //Instantiate muzzle flash, if you have one
        if (muzzleFlash != null)
        {
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        }

        if (!shooting)
        {
            weaponData.bulletsLeft--;
            animator.Play("WeaponRecoil", 0, 0f);
        }
        weaponData.bulletsShot++;

        if (weaponData.bulletsLeft <= 0)
        {
            Debug.Log("Reload..");
            weaponData.bulletsLeft = weaponData.maxBullets;

            ResetShoot();
        }
        else if(weaponData.bulletsShot < weaponData.bulletsPerTap)
        {
            Invoke("Shoot", weaponData.timeBetweenShots);
            shooting = true;
        }
        else
        {
            ResetShoot();
        }

        if (!shooting)
        {
            RecoilFire();
        }
    }

    private void ResetShoot()
    {
        weaponData.bulletsShot = 0;
        shooting = false;
    }

    private void RecoilFire()
    {
        cameraRoot.transform.localRotation *= Quaternion.Euler(new Vector3(weaponData.recoilX, Random.Range(-weaponData.recoilY, weaponData.recoilY), Random.Range(-weaponData.recoilZ, weaponData.recoilZ)));
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
    }
}
