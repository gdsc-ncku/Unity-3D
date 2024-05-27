using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestEnemyController : MonoBehaviour
{
    [SerializeField] ChestEnemy chestEnemy;
    [SerializeField] GameObject Height;
    private BoxCollider boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        float height = Height.transform.position.y - transform.position.y;
        boxCollider.center = new Vector3(0, height / 2, 0);
        boxCollider.size = new Vector3(1, height, 1);
    }

    public void InvokeChestEnemyAttack()
    {
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        chestEnemy.attack(gameObject);
        yield return null;
    }
}
