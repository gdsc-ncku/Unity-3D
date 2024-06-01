using UnityEngine;

[CreateAssetMenu(fileName = "StingRayEnemy", menuName = "Enemy/StingRayEnemy", order = 6)]
public class StingRayEnemy : EnemyScriptableObject
{
    public override void attack(GameObject enemy)
    {
        int randomIndex = Random.Range(0, Bullets.Length);
        GameObject Bullet = Bullets[randomIndex];
        StingRayEnemyBullets obj = Instantiate(Bullet, enemy.transform.position + new Vector3(0, 3f, 1f), Bullet.transform.rotation).GetComponent<StingRayEnemyBullets>();
        obj.Spawner = enemy;
    }
}
