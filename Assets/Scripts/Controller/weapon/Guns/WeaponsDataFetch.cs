using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsDataFetch : MonoBehaviour
{
    public WeaponData ThisWeapon = null;

    //Can adjust
    public Vector3 weaponPosOffset = new Vector3(0, 0, 0);
    public Transform weaponAttackPoint;
    public int bulletsMax;
    public int bulletsLeft;
    public Sprite weaponIcon;
    private void Start()
    {
        weaponAttackPoint = gameObject.transform.GetChild(0).gameObject.transform;
        bulletsLeft = ThisWeapon.maxBullets;
        bulletsMax = ThisWeapon.maxBullets;
        weaponIcon = ThisWeapon.Icon;
    }
}
