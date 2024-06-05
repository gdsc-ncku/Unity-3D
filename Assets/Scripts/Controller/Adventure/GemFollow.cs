using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemFollow : MonoBehaviour
{
    [SerializeField] LayerMask followObject;
    [SerializeField] float moveSpeed, radius;
    [SerializeField] PlayerBattleValueScriptable PlayerBattleValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, followObject);

        if (colliders.Length != 0 )
        {
            transform.position = Vector3.MoveTowards(transform.position, colliders[0].gameObject.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if((1 << collision.collider.gameObject.layer & followObject) != 0)
        {
            PlayerBattleValue.Soul++;
            Destroy(gameObject);
        }
    }
}
