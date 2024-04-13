using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guncontroller_light : MonoBehaviour
{
    //武器屬性
    [SerializeField]
    WeaponData gun;
    public int damage;
    public float timeBetweenShooting,spread,range,reloadtime,timebetweenshots;
    // public int bulletsPertag;
    public bool allowButtonHold;
    int bulletLeft;
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    // Start is called before the first frame update
    
    // public Camshake camShake;
    public GameObject muzzleFlash,bulletHoleGraphic;
    bool shooting,readyToshoot,reloading;
    private void Awake(){
        readyToshoot = true;
    }
    private void Update(){
        MyInput();
    }
    private void MyInput(){
        if(allowButtonHold){
            shooting=Input.GetKeyDown(KeyCode.Mouse0);
        }
        else shooting=Input.GetKeyDown(KeyCode.Mouse0);
        if(readyToshoot && shooting){
            shoot();
        }
    }
    private void shoot(){
        readyToshoot = false;
        float x = UnityEngine.Random.Range(-spread, spread);
        float y = UnityEngine.Random.Range(-spread, spread);
        Ray ray = new Ray(fpsCam.transform.position, fpsCam.transform.forward);
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit,range,whatIsEnemy)){
            targetPoint = hit.point;
        }
        else
            targetPoint = ray.GetPoint(75); //Just a point far away from the player

        //Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;
        Debug.DrawLine(transform.position, transform.forward,Color.white);
        Instantiate(muzzleFlash,attackPoint.position,Quaternion.identity);
        Invoke("ResetShot",timeBetweenShooting);
    }
    private void ResetShot(){
        readyToshoot = true;
    }
}
