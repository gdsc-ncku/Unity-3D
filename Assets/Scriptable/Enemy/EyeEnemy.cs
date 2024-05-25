using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EyeEnemy", menuName = "Enemy/EyeEnemy", order = 2)]
public class EyeEnemy : EnemyScriptableObject
{
    public GameObject[] Bullets;
    public int BulletsPerGen;

    public override void attack(GameObject enemy, float waittingTime)
    {
        float xOffset = Random.Range(-3f, 3f);
        float yOffset = Random.Range(0f, 2f) + 2f;
        float zOffset = Random.Range(-1f, 1f);

        Vector3 offset = new Vector3(xOffset, yOffset, zOffset);
        Vector3 Pos = enemy.transform.position + offset;

        GameObject Bullet = Bullets[Random.Range(0, Bullets.Length)];
        EyeEnemyBullet obj = Instantiate(Bullet, Pos, Bullet.transform.rotation).GetComponent<EyeEnemyBullet>();
        obj.Spawner = enemy;
        obj.waittingTime = waittingTime;

    }
}