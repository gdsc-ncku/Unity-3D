using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawMainEffect : MonoBehaviour
{
    [SerializeField] PlayerBattleValueScriptable playerBattleInfo;
    private void Start()
    {
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
    }
    private void OnParticleCollision(GameObject other)
    {
        EnemyAI enemy;
        if(other.TryGetComponent(out enemy))
        {
            enemy.ReduceHealth(playerBattleInfo.Role.GetComponent<StudentDataManager>().studentData.Q_SkillDamage * playerBattleInfo.Role.GetComponent<StudentDataManager>().studentData.Q_SkillDamageRate / GetComponent<ParticleSystem>().main.duration);
        }
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
    }
}
