using UnityEngine;

public class MushroomEnemyController : MonoBehaviour
{
    [SerializeField] MushroomEnemy mushroomEnemy;

    public void InvokeMushroomEnemyAttack()
    {
        mushroomEnemy.attack(gameObject);
    }
}
