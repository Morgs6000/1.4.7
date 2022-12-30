using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour {
    // Lista de GameObjects com as sprites de coração
    [SerializeField] private GameObject[] drumsticks;

    // Variável para armazenar o número de pontos de vida atuais do jogador
    public int maxHunger = 20;
    public int currentHunger;

    // Lista de sprites para representar os diferentes estados de cada coração
    [SerializeField] private Sprite[] sprites;

    private void Start() {
        currentHunger = maxHunger;
    }

    private void Update() {
        // Atualiza a barra de vida do jogador baseado nos pontos de vida atuais
        HungerBarUpdate();
    }

    private void HungerBarUpdate() {
        // Percorre todos os GameObjects com as sprites de coração
        for (int i = 0; i < drumsticks.Length; i++) {
            // Se o índice atual representar um ponto de vida, ativa o GameObject e altera a sprite para um coração inteiro
            if (i < currentHunger / 2) {
                drumsticks[i].SetActive(true);
                drumsticks[i].GetComponent<Image>().sprite = sprites[0];
            }
            // Se o índice atual representar meio ponto de vida, ativa o GameObject e altera a sprite para meio coração
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
        HungerBarUpdate();
    }

    // Método para aumentar os pontos de vida do jogador
    private void GanharVida(int vida) {
        currentHunger += vida;
        HungerBarUpdate();
    }
    */
}
