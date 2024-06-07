using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StingRayEnemyController : MonoBehaviour
{
    [SerializeField] StingRayEnemy stingRayEnemy;

    public void InvokeStingRayEnemyAttack()
    {
        stingRayEnemy.attack(gameObject);
    }
}
