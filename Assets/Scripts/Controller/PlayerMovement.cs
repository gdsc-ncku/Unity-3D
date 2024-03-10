using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerBasicInformationScriptable PlayerMove;
    [SerializeField] private PlayerBattleValueScriptable MovementConst;
    private Rigidbody rb;
    float Speed;  // Movement speed
    float JumpForce;  // Jump force
    private bool Grounded;  // Whether the player is on the ground
    float horizontal = 0f;
    float vertical = 0f;

    void Start()
    {
        Speed = MovementConst.Role.WalkSpeed;
        JumpForce = MovementConst.Role.JumpForce;
        // Initialize Rigidbody and freeze rotation
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get keyboard input and move
        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");

        if (Input.GetKey(PlayerMove.WalkForward)) {
            vertical = 1f;
        }
        else if(Input.GetKey(PlayerMove.WalkBackward)){
            vertical = -1f;
        } else{
            vertical = 0f;
        }

        if (Input.GetKey(PlayerMove.WalkRight)) {
            horizontal = 1f;
        }
        else if(Input.GetKey(PlayerMove.WalkLeft)){
            horizontal = -1f;
        } else{
            horizontal = 0f;
        }

        Vector3 movement = new Vector3(horizontal, 0f, vertical) * Speed * Time.deltaTime;
        transform.Translate(movement);

        // Jump
        if (Input.GetKey(PlayerMove.Jump) && Grounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        // Reset vertical velocity, apply jump force, and set grounded to false
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
        Grounded = false;
    }
void OnCollisionEnter(Collision collision)
    {
        // Check if the player is on the ground by detecting collision with an object tagged as "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            Grounded = true;
        }
    }
}