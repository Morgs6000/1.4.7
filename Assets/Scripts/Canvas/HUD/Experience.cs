using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Experience : MonoBehaviour {
    [SerializeField] private int currentExperience;

    [SerializeField] private Image experienceBar;
    [SerializeField] private TextMeshProUGUI experienceLevel;

    [SerializeField] private float contador;
    
    [SerializeField] private InterfaceManager interfaceManager;
    private bool openGameMenu;
    
    private void Start() {
        
    }

    private void Update() {
        openGameMenu = interfaceManager.openGameMenu;

        if(!openGameMenu) {
            ExperienceUpdate();
        }
    }

    private void ExperienceUpdate() {
        experienceBar.fillAmount = contador;

        //contador += Time.deltaTime;

        if(contador >= 1.0f) {
            currentExperience++;
            contador = 0;
        }
        
        experienceLevel.text = currentExperience.ToString();

        if(currentExperience == 0) {
            experienceLevel.text = null;
        }
    }
}
