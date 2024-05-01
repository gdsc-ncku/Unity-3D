using UnityEngine;
using Cinemachine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private PlayerBasicInformationScriptable PlayerMove;
    private CinemachineBrain cinemachineBrain;
    private float pickUpRange = 2f; 

    void Start()
    {
        // Get cinemachinebrain componet
        cinemachineBrain = GetComponent<CinemachineBrain>();
    }

    void Update()
    {
        
    }

    public void pickUp()
    {
        // Get the position of main camera
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 cameraDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;

        // Emit a ray to detect item
        if (Physics.Raycast(cameraPosition, cameraDirection, out hitInfo, pickUpRange))
        {
            if (hitInfo.collider.CompareTag("Item"))
            {
                Debug.Log("Picked up: " + hitInfo.collider.name);
                Destroy(hitInfo.collider.gameObject);
            }
        }
    }
}
