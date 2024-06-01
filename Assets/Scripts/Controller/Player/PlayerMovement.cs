using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerBasicInformationScriptable PlayerMove;
    [SerializeField] private PlayerBattleValueScriptable MovementConst;
    [SerializeField] private GameObject GroundDetector;
    private Rigidbody rb;
    float Speed;  // Movement speed
    private bool Grounded = true, isJumping = false;  // Whether the player is on the ground
    PlayerControl playerControl;
    private void Awake()
    {
        if(PlayerMove == null || PlayerMove.playerControl == null)
        {
            Debug.Log("PlayerBasicInformationScriptable Disappear");
            return;
        }

        playerControl = PlayerMove.playerControl;
        playerControl.Player.Enable();
        playerControl.Player.Jump.performed += Jumping;
    }

    private void OnEnable()
    {
        if (playerControl == null)
        {
            Debug.Log("PlayerBasicInformationScriptable Disappear");
            return;
        }

        playerControl.Player.Jump.performed += Jumping;
    }

    private void OnDisable()
    {
        if (playerControl == null)
        {
            Debug.Log("PlayerBasicInformationScriptable Disappear");
            return;
        }

        playerControl.Player.Jump.performed -= Jumping;
        playerControl.Player.Disable();
    }

    private void OnDestroy()
    {
        if (playerControl == null)
        {
            Debug.Log("PlayerBasicInformationScriptable Disappear");
            return;
        }

        playerControl.Player.Jump.performed -= Jumping;
    }

    void Start()
    {
        if (MovementConst == null || MovementConst.Role == null)
        {
            Debug.Log("PlayerBasicInformationScriptable Disappear");
            return;
        }

        Speed = MovementConst.Role.GetComponent<StudentDataManager>().studentData.WalkSpeed;
        // Initialize Rigidbody and freeze rotation
        rb = GetComponent<Rigidbody>();
        MovementConst.Player = gameObject;
    }

    private void Update()
    {
        SpeedLimit();
    }

    void FixedUpdate()
    {
        //Moving
        Vector2 inputVector = playerControl.Player.Move.ReadValue<Vector2>();
        Vector3 moveDirection = rb.transform.forward * inputVector.y + rb.transform.right * inputVector.x;
        rb.AddForce(moveDirection.normalized * Speed * 5f, ForceMode.Force);
    }

    // Jump
    void Jumping(InputAction.CallbackContext context)
    {
        if (Grounded && !isJumping)
        {
            isJumping = true;
            Grounded = false;
            StartCoroutine(Jump());
        }
    }

    void SpeedLimit()
    {
        Vector3 Vel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (Vel.magnitude > Speed)
        {
            Vector3 limitedVel = Vel.normalized * Speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    IEnumerator Jump()
    {
<<<<<<<<< Temporary merge branch 1
        float jumpForce = MovementConst.Role.GetComponent<StudentDataManager>().studentData.JumpForce;
        rb.AddForce(gameObject.transform.up * jumpForce, ForceMode.Impulse);

        yield return new WaitForSeconds(0.1f);

        while (!Physics.Raycast(GroundDetector.transform.position, GroundDetector.transform.up * -1, out _, 0.05f))
        {
        }

        Grounded = true;
        isJumping = false;
    }
    }

}
