using UnityEngine;

public class GravelEffect : MonoBehaviour
{
    [SerializeField] PlayerBattleValueScriptable playerBattleInfo;

    // Start is called before the first frame update
    void FixedUpdate()
    {
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
