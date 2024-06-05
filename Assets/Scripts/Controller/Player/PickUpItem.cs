using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private PlayerBasicInformationScriptable PlayerMove;
    [SerializeField] private PlayerBattleValueScriptable PlayerBattleValue;
    [SerializeField] Weapons Weapon;
    private float pickUpRange = 8f;

    private void OnEnable()
    {
        if (PlayerMove == null || PlayerMove.playerControl == null)
        {
            Debug.Log("PlayerBasicInformationScriptable disapear");
            return;
        }

        PlayerMove.playerControl.Player.PickUp.started += pickUp;
    }

    private void OnDisable()
    {
        if (PlayerMove == null || PlayerMove.playerControl == null)
        {
            Debug.Log("PlayerBasicInformationScriptable disapear");
            return;
        }

        PlayerMove.playerControl.Player.PickUp.started -= pickUp;
    }

    private void OnDestroy()
    {
        if (PlayerMove == null || PlayerMove.playerControl == null)
        {
            Debug.Log("PlayerBasicInformationScriptable disapear");
            return;
        }

        PlayerMove.playerControl.Player.PickUp.started -= pickUp;
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
            if (hitInfo.collider.CompareTag("Gem"))
            {
                PlayerBattleValue.Soul++;
                Destroy(hitInfo.collider.gameObject);
            }
            else if (hitInfo.collider.CompareTag("Weapon"))
            {
                hitInfo.collider.gameObject.GetComponent<GunInGround>().enabled = false;
                if (Weapon.transform.GetChild(0).childCount == 0)
                {
                    hitInfo.collider.gameObject.transform.parent = Weapon.transform.GetChild(0);
                    hitInfo.collider.gameObject.transform.localPosition = Vector3.zero;
                    hitInfo.collider.gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
                }
                else if (Weapon.transform.GetChild(1).childCount == 0)
                {
                    hitInfo.collider.gameObject.transform.parent = Weapon.transform.GetChild(1);
                    hitInfo.collider.gameObject.transform.localPosition = Vector3.zero;
                    hitInfo.collider.gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
                }
                else
                {
                    changeItems(Weapon.HoldingWeapon.transform.GetChild(0).gameObject, hitInfo.collider.gameObject);
                }
            }
        }
    }

    private void changeItems(GameObject HandleItemA, GameObject GroundItemB)
    {
        Vector3 positionA = HandleItemA.transform.position;
        Quaternion rotationA = HandleItemA.transform.rotation;
        Vector3 scaleA = HandleItemA.transform.localScale;
        Transform parentA = HandleItemA.transform.parent;

        Vector3 positionB = GroundItemB.transform.position;
        Quaternion rotationB = GroundItemB.transform.rotation;
        Vector3 scaleB = GroundItemB.transform.localScale;
        Transform parentB = GroundItemB.transform.parent;

        HandleItemA.transform.position = positionB;
        HandleItemA.transform.rotation = rotationB;
        HandleItemA.transform.localScale = scaleB;
        HandleItemA.transform.parent = parentB;

        GroundItemB.transform.position = positionA;
        GroundItemB.transform.rotation = rotationA;
        GroundItemB.transform.localScale = scaleA;
        GroundItemB.transform.parent = parentA;

        HandleItemA.GetComponent<GunInGround>().enabled = true;
        if (Weapon.HoldingWeapon == Weapon.MainWeapon)
        {
            Weapon.changeToMainWeapon();
        }
        else
        {
            Weapon.changeToSecondWeapon();
        }
    }
}
