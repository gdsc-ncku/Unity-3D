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
    private float timer =0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer+= Time.deltaTime;
        if (timer>gun.attackSpeedMultiplier && Input.GetMouseButton(0)) {
            timer =0;
            Instantiate(bulletPre,shotpoint.position,shotpoint.rotation);
        }
    }
}
