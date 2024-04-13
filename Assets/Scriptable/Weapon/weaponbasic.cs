using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Storage inheritable information
[CreateAssetMenu(fileName = "weaponbasic", menuName = "PlayerInformation/Weapon/Weaponbasic", order = 1)]
public class weaponbasic : ScriptableObject
{
    public float attack = 1;
    public float attackSpeed = 0.1f;
    public float reloadSpeed = 1;
    // public float criticalHitMultiplier = 0;
}
