using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oxygen : MonoBehaviour {
    // Lista de GameObjects com as sprites de bolhas
    [SerializeField] private GameObject[] bubbles;

    // Variável para armazenar o número de pontos de oxigênio atuais do jogador
    [SerializeField] private int maxOxygen = 20;
    [SerializeField] private int currentOxygen;

    // Lista de sprites para representar os diferentes estados de cada bolha
    [SerializeField] private Sprite[] sprites;

    [SerializeField] private GameObject oxygenBar;

    void Start() {
        
    }

    void Update() {
        OxygenUpdate();
    }

    private void OxygenUpdate() {
        if(currentOxygen == 0) {
            oxygenBar.SetActive(false);
        }
        else if(currentOxygen > 0){
            oxygenBar.SetActive(true);
        }
    }
}
