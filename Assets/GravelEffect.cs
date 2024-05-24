using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravelEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity);

        for(int i = 0; i < transform.childCount; i++) 
        {
            transform.GetChild(i).gameObject.transform.position = hit.point;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
