using System.Collections;
using UnityEngine;


public class guncontroller_light : MonoBehaviour
{
    //武器屬性
    [SerializeField]
    WeaponData gun;
    public int damage;
    public float timeBetweenShooting, spread, range, reloadtime, timebetweenshots;
    // public int bulletsPertag;
    public bool allowButtonHold;
    int bulletLeft;
    public Camera fpsCam;
    public Transform playerBody;
    public Transform attackPoint;
    public Transform internPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    private LineRenderer lineRenderer;//繪製射線
    // Start is called before the first frame update
    
    // public Camshake camShake;
    public GameObject muzzleFlash,bulletHoleGraphic;
    private Ray ray; 
    bool shooting,readyToshoot,reloading;
    Vector3 recoil;
    void Start(){
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.02f;
        lineRenderer.positionCount = 2;
    }
    private void Awake(){
        readyToshoot = true;
    }
    private void Update(){
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(ray.direction);
        MyInput();
    }
    private void MyInput()
    {
        if (allowButtonHold)
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);
        if (readyToshoot && shooting)
        {
            shoot();
        }
    }
    private void shoot()
    {
        readyToshoot = false;
        float x = UnityEngine.Random.Range(-spread, spread);
        float y = UnityEngine.Random.Range(-spread, spread);
        Ray ray = new Ray(internPoint.transform.position, fpsCam.transform.forward);
        RaycastHit hit;
        //射線渲染
        lineRenderer.SetPosition(0,attackPoint.position);
        lineRenderer.SetPosition(1,ray.GetPoint(range));
        //
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit, range, whatIsEnemy))
        {
            targetPoint = hit.point;
            Debug.Log(hit.collider.name);
        }
        else
            targetPoint = ray.GetPoint(75); //Just a point far away from the player

        //Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;
        float recoilAmount = 5.0f; // Change this to adjust the amount of recoil

        recoil = new Vector3(recoilAmount,recoilAmount, 0);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + recoil);
        
        StartCoroutine(RecoilRecovery());
        Instantiate(muzzleFlash,attackPoint.position,Quaternion.identity);
        Invoke("ResetShot",timeBetweenShooting);
    }
    IEnumerator RecoilRecovery(){
        readyToshoot=false;
        float recoverSpeed =0.8f;
        while (recoil.magnitude > 0.01f){
            recoil = Vector3.Lerp(recoil,Vector3.zero,recoverSpeed*Time.deltaTime);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles - recoil);
            yield return null;
        }
        recoil= Vector3.zero;
        readyToshoot=true;
    }
    private void ResetShot(){
        readyToshoot = true;
    }
}
