using UnityEngine;

public class ChestEnemyBullets : MonoBehaviour
{
    public GameObject Spawner;
    private Rigidbody rb;
    private float damage;
    [SerializeField] LayerMask bulletAim;
    [SerializeField] PlayerBattleValueScriptable PlayerInfo;
    [SerializeField] ChestEnemy chestEnemy;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        float randomX = Random.Range(0f, 360f);
        float randomY = Random.Range(0f, 360f);
        float randomZ = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(randomX, randomY, randomZ);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, chestEnemy.AttackRange, bulletAim);
        if(hitColliders.Length > 0 )
        {
            rb.AddForce(rb.mass * Random.Range(20f, 25f) * (hitColliders[0].transform.position - transform.position).normalized, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(rb.mass * Random.Range(20f, 25f) * Spawner.transform.forward, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Spawner || ((1 << other.gameObject.layer) & gameObject.layer) != 0)
        {
            return;
        }

        if (((1 << other.gameObject.transform.root.gameObject.layer) & bulletAim) != 0)
        {
            float effectiveDefense = Mathf.Max(PlayerInfo.Role.GetComponent<StudentDataManager>().studentData.Defense, chestEnemy.AttackDamage * 0.1f);
            PlayerInfo.ReduceHealth(Mathf.Max(0, chestEnemy.AttackDamage * (100f / (100f + effectiveDefense))));
            other.gameObject.transform.root.gameObject.GetComponent<Rigidbody>().AddForce(rb.velocity.normalized * rb.mass * Random.Range(5f, 10f), ForceMode.Impulse);
        }

        Destroy(gameObject);
    }
}
