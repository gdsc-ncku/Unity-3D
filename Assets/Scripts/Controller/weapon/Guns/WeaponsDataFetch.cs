using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponsDataFetch : MonoBehaviour
{
    public WeaponData ThisWeapon = null;
    public UnityEvent BulletsLeftChange;

    //Can adjust
    public Vector3 weaponPosOffset = new Vector3(0, 0, 0);
    public Transform weaponAttackPoint;
    public int bulletsMax;
    public int BulletsLeft;
    public int bulletsLeft
    {
        get
        { 
            return BulletsLeft; 
        }

        set
        {
            BulletsLeft = value;
            BulletsLeftChange.Invoke();
        }
    }

    private void Awake()
    {
        weaponAttackPoint = gameObject.transform.GetChild(0).gameObject.transform;
        bulletsLeft = ThisWeapon.maxBullets;
        bulletsMax = ThisWeapon.maxBullets;
    }

    private void Start()
    {
        
    }
}
