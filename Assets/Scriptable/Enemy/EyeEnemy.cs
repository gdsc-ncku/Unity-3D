using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EyeEnemy", menuName = "Enemy/EyeEnemy", order = 2)]
public class EyeEnemy : EnemyScriptableObject
{
    public GameObject[] Bullets;
    public int BulletsPerGen;
    public float GenBulletsTime;

    private void OnValidate()
    {
        AttackTime = BulletsPerGen * GenBulletsTime;
    }

    public override void attack(GameObject enemy, float waittingTime)
    {
        float xOffset = Random.Range(-6f, 6f);
        float yOffset = Random.Range(0f, 5f) + 3f;
        float zOffset = Random.Range(-2f, 2f);

        Vector3 offset = new Vector3(xOffset, yOffset, zOffset);
        Vector3 Pos = enemy.transform.position + offset;

        int randomIndex = Random.Range(0, Bullets.Length);
        GameObject Bullet = Bullets[randomIndex];
        EyeEnemyBullet obj = Instantiate(Bullet, Pos, Bullet.transform.rotation).GetComponent<EyeEnemyBullet>();
        obj.Spawner = enemy;
        obj.waittingTime = waittingTime;
    }
}