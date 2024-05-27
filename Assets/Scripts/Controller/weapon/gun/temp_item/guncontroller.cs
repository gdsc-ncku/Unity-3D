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
    [SerializeField] Vector3 offset = new(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {

    }
}
