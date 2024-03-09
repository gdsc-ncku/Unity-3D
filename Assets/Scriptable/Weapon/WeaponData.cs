using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Storage inheritable information
[CreateAssetMenu(fileName = "WeaponData", menuName = "PlayerInformation/Weapon/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    public float attackMultiplier = 1;
    public double attackSpeedMultiplier = 0.1;
    // public float reloadSpeedMultiplier = 1;
    // public float criticalHitMultiplier = 0;
}
