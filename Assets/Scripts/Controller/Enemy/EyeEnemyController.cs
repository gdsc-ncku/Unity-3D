using System.Collections;
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
            yield return new WaitForSeconds(eyeEnemy.GenBulletsTime);
            eyeEnemy.attack(gameObject, eyeEnemy.GenBulletsTime * (eyeEnemy.BulletsPerGen - i));
        }
    }
}
