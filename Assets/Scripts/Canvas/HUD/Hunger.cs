using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hunger : MonoBehaviour {
    // Lista de GameObjects com as sprites de baquetas
    [SerializeField] private GameObject[] drumsticks;

    // Variável para armazenar o número de pontos de fome atuais do jogador
    public int maxHunger = 20;
    public int currentHunger;

    // Lista de sprites para representar os diferentes estados de cada baqueta
    [SerializeField] private Sprite[] sprites;

    private void Start() {
        currentHunger = maxHunger;
    }

    private void Update() {
        // Atualiza a barra de vida do jogador baseado nos pontos de vida atuais
        HungerUpdate();
    }

    private void HungerUpdate() {
        // Percorre todos os GameObjects com as sprites de coração
        for (int i = 0; i < drumsticks.Length; i++) {
            // Se o índice atual representar um ponto de vida, ativa o GameObject e altera a sprite para uma baqueta inteira
            if (i < currentHunger / 2) {
                drumsticks[i].SetActive(true);
                drumsticks[i].GetComponent<Image>().sprite = sprites[0];
            }
            // Se o índice atual representar meio ponto de vida, ativa o GameObject e altera a sprite para meia baqueta
            else if (i < currentHunger / 2 + currentHunger % 2) {
                drumsticks[i].SetActive(true);
                drumsticks[i].GetComponent<Image>().sprite = sprites[1];
            }
            // Se o índice atual não representar nenhum ponto de vida, desativa o GameObject
            else {
                drumsticks[i].SetActive(false);
            }
        }
    }

    /*
    // Método para reduzir os pontos de vida do jogador
    private void TomarDano(int dano) {
        currentHunger -= dano;
        HungerUpdate();
    }

    // Método para aumentar os pontos de vida do jogador
    private void GanharVida(int vida) {
        currentHunger += vida;
        HungerUpdate();
    }
    */
}
