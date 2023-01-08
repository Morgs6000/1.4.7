using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenToLan : MonoBehaviour {
    [SerializeField] private GameMenu gameMenu;

    [SerializeField] private Button cancelButton;

    public bool openOpenToLan;

    private void Start() {
        cancelButton.onClick.AddListener(ButtonCancel);
    }

    private void Update() {
        openOpenToLan = true;
        
        //Menu();
    }

    private void ButtonCancel() {
        openOpenToLan = false;
        
        gameMenu.gameMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }

    private void Menu() {
        if(Input.GetButtonDown("Escape")) {
            openOpenToLan = false;
            gameMenu.openGameMenu = false;
            
            this.gameObject.SetActive(false);
        }
    }
}
