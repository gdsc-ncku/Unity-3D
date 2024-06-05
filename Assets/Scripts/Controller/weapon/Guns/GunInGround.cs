using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInGround : MonoBehaviour
{
    Rigidbody rb;
    private void OnEnable()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        GetComponent<Collider>().isTrigger = false;
    }
    private void OnDisable()
    {
        rb.isKinematic = true;
        GetComponent<Collider>().isTrigger = true;
    }
}
