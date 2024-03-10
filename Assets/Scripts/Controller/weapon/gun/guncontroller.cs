using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guncontroller : MonoBehaviour
{
    //武器屬性
    [SerializeField]
    WeaponData gun;
    //開槍點
    public Transform shotpoint;
    //子彈
    public GameObject bulletPre;
    //計時器
    private bool shooting = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !shooting) {
            shooting = true;
            StartCoroutine(Shoot());
            
        }
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(gun.attackSpeedMultiplier);
        Instantiate(bulletPre, shotpoint.position, shotpoint.rotation);
        shooting = false;
    }
}
