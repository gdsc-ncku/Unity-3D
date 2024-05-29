using UnityEngine;

public class CactusEnemyController : MonoBehaviour
{
    [SerializeField] CactusEnemy cactusEnemy;

    public void InvokeCactusEnemyAttack()
    {
        cactusEnemy.attack(gameObject);
    }
}
