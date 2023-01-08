using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExperienceBar : MonoBehaviour {
    [SerializeField] private int currentExperience;

    [SerializeField] private Image experienceBar;
    [SerializeField] private TextMeshProUGUI experienceLevel;

    [SerializeField] private float contador;
    
    private bool openGameMenu;
    
    private void Start() {
        
    }

    private void Update() {
        openGameMenu = CanvasManager.openGameMenu;

        if(!openGameMenu) {
            ExperienceBarUpdate();
        }
    }

    private void ExperienceBarUpdate() {
        experienceBar.fillAmount = contador;

        contador += Time.deltaTime;

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
