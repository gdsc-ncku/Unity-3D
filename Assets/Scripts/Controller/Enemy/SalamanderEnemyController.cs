using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalamanderEnemyController : MonoBehaviour
{
    [SerializeField] SalamanderEnemy salamanderEnemy;
    [SerializeField] GameObject GenPos;

    public void InvokeSalamanderEnemyAttack()
    {
        InvokeRepeating("Attack", 0, 0.2f);
        Invoke("FinishSalamanderEnemyAttack", salamanderEnemy.AttackTime / 2);
    }

    public void FinishSalamanderEnemyAttack()
    {
        CancelInvoke("Attack");
    }

    void Attack()
    {
        StartCoroutine(executeAttack());
    }

    IEnumerator executeAttack()
    {
        //wait for state update
        yield return null;
        if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attacking"))
        {
            salamanderEnemy.attack(gameObject, GenPos);
        }
        else
        {
            CancelInvoke("Attack");
        }
    }
}
