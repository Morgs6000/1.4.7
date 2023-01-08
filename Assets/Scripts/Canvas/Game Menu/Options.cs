using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour {
    [SerializeField] private GameMenu gameMenu;

    [SerializeField] private Button doneButton;

    public bool openOptions;

    private void Start() {
        doneButton.onClick.AddListener(ButtonDone);
    }

    private void Update() {
        openOptions = true;
        
        //Menu();
    }

    private void ButtonDone() {
        openOptions = false;

        gameMenu.gameMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }

    private void Menu() {
        if(Input.GetButtonDown("Escape")) {
            openOptions = false;
            gameMenu.openGameMenu = false;
            
            this.gameObject.SetActive(false);
        }
    }
}
