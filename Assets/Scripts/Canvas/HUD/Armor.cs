using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Armor : MonoBehaviour {
    // Lista de GameObjects com as sprites de armaduras
    [SerializeField] private GameObject[] armors;

    // Variável para armazenar o número de pontos de armadura atuais do jogador
    [SerializeField] private int maxArmor = 20;
    [SerializeField] private int currentArmor;

    // Lista de sprites para representar os diferentes estados de cada armadura
    [SerializeField] private Sprite[] sprites;

    [SerializeField] private GameObject armorBar;

    private void Update() {
        // Atualiza a barra de vida do jogador baseado nos pontos de vida atuais
        ArmorUpdate();
    }

    private void ArmorUpdate() {
        // Percorre todos os GameObjects com as sprites de coração
        for (int i = 0; i < armors.Length; i++) {
            // Se o índice atual representar um ponto de vida, ativa o GameObject e altera a sprite para um coração inteiro
            if (i < currentArmor / 2) {
                armors[i].SetActive(true);
                armors[i].GetComponent<Image>().sprite = sprites[0];
            }
            // Se o índice atual representar meio ponto de vida, ativa o GameObject e altera a sprite para meio coração
            else if (i < currentArmor / 2 + currentArmor % 2) {
                armors[i].SetActive(true);
                armors[i].GetComponent<Image>().sprite = sprites[1];
            }
            // Se o índice atual não representar nenhum ponto de vida, desativa o GameObject
            else {
                armors[i].SetActive(false);
            }
        }

        if(currentArmor == 0) {
            armorBar.SetActive(false);
        }
        else if(currentArmor > 0){
            armorBar.SetActive(true);
        }
    }
}
