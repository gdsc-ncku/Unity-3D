using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flash : MonoBehaviour
{
     void Start()
    {
        Invoke("DestroySpark", 5f);
    }

    void DestroySpark()
    {
        Destroy(gameObject);
    }
}
