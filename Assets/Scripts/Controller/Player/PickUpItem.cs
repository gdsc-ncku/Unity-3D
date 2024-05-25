using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using System.Collections;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private PlayerBasicInformationScriptable PlayerMove;
    [SerializeField] Weapons Weapon;
    private float pickUpRange = 4f;

    private void OnEnable()
    {
        if (PlayerMove == null || PlayerMove.playerControl == null)
        {
            Debug.Log("GunBasic: Setting informationScriptable and playerControl first");
            StartCoroutine("DebugPlayerControl");
            return;
        }
    }

    private void OnDisable()
    {
        if (PlayerMove == null)
        {
            Debug.Log("GunBasic: Setting informationScriptable first");
            return;
        }

        PlayerMove.playerControl.Player.PickUp.started -= pickUp;
    }

    private void OnDestroy()
    {
        if (PlayerMove == null)
        {
            Debug.Log("GunBasic: Setting informationScriptable first");
            return;
        }

        PlayerMove.playerControl.Player.PickUp.started -= pickUp;
    }

    IEnumerator DebugPlayerControl()
    {
        yield return new WaitForSeconds(1);
        PlayerMove.playerControl.Player.PickUp.started += pickUp;
    }

    void Update()
    {
        
    }

    public void pickUp(InputAction.CallbackContext context)
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
            else if(hitInfo.collider.CompareTag("Weapon"))
            {
                changeItems(Weapon.HoldingWeapon.transform.GetChild(0).gameObject, hitInfo.collider.gameObject);
            }
        }
    }

    private void changeItems(GameObject ItemA, GameObject ItemB)
    {
        // 保存物件A的位置、旋轉和縮放
        Vector3 positionA = ItemA.transform.position;
        Quaternion rotationA = ItemA.transform.rotation;
        Vector3 scaleA = ItemA.transform.localScale;
        Transform parentA = ItemA.transform.parent;

        // 保存物件B的位置、旋轉和縮放
        Vector3 positionB = ItemB.transform.position;
        Quaternion rotationB = ItemB.transform.rotation;
        Vector3 scaleB = ItemB.transform.localScale;
        Transform parentB = ItemB.transform.parent;

        // 將物件A的位置、旋轉和縮放設置為物件B的位置、旋轉和縮放
        ItemA.transform.position = positionB;
        ItemA.transform.rotation = rotationB;
        ItemA.transform.localScale = scaleB;
        ItemA.transform.parent = parentB;

        // 將物件B的位置、旋轉和縮放設置為物件A的位置、旋轉和縮放
        ItemB.transform.position = positionA;
        ItemB.transform.rotation = rotationA;
        ItemB.transform.localScale = scaleA;
        ItemB.transform.parent = parentA;

        if(Weapon.HoldingWeapon == Weapon.MainWeapon)
        {
            Weapon.changeToMainWeapon();
        }
        else
        {
            Weapon.changeToSecondWeapon();
        }
    }
}
