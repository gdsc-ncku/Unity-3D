using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerBasicInformationScriptable PlayerMove;
    [SerializeField] private PlayerBattleValueScriptable MovementConst;
    private Rigidbody rb;
    float Speed;  // Movement speed
    private bool Grounded = true;  // Whether the player is on the ground
    float horizontal = 0f;
    float vertical = 0f;

    void Start()
    {
        Speed = MovementConst.Role.WalkSpeed;
        // Initialize Rigidbody and freeze rotation
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
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

        // Jump
        if (Input.GetKeyDown(PlayerMove.Jump) && Grounded)
        {
            Grounded = false;
            StartCoroutine(Jump());
        }

        Vector3 movement = new Vector3(horizontal, 0f, vertical) * Speed * Time.deltaTime;
        transform.Translate(movement);
    }

    IEnumerator Jump()
    {
        float jumpSpeed = MovementConst.Role.JumpSpeed;
        float targetHeight = transform.position.y + MovementConst.Role.JumpHigh;
        while (transform.position.y < targetHeight)
        {
            transform.position += Vector3.up * jumpSpeed * Time.deltaTime;
            yield return null;
        }

        while (transform.position.y > targetHeight - MovementConst.Role.JumpHigh)
        {
            transform.position -= Vector3.up * jumpSpeed * Time.deltaTime;
            yield return null;
        }

        Grounded = true;
    }
}