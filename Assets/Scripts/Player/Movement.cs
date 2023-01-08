using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    [SerializeField] private CharacterController characterController;

    [SerializeField] private float speed;
    private float walkingSpeed = 4.317f;
    
    private Vector3 velocity;    
    private float fallSpeed = -78.4f;
    public static bool isGrounded;
    
    [SerializeField] private Transform groundCheck;
    private float groundDistance = 0.1f;
    [SerializeField] private LayerMask groundMask;
    
    private float jumpHeight = 1.2522f;

    [SerializeField] private bool running;
    private float sprintingSpeed = 5.612f;

    private float lastClickTime;
    private const float DOUBLE_CLICK_TIME = 0.2f;

    //private float stepOffset = 1.0f;

    private bool openMenu;
    private bool openGameMenu;

    private void Start() {
        speed = walkingSpeed;
    }

    private void Update() {
        openMenu = CanvasManager.openMenu;
        openGameMenu = CanvasManager.openGameMenu;

        if(!openMenu) {
            MovementUpdate();
            JumpUpdate();
            SprintUpdate();
        }
        if(!openGameMenu) {
            FallUpdate();
        }
        
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
            running = false;

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

    private void SprintUpdate() {
        if(running == false) {
            speed = walkingSpeed;
        }
        if(running == true) {
            speed = sprintingSpeed;
        }

        // Pressionar LeftControl corre.
        if(Input.GetButton("LCtrl")) {
            running = true;
        }

        // Pressionar W duas vezes, também faz correr.
        if(Input.GetKeyDown(KeyCode.W)) {
            float timeSinceLastClick = Time.time - lastClickTime;

            if(timeSinceLastClick <= DOUBLE_CLICK_TIME) {
                running = true;
            }
            else {
                running = false;
            }

            lastClickTime = Time.time;
        }

        // Soltar W para de corre.
        if(Input.GetKeyUp(KeyCode.W)) {
            running = false;
        }
    }
}
