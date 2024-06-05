using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

enum status { chasing, stuck, repath, attack, die};
public class EnemyAI : MonoBehaviour
{
    public EnemyScriptableObject EnemyInfo;
    [SerializeField] EnemyScriptableObject WeaponDrops;
    [SerializeField] PlayerBattleValueScriptable BattleInfo;
    [SerializeField] LayerMask searchLayer;
    [SerializeField] Slider HealthBar;
    private Animator animator;
    private status nowStatus;
    private float Hp;
    private Vector3 lastPosition;
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
                nowStatus = status.die;
                StartCoroutine(EnemyDie());
            }
        }
    }

    IEnumerator EnemyDie()
    {
        if (gameObject.GetComponent<Collider>())
        {
            gameObject.GetComponent<Collider>().enabled = false;
        }
        else
        {
            gameObject.transform.GetChild(1).GetComponent<Collider>().enabled = false;
        }

        animator.Play("Die", 0, 0);
        yield return null;
        if (EnemyInfo.Drops.Length != 0 && Random.Range(0f, 1f) < EnemyInfo.DropsProbability)
        {
            GameObject drop = EnemyInfo.Drops[0];
            if(Random.Range(0f, 1f) < EnemyInfo.DropsProbability)
            {
                Instantiate(WeaponDrops.Drops[(int)Random.Range(0, WeaponDrops.Drops.Length)], transform.position, Quaternion.identity);
            }
            Instantiate(drop, transform.position, Quaternion.identity);
        }

        Destroy(gameObject, animator.GetCurrentAnimatorClipInfo(0).Length);
        yield break;
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
        lastPosition = transform.position;
        animator = GetComponent<Animator>();
        animator.speed = 1;
        agent = GetComponent<NavMeshAgent>();
        HealthBar.maxValue = EnemyInfo.Health;
        agent.speed = EnemyInfo.MoveSpeed;
        Health = EnemyInfo.Health;
        nowStatus = status.chasing;
        StartCoroutine(SearchRoutine());
        StartCoroutine(AvoidStuck());
    }

    IEnumerator AvoidStuck()
    {
        int stuckTime = 0;
        yield return new WaitForSeconds(1f);
        while(true)
        {
            if (Vector3.Distance(agent.transform.position, lastPosition) < 0.1f && nowStatus != status.attack)
            {
                //Debug.Log(gameObject.name + "Stuck");
                nowStatus = status.stuck;
                agent.ResetPath();
                Vector3 randomPoint = transform.position + Random.insideUnitSphere * 100;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, 500, NavMesh.AllAreas))
                {
                    agent.SetDestination(hit.position);
                }
                stuckTime++;
            }
            else
            {
                lastPosition = agent.transform.position;
                stuckTime = 0;
            }

            if(nowStatus == status.stuck && !agent.hasPath) 
            {
                nowStatus = status.chasing;
                StartCoroutine(SearchRoutine());
            }

            if(stuckTime > 10)
            {
                Vector3 randomPoint = transform.position + Random.insideUnitSphere * 10;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, 50, NavMesh.AllAreas))
                {
                    transform.position = hit.position;
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }

    //Chasing player
    IEnumerator SearchRoutine()
    {    
        //Debug.Log("Chasing");
        while (BattleInfo.Player != null && nowStatus == status.chasing)
        {
            animator.Play("Chasing", 0, 0);
            agent.speed = EnemyInfo.RunSpeed;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(BattleInfo.Player.transform.position, out hit, EnemyInfo.AttackRange, NavMesh.AllAreas))
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
        agent.speed = EnemyInfo.MoveSpeed;
        Vector3 direction = BattleInfo.Player.transform.position - transform.position;
        direction.y = 0; // ���� Y �b���t��
        StartCoroutine(Attack());

        while (BattleInfo.Player != null && direction != Vector3.zero && (nowStatus == status.attack || nowStatus == status.repath))
        {
            if (IsPathObstructed() && !agent.hasPath)
            {
                nowStatus = status.repath;
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
            nowStatus = status.attack;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5.0f);
            direction = BattleInfo.Player.transform.position - transform.position;
            direction.y = 0; // ���� Y �b���t��

            if (!SearchInSphere())
            {
                nowStatus = status.chasing;
                StartCoroutine(SearchRoutine());
                StopCoroutine(Attack());
                yield break;
            }
            yield return null;
        } 
    }

    //Detect if has obstacle between enemy and player
    bool IsPathObstructed()
    {
        Ray ray = new Ray(transform.position, BattleInfo.Player.transform.position - transform.position);
        RaycastHit[] hits = Physics.RaycastAll(ray, Vector3.Distance(transform.position, BattleInfo.Player.transform.position)); ;
        Debug.DrawRay(transform.position, BattleInfo.Player.transform.position - transform.position, Color.red, 0.1f);
        foreach(RaycastHit hit in hits)
        {
            if(hit.collider.gameObject.transform.root == gameObject.transform.root)
            {
                continue;
            }

            if (hit.collider.transform.root.gameObject.layer == BattleInfo.Player.layer)
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

        return false;
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
        while (BattleInfo.Player != null && (nowStatus == status.attack || nowStatus == status.repath))
        {
            if(IsPathObstructed() || agent.hasPath)
            {
                yield return new WaitForSeconds(0.5f);
                continue;
            }

            if (nowStatus == status.attack)
            {
                animator.SetFloat("Attack", 1 / EnemyInfo.AttackTime);
                animator.Play("Attacking", 0, 0);
            }

            //Wait for StateInfo update
            yield return null;
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            if (BattleInfo.Player != null && nowStatus == status.attack)
            {
                animator.Play("Idle", 0, 0);
            }
            else
            {
                yield break;
            }
            //Debug.Log("Attack");
            yield return new WaitForSeconds(EnemyInfo.AttackSpeed);
        }
    }

    //Search if player around enemy
    bool SearchInSphere()
    {
        Vector3 center = transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(center, EnemyInfo.AttackRange, searchLayer);

        foreach (Collider hitCollider in hitColliders)
        {
            GameObject foundObject = hitCollider.gameObject;
            //Debug.Log("Found object: " + foundObject.name);
        }

        return hitColliders.Length > 0;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.root.gameObject.CompareTag("bullet"))
        {
            ReduceHealth(collider.GetComponent<FPSCustomBullet>().AttackWeapon.ThisWeapon.damage * BattleInfo.Role.GetComponent<StudentDataManager>().studentData.AttackDamage);
        }
    }
}
