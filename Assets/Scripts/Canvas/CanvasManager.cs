using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour {
    [SerializeField] private GameObject interfaceGamObject;

    [Space(20)]
    [SerializeField] private GameObject debugScreen;
    
    [Space(20)]
    [SerializeField] private GameObject gameMenu;
    
    private void Start() {
        interfaceGamObject.SetActive(true);

        debugScreen.SetActive(true);

        gameMenu.SetActive(false);
    }

    private void Update() {
        Interface();

        DebugScreen();

        //GameMenu();
    }

    private void Interface() {
        if(Input.GetKeyDown(KeyCode.F1)) {
            interfaceGamObject.SetActive(!interfaceGamObject.activeSelf);
        }
    }

    private void DebugScreen() {
        if(Input.GetKeyDown(KeyCode.F3)) {
            debugScreen.SetActive(!debugScreen.activeSelf);
        }
    }

    private void GameMenu() {
        if(Input.GetButtonDown("Escape")) {
            gameMenu.SetActive(!gameMenu.activeSelf);
        }
    }
}
