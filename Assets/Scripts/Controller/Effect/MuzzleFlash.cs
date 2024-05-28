using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    public GameObject followingAttackPoint;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(followingAttackPoint != null)
        {
            transform.position = followingAttackPoint.transform.position;
        }
    }
}
