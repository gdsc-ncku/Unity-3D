using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CactusEnemy", menuName = "Enemy/CactusEnemy", order = 4)]
public class CactusEnemy : EnemyScriptableObject
{
    public float BulletChargingTime;
    public float BulletZoomInEverySecond;
    public float BulletFinalSize;
    
    private void OnValidate()
    {
        base.OnValidate();
        BulletFinalSize = BulletChargingTime * BulletZoomInEverySecond;
    }

    public override void attack(GameObject enemy)
    {
        Instantiate(Bullets[Random.Range(0, Bullets.Length)], enemy.transform.position + new Vector3(0, BulletZoomInEverySecond, 0), enemy.transform.rotation).GetComponent<CactusEnemyBullets>().Spawner = enemy;
    }
}
