using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

enum status { chasing, attack, die};
public class EnemyAI : MonoBehaviour
{
    public EnemyScriptableObject EnemyInfo;
    [SerializeField] PlayerBattleValueScriptable BattleInfo;
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

            if(Hp <= 0)
            {
                Debug.Log("Enemy Die");
                gameObject.GetComponent<Collider>().enabled = false;
                animator.Play("Die", 0, 0);
                if(EnemyInfo.Drops.Length != 0 && Random.Range(0f, 1f) <= EnemyInfo.DropsProbability)
                {
                    Instantiate(EnemyInfo.Drops[Random.Range(0, EnemyInfo.Drops.Length)], transform);
                }

                Destroy(gameObject, animator.GetCurrentAnimatorClipInfo(0).Length);
                nowStatus = status.die;
            }
        }
    }

    public float GetHealth()
    {
        return Health;
    }

    public void ReduceHealth(float val)
    {
        Health -= val;
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
        
    }

    //Chasing player
    IEnumerator SearchRoutine()
    {
        animator.Play("Chasing", 0, 0);
        //Debug.Log("Chasing");
        while (BattleInfo.Player != null && nowStatus == status.chasing)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(BattleInfo.Player.transform.position, out hit, 20.0f, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }

            if (SearchInSphere())
            {
                nowStatus = status.attack;
                agent.ResetPath();
                StartCoroutine(Aim());
                yield break;
            }
            else
            {
                nowStatus = status.chasing;
            }
            yield return new WaitForSeconds(1.0f);
        }
    }

    //Let enemy orient to player
    IEnumerator Aim()
    {
        Vector3 direction = BattleInfo.Player.transform.position - transform.position;
        direction.y = 0; // ©¿²¤ Y ¶bªº®t²§
        StartCoroutine(Attack());

        while (BattleInfo.Player != null && direction != Vector3.zero && nowStatus == status.attack)
        {
            if (IsPathObstructed() && !agent.hasPath)
            {
                animator.Play("Walking", 0, 0);
                MoveToNoObstalcePosition();
                yield return null;
                continue;
            }
            else if(agent.hasPath)
            {
                yield return null;
                continue;
            }

            //Debug.Log("Aim");
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5.0f);
            direction = BattleInfo.Player.transform.position - transform.position;
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

    //Detect if has obstacle between enemy and player
    bool IsPathObstructed()
    {
        Ray ray = new Ray(transform.position, BattleInfo.Player.transform.position - transform.position);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Vector3.Distance(transform.position, BattleInfo.Player.transform.position));
        Debug.DrawRay(transform.position, BattleInfo.Player.transform.position - transform.position, Color.red, 0.1f);
        if(hit.collider.gameObject.layer == BattleInfo.Player.layer)
        {
            agent.ResetPath();
            //Debug.Log("No Obstacle");
            return false;
        }
        else
        {
            //Debug.Log("Has Obstacle");
            return true;
        }
    }

    //Repath
    void MoveToNoObstalcePosition()
    {
        //Debug.Log("Reset Path");
        Vector3 directionToTarget = BattleInfo.Player.transform.position - transform.position;
        float angle = Random.Range(-90f, 90f);
        Vector3 direction = Quaternion.Euler(0, angle, 0) * directionToTarget.normalized;
        Vector3 RePathForObstacle = transform.position + direction * Random.Range(5f, 20f);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(RePathForObstacle, out hit, 20.0f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
        //Debug.Log(hit.position);
    }

    //Attacking between every EnemyInfo.AttackSpeed second
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.5f);
        while (BattleInfo.Player != null && nowStatus == status.attack)
        {
            if(IsPathObstructed() || agent.hasPath)
            {
                //Debug.Log("return attack");
                yield return new WaitForSeconds(0.5f);
                continue;
            }

            animator.SetFloat("Attack", 1 / EnemyInfo.AttackTime);
            animator.Play("Attacking", 0, 0);
            //Debug.Log("Attack");
            yield return new WaitForSeconds(EnemyInfo.AttackSpeed + EnemyInfo.AttackTime);
        }
    }

    //Search if player around enemy
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
