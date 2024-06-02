using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamageController : MonoBehaviour
{
    [SerializeField] PlayerBattleValueScriptable BattleInfo;
    public GameObject bullet;
    public RaycastHit hit;
    private void Start()
    {
        SphereCollider collider = GetComponent<SphereCollider>();
        float Radius = collider.radius, OriginDamage = bullet.GetComponent<FPSCustomBullet>().AttackWeapon.ThisWeapon.damage * BattleInfo.Role.GetComponent<StudentDataManager>().studentData.AttackDamage;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Radius);
        EnemyAI ai;

        foreach (var hitCollider in hitColliders)
        {
            float currentDistance = (hitCollider.gameObject.transform.position - (transform.position + collider.center)).magnitude;
            if (hitCollider.transform != hit.transform && hitCollider.gameObject.TryGetComponent(out ai))
            {
                Debug.Log($"{hitCollider.gameObject.name}, {(1f - (currentDistance / Radius))}");
                ai.ReduceHealth(OriginDamage * (1f - (currentDistance / Radius)));
            }
        }
    }
}
