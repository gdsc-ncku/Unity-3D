using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "Enemy/EnemyBasicInformation", order = 1)]
public class EnemyScriptableObject : ScriptableObject
{
    public float AttackRangeOffset;
    public float AttackRange;
    public float AttackSpeed;
    public float AttackDamage;
    public float MoveSpeed;
    public float Health;
    public virtual void attack(GameObject gameObject)
    {
        
    }
}
