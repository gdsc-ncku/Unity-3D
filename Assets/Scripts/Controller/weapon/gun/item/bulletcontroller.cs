using UnityEngine;

public class bulletcontroller : MonoBehaviour
{
    //武器屬性
    [SerializeField]
    WeaponData gun;
    private float downtime = 0.8f; //落下時間(高度)
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        Invoke("displayBullet", 0.05f);
        GetComponent<Rigidbody>().AddForce(transform.forward * 8000);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        //碰撞後的傷害可以放在這裡
    }
    // // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, downtime);
    }

    private void displayBullet()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}
