using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {    
    [SerializeField] public GameObject gameMenu;
    public bool openGameMenu;

    [Space(20)]
    [SerializeField] private Button backToGameButton;
    [SerializeField] private Button achievementsButton;
    [SerializeField] private Button statisticsButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button openToLanButton;

    [Space(20)]
    [SerializeField] public GameObject achievements;
    [SerializeField] public GameObject statistics;
    [SerializeField] public GameObject options;
    [SerializeField] public GameObject openToLan;

    [SerializeField] private MenusManager menusManager;
    private bool openMenu;
    
    private void Start() {        
        backToGameButton.onClick.AddListener(ButtonBacktoGame);

        achievementsButton.onClick.AddListener(ButtonAchievements);
        statisticsButton.onClick.AddListener(ButtonStatistics);
        optionsButton.onClick.AddListener(ButtonOptions);
        openToLanButton.onClick.AddListener(ButtonOpenToLan);

        gameMenu.SetActive(false);

        achievements.SetActive(false);
        statistics.SetActive(false);
        options.SetActive(false);
        openToLan.SetActive(false);
    }

    private void Update() {
        Menu();
    }

    private void Menu() {
        if(!openMenu) {
            if(Input.GetButtonDown("Escape")) {
                openGameMenu = !openGameMenu;

                gameMenu.SetActive(!gameMenu.activeSelf);
            }
        }
    }

    private void ButtonBacktoGame() {
        openGameMenu = false;

        gameMenu.SetActive(false);
    }

    private void ButtonAchievements() {
        achievements.SetActive(true);   
        gameMenu.SetActive(false);     
    }

    private void ButtonStatistics() {
        statistics.SetActive(true);   
        gameMenu.SetActive(false);     
    }

    private void ButtonOptions() {
        options.SetActive(true);   
        gameMenu.SetActive(false);     
    }

    private void ButtonOpenToLan() {
        openToLan.SetActive(true);   
        gameMenu.SetActive(false);     
    }
}
