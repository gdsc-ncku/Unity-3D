using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemStudentMainSkill : MonoBehaviour
{
    [SerializeField] PlayerBattleValueScriptable playerBattleInfo;
    [SerializeField] GameObject effect;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(effect, gameObject.transform.position, Quaternion.Euler(-90, 0, 0));
        Destroy(gameObject);
    }

    private void Start()
    {
        ParticleSystem particleSystem;
        if (TryGetComponent(out particleSystem))
        {
            Destroy(gameObject, particleSystem.main.duration);
        }
    }

    private void Update()
    {
        ParticleSystem particleSystem;
        if (TryGetComponent(out particleSystem))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius, LayerMask.GetMask("Enemy"));

            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.transform.root.gameObject.TryGetComponent(out EnemyAI enemy))
                {
                    enemy.ReduceHealth(playerBattleInfo.Role.GetComponent<StudentDataManager>().studentData.Q_SkillDamage * playerBattleInfo.Role.GetComponent<StudentDataManager>().studentData.Q_SkillDamageRate * Time.deltaTime);
                }
            }
        }
    }
}
