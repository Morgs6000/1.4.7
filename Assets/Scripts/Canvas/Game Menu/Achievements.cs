using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievements : MonoBehaviour {
    [SerializeField] private GameMenu gameMenu;

    [SerializeField] private Button doneButton;

    public bool openAchievements;

    private void Start() {
        doneButton.onClick.AddListener(ButtonDone);
    }

    private void Update() {
        openAchievements = true;

        //Menu();
    }

    private void ButtonDone() {
        openAchievements = false;

        gameMenu.gameMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }

    private void Menu() {
        if(Input.GetButtonDown("Escape")) {
            openAchievements = false;
            gameMenu.openGameMenu = false;            
            
            this.gameObject.SetActive(false);
        }
    }
}
