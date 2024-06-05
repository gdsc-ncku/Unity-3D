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

        if (((1 << other.gameObject.layer) & layerMask) != 0)
        {
            float effectiveDefense = Mathf.Max(playerInfo.Role.GetComponent<StudentDataManager>().studentData.Defense, mushroomEnemy.AttackDamage * 0.1f);
            playerInfo.ReduceHealth(Mathf.Max(0, mushroomEnemy.AttackDamage * (100f / (100f + effectiveDefense))));
        }
    }
}
