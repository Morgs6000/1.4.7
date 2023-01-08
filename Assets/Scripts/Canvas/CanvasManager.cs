using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour {
    [SerializeField] private GameObject interfaceGameObject;
    public static bool openInterface;

    [SerializeField] private GameObject debugScreen;

    [SerializeField] private GameObject inventory;
    public static bool openMenu;

    [SerializeField] private GameObject gameMenu;
    public static bool openGameMenu;
    
    private void Start() {
        interfaceGameObject.SetActive(true);
        openInterface = true;

        debugScreen.SetActive(true);
        
        inventory.SetActive(false);
        gameMenu.SetActive(false);
    }

    private void Update() {
        if(!openMenu) {
            Interface();
        }

        DebugScreen();

        Inventory();
        GameMenu();
    }

    private void Interface() {
        if(Input.GetKeyDown(KeyCode.F1)) {
            openInterface = !openInterface;

            interfaceGameObject.SetActive(!interfaceGameObject.activeSelf);
        }
    }

    private void DebugScreen() {
        if(Input.GetKeyDown(KeyCode.F3)) {
            debugScreen.SetActive(!debugScreen.activeSelf);
        }
    }

    private void Inventory() {
        if(Input.GetKeyDown(KeyCode.E)) {
            openMenu = !openMenu;

            inventory.SetActive(!inventory.activeSelf);

            if(!openInterface) {
                interfaceGameObject.SetActive(!interfaceGameObject.activeSelf);
            }
        }
    }

    private void GameMenu() {
        if(Input.GetButtonDown("Escape")) {
            openMenu = !openMenu;
            openGameMenu = !openGameMenu;

            gameMenu.SetActive(!gameMenu.activeSelf);
        }
    }

    public void CloseGameMenu() {
        openMenu = false;
        openGameMenu = false;
    }
}
