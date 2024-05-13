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

    //bullet force
    public float shootForce, upwardForce;

    //Gun stats
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    public int bulletsLeft, maxBullets, bulletsShot;

    //Recoil
    public float recoilForce;

    //bug fixing 
    public bool allowInvoke = true;

    public AudioClip gunSound;

    public float recoilX;
    public float recoilY;
    public float recoilZ;
}
