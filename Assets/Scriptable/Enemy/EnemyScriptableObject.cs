using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "Enemy/EnemyBasicInformation", order = 1)]
public class EnemyScriptableObject : ScriptableObject
{
    public GameObject[] Drops;
    public float DropsProbability;
    public float AttackRange;
    //Time between every new attack
    public float AttackSpeed;
    //Time between 2 bullets genarate
    public float AttackTime;
    public float AttackDamage;
    public float MoveSpeed;
    public float RunSpeed;
    public float Health;
    public GameObject[] Bullets;

    protected virtual void OnValidate()
    {
        if (DropsProbability > 1)
        {
            DropsProbability = 1;
        }
        else if (DropsProbability < 0)
        {
            DropsProbability = 0;
        }
    }

    //Polymorphism function
    public virtual void attack(GameObject enemy)
    {

    }

    public virtual void attack(GameObject gameObject, float waittingTime)
    {

    }
}
