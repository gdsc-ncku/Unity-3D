using UnityEngine;

public class PlayerCam1 : MonoBehaviour
{
    [SerializeField] private float senX;
    [SerializeField] private float senY;

/* 取消合併專案 'Assembly-CSharp.Player' 的變更
之前:
    public Transform orientation;
    
    float xRotation;
之後:
    public Transform orientation;

    float xRotation;
*/
    public Transform orientation;

    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //滑鼠輸入
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * senX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * senX;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //視角旋轉
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
