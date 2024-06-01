using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StingRayEnemyBullets : MonoBehaviour
{
    public GameObject Spawner;
    private Rigidbody rb;
    [SerializeField] LayerMask bulletAim;
    [SerializeField] PlayerBattleValueScriptable PlayerInfo;
    [SerializeField] StingRayEnemy stingRayEnemy;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        float randomX = Random.Range(0f, 360f);
        float randomY = Random.Range(0f, 360f);
        float randomZ = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(randomX, randomY, randomZ);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, stingRayEnemy.AttackRange, bulletAim);
        if (hitColliders.Length > 0)
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
        if (other.gameObject == Spawner || ((1 << other.gameObject.layer) & gameObject.layer) != 0 || other.gameObject.tag == gameObject.tag)
        {
            return;
        }

        if (((1 << other.gameObject.transform.root.gameObject.layer) & bulletAim) != 0)
        {
            PlayerInfo.ReduceHealth(stingRayEnemy.AttackDamage);
            other.gameObject.transform.root.gameObject.GetComponent<Rigidbody>().AddForce(rb.velocity.normalized * rb.mass * Random.Range(5f, 10f), ForceMode.Impulse);
        }

        Destroy(gameObject);
    }
}
