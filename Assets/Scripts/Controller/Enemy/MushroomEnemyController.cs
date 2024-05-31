using UnityEngine;

public class MushroomEnemyController : MonoBehaviour
{
    [SerializeField] MushroomEnemy mushroomEnemy;
    [SerializeField] PlayerBattleValueScriptable playerInfo;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Collider attackDetectCollider;
    [SerializeField] bool triggerOpen;

    public void InvokeMushroomAttack()
    {
        attackDetectCollider.enabled = true;
    }

    public void FinishMushroomAttack()
    {
        attackDetectCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!triggerOpen)
        {
            return;
        }

        Debug.Log(other.gameObject.name);
        if (((1 << other.gameObject.layer) & layerMask) != 0)
        {
            playerInfo.ReduceHealth(mushroomEnemy.AttackDamage);
        }
    }
}
