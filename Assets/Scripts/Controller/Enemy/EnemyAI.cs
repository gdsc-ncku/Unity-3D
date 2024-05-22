using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

enum status { chasing, attack, die};
public class EnemyAI : MonoBehaviour
{
    [SerializeField] EnemyScriptableObject EnemyInfo;
    [SerializeField] GameObject player;
    [SerializeField] LayerMask searchLayer;
    [SerializeField] Slider HealthBar;
    private Animator animator;
    private status nowStatus;
    private float Hp;
    private float Health 
    { 
        get 
        {
            return Hp; 
        }
        
        set
        {
            Hp = value;
            HealthBar.value = Hp;

            if(Hp == 0)
            {
                Debug.Log("Enemy Die");
                nowStatus = status.die;
            }
        }
    }

    public float GetHealth()
    {
        return Health;
    }

    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        HealthBar.maxValue = EnemyInfo.Health;
        agent.speed = EnemyInfo.MoveSpeed;
        Health = EnemyInfo.Health;
        nowStatus = status.chasing;
        StartCoroutine(SearchRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Health -= Random.Range(5.0f, 10.0f);
        }
    }

    IEnumerator SearchRoutine()
    {
        //animator.Play("Chasing", 0, 0);
        //Debug.Log("Chasing");
        while (player != null && nowStatus == status.chasing)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(player.transform.position, out hit, 20.0f, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }

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
                //animator.Play("Walking", 0, 0);
                MoveToNoObstalcePosition();
                yield return null;
            }
            else if(agent.hasPath)
            {
                yield return null;
            }

            //Debug.Log("Aim");
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
        //Debug.Log("Reset Path");
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

            //animator.Play("Attacking", 0, 0);
            //Debug.Log("Attack");
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
            //Debug.Log("Found object: " + foundObject.name);
        }

        return hitColliders.Length > 0;
    }
}
