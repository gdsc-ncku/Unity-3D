using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletcontroller : MonoBehaviour
{
    //武器屬性
    [SerializeField]
    WeaponData gun;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward*8000);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject,0.5f);
    }
    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
