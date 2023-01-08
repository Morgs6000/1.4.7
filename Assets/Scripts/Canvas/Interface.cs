using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface : MonoBehaviour {
    [SerializeField] private GameObject crosshair;

    [Space(20)]
    [SerializeField] private GameObject debugTextLeft;
    [SerializeField] private GameObject debugTextRight;
    
    [Space(20)]
    [SerializeField] private GameObject toolbar;
    [SerializeField] private GameObject HUD;

    [Space(20)]
    public bool openInterface;

    [Space(20)]
    [SerializeField] private Inventory inventory;
    private bool openInventory;
    
    
    private void Start() {
        openInterface = true;

        openInventory = inventory.openInventory;
    }

    private void Update() {
        Menu();
    }

    public void Menu() {
        if(!openInventory) {
            if(Input.GetKeyDown(KeyCode.F1)) {
                openInterface = !openInterface;

                OpenInterface();
            }
        }
    }

    public void OpenInterface() {
        crosshair.SetActive(!crosshair.activeSelf);
        
        debugTextLeft.SetActive(!debugTextLeft.activeSelf);
        debugTextRight.SetActive(!debugTextRight.activeSelf);

        toolbar.SetActive(!toolbar.activeSelf);
        HUD.SetActive(!HUD.activeSelf);
    }
}
