using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    [SerializeField] private Transform player;

    private float xRotation = 0;

    [SerializeField] private MenusManager menusManager;
    private bool openMenu;
    
    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        openMenu = menusManager.openMenu;
        
        if(!openMenu) {
            Cursor.lockState = CursorLockMode.Locked;

            CameraUpdates();
        }
        else if(openMenu) {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void CameraUpdates() {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        player.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
