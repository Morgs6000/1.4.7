using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    [SerializeField] Transform player;

    float xRotation = 0;
    
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        Cursor.lockState = CursorLockMode.Locked;

        CameraUpdates();
    }

    void CameraUpdates() {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        player.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
