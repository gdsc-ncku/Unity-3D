using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChestEnemy", menuName = "Enemy/ChestEnemy", order = 3)]
public class ChestEnemy : EnemyScriptableObject
{
    public override void attack(GameObject enemy)
    {
        Instantiate(Bullets[Random.Range(0, Bullets.Length)], enemy.transform.position + new Vector3(0, 1.5f, 0), enemy.transform.rotation).GetComponent<ChestEnemyBullets>().Spawner = enemy;
    }
}
