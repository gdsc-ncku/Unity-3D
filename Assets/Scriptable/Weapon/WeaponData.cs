using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Storage inheritable information
[CreateAssetMenu(fileName = "WeaponData", menuName = "PlayerInformation/Weapon/WeaponData", order = 2)]
public class WeaponData : ScriptableObject
{
    public float attackMultiplier = 1;
    public float attackSpeedMultiplier = 0.1f;
    public float reloadSpeedMultiplier = 1;
    // public float criticalHitMultiplier = 0;
}
