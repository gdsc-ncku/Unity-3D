using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MEStudentSecondSkill : MonoBehaviour
{
    [SerializeField] PlayerBattleValueScriptable playerBattleInfo;
    public GameObject target;
    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
    }
    void FixedUpdate()
    {
        if (target != null)
        {
            gameObject.transform.position = target.transform.position + new Vector3(0f, 8f, 0f);
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        EnemyAI enemy;
        if (other.TryGetComponent(out enemy))
        {
            enemy.ReduceHealth(playerBattleInfo.Role.GetComponent<StudentDataManager>().studentData.E_SkillDamage * playerBattleInfo.Role.GetComponent<StudentDataManager>().studentData.E_SkillDamageRate);
        }

        Collider[] hitColliders = Physics.OverlapSphere(other.transform.position, GetComponent<SphereCollider>().radius, LayerMask.GetMask("Enemy"));

        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.gameObject == other)
            {
                continue;
            }    

            hitCollider.GetComponent<EnemyAI>().ReduceHealth(playerBattleInfo.Role.GetComponent<StudentDataManager>().studentData.E_SkillDamage * playerBattleInfo.Role.GetComponent<StudentDataManager>().studentData.E_SkillDamageRate * 0.3f);
        }
    }
}
