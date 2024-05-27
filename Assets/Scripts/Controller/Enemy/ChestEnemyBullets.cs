using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestEnemyBullets : MonoBehaviour
{
    public GameObject Spawner;
    private Rigidbody rb;
    [SerializeField] LayerMask bulletAim;
    [SerializeField] PlayerBattleValueScriptable PlayerInfo;
    [SerializeField] ChestEnemy chestEnemy;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            PlayerInfo.ReduceHealth(Spawner.GetComponent<EnemyAI>().EnemyInfo.AttackDamage);
            other.gameObject.transform.root.gameObject.GetComponent<Rigidbody>().AddForce(rb.velocity.normalized * rb.mass * Random.Range(5f, 10f), ForceMode.Impulse);
        }

        Destroy(gameObject);
    }
}
