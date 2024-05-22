using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EyeEnemy", menuName = "Enemy/EyeEnemy", order = 2)]
public class EyeEnemy : EnemyScriptableObject
{
    public GameObject[] bullets;

    public override void attack(GameObject gameObject)
    {
        Debug.Log("Attack");
    }
}
