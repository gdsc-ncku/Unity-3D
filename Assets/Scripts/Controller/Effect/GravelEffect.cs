using System.Threading;
using UnityEngine;

public class GravelEffect : MonoBehaviour
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
        
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.transform.position = hit.point;
        }

    }
    private void OnParticleCollision(GameObject other)
    {
        EnemyAI enemy;
        if (other.TryGetComponent(out enemy))
        {
            enemy.ReduceHealth(playerBattleInfo.Role.GetComponent<StudentDataManager>().studentData.Q_SkillDamage * playerBattleInfo.Role.GetComponent<StudentDataManager>().studentData.Q_SkillDamageRate);
        }
    }
}
