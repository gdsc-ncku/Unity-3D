using UnityEngine;

public class MushroomEnemyController : MonoBehaviour
{
    [SerializeField] MushroomEnemy mushroomEnemy;
    [SerializeField] PlayerBattleValueScriptable playerInfo;
    [SerializeField] LayerMask layerMask;

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.collider.gameObject.layer) & layerMask) != 0)
        {
            playerInfo.ReduceHealth(mushroomEnemy.AttackDamage);
        }
    }
}
