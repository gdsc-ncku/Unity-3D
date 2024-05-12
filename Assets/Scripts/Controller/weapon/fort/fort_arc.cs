using UnityEngine;
public class fort_arc: MonoBehaviour
{
    public float speed = 5f;
    public Transform fort; // 砲台
    public LayerMask whatIsEnemy; // 敵人的層
    public float searchRadius = 0.1f; // 尋找敵人的範圍

    void Update()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, searchRadius, whatIsEnemy);
        // GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
            Collider closestEnemy = null;
            float closestDistance = Mathf.Infinity;
            // 找到最近的敵人
            foreach (Collider enemy in enemies)
            {
                float distance = Vector3.Distance(fort.transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }
            if (closestEnemy != null)
            {
                Vector3 direction = closestEnemy.transform.position - transform.position;
                Quaternion toRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(fort.rotation, toRotation, speed * Time.deltaTime);
            }
        }

    }
}