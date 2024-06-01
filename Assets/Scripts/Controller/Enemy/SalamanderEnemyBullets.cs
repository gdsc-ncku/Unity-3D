using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalamanderEnemyBullets : MonoBehaviour
{
    public GameObject GenPos;
    public GameObject Spawner;
    [SerializeField] LayerMask bulletAim;
    [SerializeField] SalamanderEnemy EnemyInfo;
    [SerializeField] PlayerBattleValueScriptable PlayerInfo;
    [SerializeField] Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.velocity = Vector3.zero; // 清除初始速度
        rb.angularVelocity = Vector3.zero; // 清除初始角速度
        rb.AddForce(-GenPos.transform.up * 40f, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != gameObject.tag && other.transform.root.tag != Spawner.tag)
        {
            if (((1 << other.gameObject.transform.root.gameObject.layer) & bulletAim) != 0)
            {
                float effectiveDefense = Mathf.Max(PlayerInfo.Role.GetComponent<StudentDataManager>().studentData.Defense, EnemyInfo.AttackDamage * 0.1f);
                PlayerInfo.ReduceHealth(Mathf.Max(0, EnemyInfo.AttackDamage * (100f / (100f + effectiveDefense))));
                other.gameObject.transform.root.gameObject.GetComponent<Rigidbody>().AddForce(rb.velocity.normalized * rb.mass, ForceMode.Impulse);
            }

            Destroy(gameObject);
        }
    }
}
