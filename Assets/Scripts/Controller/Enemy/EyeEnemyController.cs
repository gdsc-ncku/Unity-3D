using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeEnemyController : MonoBehaviour
{
    [SerializeField] EyeEnemy eyeEnemy;

    public void InvokeEyeEnemyAttack()
    {
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        for(int i = 0; i < eyeEnemy.BulletsPerGen; i++)
        {
            yield return new WaitForSeconds(0.5f);
            eyeEnemy.attack(gameObject, 0.5f * (eyeEnemy.BulletsPerGen - i));
        }
    }
}
