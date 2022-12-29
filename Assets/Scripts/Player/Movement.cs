using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    [SerializeField] private CharacterController characterController;

    private float speed;
    private float walkingSpeed = 4.317f;
    
    private Vector3 velocity;    
    private float fallSpeed = -78.4f;
    private bool isGrounded;
    
    [SerializeField] private Transform groundCheck;
    private float groundDistance = 0.1f;
    [SerializeField] private LayerMask groundMask;
    
    private float jumpHeight = 1.2522f;

    //private float stepOffset = 1.0f;

    private void Start() {
        speed = walkingSpeed;
    }

    private void Update() {
        MovementUpdate();
        FallUpdate();
        JumpUpdate();

        //StepOffsetUpdate();
    }

    private void MovementUpdate() {
        float x = Input.GetAxis("HorizontalAD");
        float z = Input.GetAxis("VerticalWS");

        Vector3 moveDirection = transform.TransformDirection(new Vector3(x, 0.0f, z));

        moveDirection *= speed;

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void FallUpdate() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        velocity.y += fallSpeed * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

        if(isGrounded && velocity.y < 0) {
            velocity.y = -2.0f;
        }
    }

    private void JumpUpdate() {
        if(isGrounded && Input.GetButton("Space")) {
            isGrounded = false;

            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * fallSpeed);
        }
    }

    /*
    private void StepOffsetUpdate() {
        RaycastHit hit;

        
        if (Physics.Raycast(transform.position, Vector3.down, out hit, stepOffset)) {
            Vector3 newPosition = hit.point + Vector3.up * stepOffset;
            characterController.Move(newPosition - transform.position);
        }
        else {
            characterController.Move(moveDirection * Time.deltaTime);
        }
    }
    */
}
