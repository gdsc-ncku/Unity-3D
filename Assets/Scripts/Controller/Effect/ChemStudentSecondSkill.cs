using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemStudentSecondSkill : MonoBehaviour
{
    [SerializeField] PlayerBattleValueScriptable playerBattleInfo;
    [SerializeField] GameObject effect;

    private void Start()
    {
        ParticleSystem particleSystem;
        if (TryGetComponent(out particleSystem))
        {
            Destroy(gameObject, particleSystem.main.duration);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(effect, gameObject.transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnParticleCollision(GameObject other)
    {
        EnemyAI enemy;
        if (other.TryGetComponent(out enemy))
        {
            enemy.ReduceHealth(playerBattleInfo.Role.GetComponent<StudentDataManager>().studentData.E_SkillDamage * playerBattleInfo.Role.GetComponent<StudentDataManager>().studentData.E_SkillDamageRate / gameObject.GetComponent<ParticleSystem>().main.duration);
        }
        else if(other.layer == LayerMask.NameToLayer("Player"))
        {
            playerBattleInfo.ReduceHealth(playerBattleInfo.Role.GetComponent<StudentDataManager>().studentData.E_SkillDamage * playerBattleInfo.Role.GetComponent<StudentDataManager>().studentData.E_SkillDamageRate / gameObject.GetComponent<ParticleSystem>().main.duration * 0.3f);
        }
    }
}
