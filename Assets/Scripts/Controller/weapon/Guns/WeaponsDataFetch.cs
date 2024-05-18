using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsDataFetch : MonoBehaviour
{
    public WeaponData ThisWeapon = null;

    //Can adjust
    public Vector3 weaponPosOffset = new Vector3(0, 0, 0);
    public Transform weaponAttackPoint;
    public int bulletsLeft;
    private void Start()
    {
        weaponAttackPoint = gameObject.transform.GetChild(0).gameObject.transform;
        bulletsLeft = ThisWeapon.maxBullets;
    }
}
