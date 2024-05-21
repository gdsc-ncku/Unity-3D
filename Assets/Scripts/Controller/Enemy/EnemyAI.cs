using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum status { chasing, attack};
public class EnemyAI : MonoBehaviour
{
    [SerializeField] EnemyScriptableObject EnemyInfo;
    [SerializeField] GameObject player;
    [SerializeField] LayerMask searchLayer;
    [SerializeField] float searchRadius;
    status nowStatus;

    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        nowStatus = status.chasing;
        StartCoroutine(SearchRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null && transform.position != player.transform.position)
        {
            
        }

        if (agent.isOnNavMesh)
        {
            
        }
    }

    IEnumerator SearchRoutine()
    {
        while (player != null && nowStatus == status.chasing)
        {
            Debug.Log("Chasing");
            agent.SetDestination(player.transform.position);
            if (SearchInSphere())
            {
                nowStatus = status.attack;
                agent.ResetPath();
                StartCoroutine(Aim());
                break;
            }
            else
            {
                nowStatus = status.chasing;
            }
            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator Aim()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0; // ©¿²¤ Y ¶bªº®t²§
        StartCoroutine(Attack());

        while (player != null && direction != Vector3.zero && nowStatus == status.attack)
        {
            if (IsPathObstructed() && !agent.hasPath)
            {
                MoveToNoObstalcePosition();
                yield return null;
            }
            else if(agent.hasPath)
            {
                yield return null;
            }

            Debug.Log("Aim");
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5.0f);
            direction = player.transform.position - transform.position;
            direction.y = 0; // ©¿²¤ Y ¶bªº®t²§

            if (!SearchInSphere())
            {
                nowStatus = status.chasing;
                StartCoroutine(SearchRoutine());
                yield break;
            }
            yield return null;
        } 
    }

    bool IsPathObstructed()
    {
        Ray ray = new Ray(transform.position, player.transform.position - transform.position);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Vector3.Distance(transform.position, player.transform.position));
        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red, 0.1f);
        if(hit.collider.gameObject.layer == player.layer)
        {
            agent.ResetPath();
            return false;
        }
        else
        {
            return true;
        }
    }

    void MoveToNoObstalcePosition()
    {
        Debug.Log("Reset Path");
        Vector3 directionToTarget = player.transform.position - transform.position;
        float angle = Random.Range(-90f, 90f);
        Vector3 direction = Quaternion.Euler(0, angle, 0) * directionToTarget.normalized;
        Vector3 RePathForObstacle = transform.position + direction * Random.Range(5f, 20f);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(RePathForObstacle, out hit, 20.0f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
        Debug.Log(hit.position);
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.5f);
        while (player != null && nowStatus == status.attack)
        {
            if(IsPathObstructed() && agent.hasPath)
            {
                yield return new WaitForSeconds(0.5f);
            }

            Debug.Log("Attack");
            yield return new WaitForSeconds(EnemyInfo.AttackSpeed);
        }
    }

    bool SearchInSphere()
    {
        Vector3 center = transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(center, EnemyInfo.AttackRange + EnemyInfo.AttackRangeOffset, searchLayer);

        foreach (Collider hitCollider in hitColliders)
        {
            GameObject foundObject = hitCollider.gameObject;
            Debug.Log("Found object: " + foundObject.name);
        }

        return hitColliders.Length > 0;
    }
}
