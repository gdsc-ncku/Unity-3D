using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Storage inheritable information
[CreateAssetMenu(fileName = "WeaponData", menuName = "PlayerInformation/Weapon/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    //bullet
    public GameObject bullet;
    public float damage;

    //bullet force
    public float shootForce, upwardForce;

    //Gun stats
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    public int maxBullets, bulletsShot;

    //Recoil
    public float recoilForce;

    //bug fixing 
    public bool allowInvoke = true;

    public AudioClip gunSound;

    public float recoilX;
    public float recoilY;
    public float recoilZ;
    public Sprite Icon;

    private void OnValidate()
    {
        if(bulletsPerTap > 1)
        {
            timeBetweenShooting = timeBetweenShots * bulletsPerTap;
        }
        else
        {
            timeBetweenShots = 0;
        }
    }
}
