using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    [SerializeField] public GameObject inventory;
    public bool openInventory;

    [Space(20)]
    [SerializeField] private Interface interfaceMenu;
    private bool openInterface;
    
    private void Start() {
        
    }

    private void Update() {
        openInterface = interfaceMenu.openInterface;
        
        Menu();
    }

    private void Menu() {
        if(Input.GetKeyDown(KeyCode.E)) {
            openInventory = !openInventory;

            inventory.SetActive(!inventory.activeSelf);

            if(!openInterface) {
                interfaceMenu.OpenInterface();
            }
        }
        /*
        if(inventory.activeSelf) {
            if(Input.GetButtonDown("Escape")) {
                openInventory = false;

                inventory.SetActive(false);
            }
        }
        */
    }
}
