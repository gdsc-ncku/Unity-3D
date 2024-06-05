using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SalamanderEnemy", menuName = "Enemy/SalamanderEnemy", order = 6)]
public class SalamanderEnemy : EnemyScriptableObject
{
    public override void attack(GameObject enemy, GameObject GenPos)
    {
        SalamanderEnemyBullets bullet = Instantiate(Bullets[Random.Range(0, Bullets.Length)], GenPos.transform.position - GenPos.transform.up * 0.5f, enemy.transform.rotation).GetComponent<SalamanderEnemyBullets>();
        bullet.Spawner = enemy;
        bullet.GenPos = GenPos;
    }
}
