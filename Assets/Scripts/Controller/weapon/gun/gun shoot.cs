using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gunshoot : MonoBehaviour
{
    // bullet
    public GameObject bullet;

    //bullet force
    public float shootForce, upwardForce;

    //Gun states
    public float timeBtShooting, spread, reloadTime, timeBtShots;
    public int magzinesize,bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    //states
    bool shooting, isready, reloading;
    
    //reference
    public Camera fpsCam;
    public Transform attackPoint;

    // Graphics
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;

    // use?
    public bool allowInvoke = true;
    
    //每次開始使用槍的時候但夾都是滿的
    private void Awake()
    {
        bulletsLeft = magzinesize; //剩下多少子彈 = 彈夾大小
        isready = true;
    }

    private void Update()
    {
        MyInput();  

        //Set ammo display, if it exists?
        if(ammunitionDisplay !=null)
        {
            ammunitionDisplay.SetText(bulletsLeft/bulletsPerTap + " / "+magzinesize/bulletsPerTap);
        }      
    }

    private void MyInput()
    {
        //check if allowed to hold down button and take corresponding input
        if (allowButtonHold)//如果按著左鍵
        {
                shooting =Input.GetKey(KeyCode.Mouse0); //就代表可以射擊
        }
        else{
            shooting = Input.GetKeyDown(KeyCode.Mouse0); //或者是每次按下滑鼠左鍵的時候都會發射()
        }

        //Reload
        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft <magzinesize && !reloading)
        {
            Reload();
        }

        // Reload automaticlly when trying to shoot without ammo
        if(isready && shooting && !reloading && bulletsLeft <=0)
        {
            Reload();
        }

        //shooting
        if( isready && shooting && !reloading && bulletsLeft>0)
        // 必須要是準備好開槍且按下射擊鍵 不可以正在重新裝子彈 還有子彈的時候可以發射
        {
            bulletsShot=0; //一開始已經射出0發
            Shoot();
        }
    }

    private void Shoot()
    {
        isready =false;
        
        //在第一人稱的射擊遊戲裡面 我們會把準心設在螢幕正中心，也就是說人物到準心的方向 就是我們想要的向量
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f , 0.5f , 0));
        RaycastHit hit; //判斷有沒有打目標過 return bool

        //check if ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit)) //射線的起點/hit碰到的物件
        {
            targetPoint = hit.point;
        }
        else{// 射在空氣裡面的就隨便射一個離自己很遠的位置
            targetPoint =ray.GetPoint(75);
        }

        //attackPoint to targetPoint的方向(座標相減)
        Vector3 directionWithoutSpeed = targetPoint - attackPoint.position;

        //?
        float x = Random.Range(-spread ,spread);
        float y = Random.Range(-spread ,spread);
        //Calculate new direction with Speed
        Vector3 directionWithSpeed = directionWithoutSpeed+ new Vector3(x,y,0);
        //?
        //生成心的子彈
        GameObject currentBullet = Instantiate(bullet, attackPoint.position,Quaternion.identity);//生成子彈在指定位置(生成子彈,生成位置,不選轉)
        // 把要射出的子彈 面向你要的方向 normalize是把向量轉成單位向量(/向量長度)
        currentBullet.transform.forward =directionWithSpeed.normalized;

        // Add force to bullet 
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpeed.normalized* shootForce, ForceMode. Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up*shootForce, ForceMode.Impulse);

        //Instatantiate Muzzle flash
        if(muzzleFlash != null)
        {
            Instantiate(muzzleFlash, attackPoint.position,Quaternion.identity);   
        }

        bulletsLeft--;
        bulletsShot++;

        if(allowInvoke)
        {
            Invoke("ResetShot", timeBtShooting);
            //只能喚醒一次
            allowInvoke = false;
        }
        if(bulletsShot < bulletsPerTap && bulletsLeft>0)
        {
            Invoke("Shoot",timeBtShots);
        }
    }
    private void ResetShot()
    {
        isready =true;
        allowInvoke =true;
    }

    private void Reload()
    {
        reloading=true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinish() 
    {
        bulletsLeft =magzinesize;
        reloading=false;
        
    }
     
    
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
