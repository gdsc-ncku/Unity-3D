using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MushroomEnemy", menuName = "Enemy/MushroomEnemy", order = 5)]
public class MushroomEnemy : EnemyScriptableObject
{
    public override void attack(GameObject enemy)
    {
        Instantiate(Bullets[Random.Range(0, Bullets.Length)], enemy.transform.position, enemy.transform.rotation).GetComponent<ChestEnemyBullets>().Spawner = enemy;
    }
}
