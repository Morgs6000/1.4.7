using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenusManager : MonoBehaviour {
    [SerializeField] private GameMenu gameMenu;

    [SerializeField] private Achievements achievements;
    [SerializeField] private Statistics statistics;
    [SerializeField] private Options options;
    [SerializeField] private OpenToLan openToLan;

    [SerializeField] private Inventory inventory;
    // Chest
    // Crafting Table
    // Furnace

    public bool openMenu;
    public bool openGameMenu;
    
    private void Start() {
        
    }

    private void Update() {
        OpenMenu();
        OpenGameMenu();
    }

    private void OpenMenu() {
        if(
            inventory.openInventory == true ||

            gameMenu.openGameMenu == true ||
            achievements.openAchievements == true ||
            statistics.openStatistics == true ||
            options.openOptions == true ||
            openToLan.openOpenToLan == true
        ) {
            openMenu = true;
        }
        else {
            openMenu = false;
        }
    }

    private void OpenGameMenu() {
        if(
            gameMenu.openGameMenu == true
        ) {
            openGameMenu = true;
        }
        else {
            openGameMenu = false;
        }
    }

    //private void HasOpenMenu() {

    //}
}
