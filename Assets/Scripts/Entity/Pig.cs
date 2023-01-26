using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour {
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float speed;
    private float walkingSpeed = 4.317f;

    private Vector3 velocity;    
    private float fallSpeed = -78.4f;
    private bool isGrounded;
    
    [Space(20)]
    [SerializeField] private Transform groundCheck;
    private float groundDistance = 0.1f;
    [SerializeField] private LayerMask groundMask;
    
    void Start() {
    }

    void Update() {
        MovementUpdate();

        FallUpdate();
        
        StepOffsetUpdate();
    }

    private void MovementUpdate() {                
        float x = Random.Range(-1.0f, 1.0f);
        float z = Random.Range(-1.0f, 1.0f);

        Vector3 moveDirection = new Vector3(x, 0.0f, z);

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

    private void StepOffsetUpdate() {
        // Ponto de origem do raio
        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;

        // Direção do raio
        Vector3 rayDirection = transform.TransformDirection(Vector3.forward);

        // Distância máxima do raio
        float maxDistance = 0.5f;

        RaycastHit hit;

        if(isGrounded) {
            // Verifica se o raio colide com alguma parede de voxel
            if(Physics.Raycast(rayOrigin, rayDirection, out hit, maxDistance, groundMask)) {
                Vector3 rayOriginUp = transform.position + Vector3.up * 1.1f;
                Vector3 rayDirectionUp = transform.TransformDirection(Vector3.forward);
                float maxDistanceUp = 0.5f;

                if (Physics.Raycast(rayOriginUp, rayDirectionUp, maxDistanceUp, groundMask)) {
                    return;
                }
                else {
                    // Se colidir, ajusta a posição do personagem para ficar no topo do voxel
                    transform.position = hit.point + Vector3.up * (characterController.height / 2);
                }
            }
        }
    }
}
