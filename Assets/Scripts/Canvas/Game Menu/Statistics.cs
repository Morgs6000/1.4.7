using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour {
    [SerializeField] private GameMenu gameMenu;

    [SerializeField] private Button doneButton;

    public bool openStatistics;

    private void Start() {
        doneButton.onClick.AddListener(ButtonDone);
    }

    private void Update() {
        openStatistics = true;
        
        //Menu();
    }

    private void ButtonDone() {
        openStatistics = false;

        gameMenu.gameMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }

    private void Menu() {
        if(Input.GetButtonDown("Escape")) {
            openStatistics = false;
            gameMenu.openGameMenu = false;
            
            this.gameObject.SetActive(false);
        }
    }
}
