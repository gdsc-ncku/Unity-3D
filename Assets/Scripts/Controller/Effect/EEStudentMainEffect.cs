using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EEStudentMainEffect : MonoBehaviour
{
    [SerializeField] PlayerBattleValueScriptable playerBattleInfo;
    [SerializeField] GameObject effect;

    private void Start()
    {
        ParticleSystem particleSystem;
        if (TryGetComponent(out particleSystem))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 5, LayerMask.GetMask("Enemy"));

            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.TryGetComponent(out EnemyAI enemy))
                {
                    enemy.ReduceHealth(playerBattleInfo.Role.GetComponent<StudentDataManager>().studentData.Q_SkillDamage * playerBattleInfo.Role.GetComponent<StudentDataManager>().studentData.Q_SkillDamageRate);
                }
            }
            Destroy(gameObject, 2f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(effect, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        Destroy(gameObject);
    }
}
