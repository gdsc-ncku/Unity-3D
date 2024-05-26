using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "Enemy/EnemyBasicInformation", order = 1)]
public class EnemyScriptableObject : ScriptableObject
{
    public GameObject[] Drops;
    public float AttackRangeOffset;
    public float AttackRange;
    //Time between every new attack
    public float AttackSpeed;
    //Time between 2 bullets genarate
    public float AttackTime;
    public float AttackDamage;
    public float MoveSpeed;
    public float Health;
    public virtual void attack(GameObject gameObject, float waittingTime)
    {
        
    }
}
