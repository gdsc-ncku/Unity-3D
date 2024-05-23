using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class EyeEnemyBullet : MonoBehaviour
{
    public GameObject Spawner;
    public float waittingTime;
    [SerializeField] EnemyScriptableObject EnemyInfo;
    [SerializeField] PlayerBattleValueScriptable PlayerInfo;
    [SerializeField] LayerMask bulletAim;
    [SerializeField] Rigidbody rb;
    private float rotateSpeed;
    private Vector3 offset;
    private bool Attacking = false;

    void Start()
    {
        Destroy(this, 3.0f);
        offset = transform.position - Spawner.transform.position;
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero; // 清除初始速度
        rb.angularVelocity = Vector3.zero; // 清除初始角速度
        rotateSpeed = Random.Range(1f, 5f);
        StartCoroutine(attack());
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.RotateAround(transform.position, Vector3.up, rotateSpeed);
        if (!Attacking)
        {
            transform.position = Spawner.transform.position + offset;
        }
    }

    IEnumerator attack()
    {
        yield return new WaitForSeconds(waittingTime);
        Attacking = true;
        Collider[] hitColliders = Physics.OverlapSphere(Spawner.transform.position, EnemyInfo.AttackRange, bulletAim);
        
        if(hitColliders.Length > 0)
        {
            Debug.Log("Attack Player");
            rb.AddForce((hitColliders[0].transform.position - transform.position).normalized * Random.Range(10f, 15f), ForceMode.Impulse);
        }
        else
        {
            StartCoroutine(attack());
            Debug.Log("No Player");
        }
        yield break;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.name != gameObject.name)
        {
            if(((1 << collision.collider.gameObject.layer) & bulletAim) != 0)
            {
                PlayerInfo.ReduceHealth(Spawner.GetComponent<EnemyAI>().EnemyInfo.AttackDamage);
            }

            Destroy(gameObject);
        }
    }
}
