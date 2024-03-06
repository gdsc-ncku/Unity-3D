using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletcontroller : MonoBehaviour
{
    //武器屬性
    [SerializeField]
    WeaponData gun;
    private float downtime=0.8f; //落下時間(高度)
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward*8000);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        //碰撞後的傷害可以放在這裡
    }
    // // Update is called once per frame
    void Update()
    {
        Destroy(gameObject,downtime);
    }
}
