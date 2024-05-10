using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "Enemy/EnemyBasicInformation", order = 1)]
public class EnemyScriptableObject : ScriptableObject
{
    public float InitSearchRange;
    public float AttackRange;
    public float AttackSpeed;
    public float AttackDamage;
    public float MoveSpeed;
    public float Health;
    public float JumpForce;

    public void attack()
    {

    }
}
