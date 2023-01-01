using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    // Lista de GameObjects com as sprites de coração
    [SerializeField] private GameObject[] hearts;

    // Variável para armazenar o número de pontos de vida atuais do jogador
    [SerializeField] private int maxHealth = 20;
    [SerializeField] private int currentHealth;
    //[SerializeField] private float decimalHealth;

    // Lista de sprites para representar os diferentes estados de cada coração
    [SerializeField] private Sprite[] sprites;

    [SerializeField] private HungerBar hunger;
    
    // Variável para armazenar o tempo decorrido desde a última vez que o método GanharVida() foi chamado
    [SerializeField] private float tempoDecorrido = 0;

    private void Start() {
        currentHealth = maxHealth;
        //decimalHealth = currentHealth;
    }

    private void Update() {
        // Atualiza a barra de vida do jogador baseado nos pontos de vida atuais
        HealthBarUpdate();

        AddHelth();
        RemoveHealth();

        // Na difculdade Fácil, a saúde do jogador para de cair em 10,
        // no Normal, para em 1,
        // e no Difícil, continua até que o jogador coma alguma coisa ou morra de fome.
        //if(currentHealth == 1) {
            
        //}
    }

    private void HealthBarUpdate() {
        // Percorre todos os GameObjects com as sprites de coração
        for (int i = 0; i < hearts.Length; i++) {
            // Se o índice atual representar um ponto de vida, ativa o GameObject e altera a sprite para um coração inteiro
            if (i < currentHealth / 2) {
                hearts[i].SetActive(true);
                hearts[i].GetComponent<Image>().sprite = sprites[0];
            }
            // Se o índice atual representar meio ponto de vida, ativa o GameObject e altera a sprite para meio coração
            else if (i < currentHealth / 2 + currentHealth % 2) {
                hearts[i].SetActive(true);
                hearts[i].GetComponent<Image>().sprite = sprites[1];
            }
            // Se o índice atual não representar nenhum ponto de vida, desativa o GameObject
            else {
                hearts[i].SetActive(false);
            }
        }
    }

    private void AddHelth() {
        if(currentHealth < maxHealth) {
            // Se a barra de FOME estiver em 20 e ainda há saturação.
            // +1 HP a cada 0,5 segundo.
            if(hunger.currentHunger == 20) {
                tempoDecorrido += Time.deltaTime;

                if(tempoDecorrido >= 0.5f) {
                    currentHealth++;
                    tempoDecorrido = 0;
                }
            }
            // Se a barra de FOME estiver em 18.
            // +1 HP a cada 4 segundos.
            else if(hunger.currentHunger >= 18) {
                tempoDecorrido += Time.deltaTime;
                
                if(tempoDecorrido >= 4.0f) {
                    currentHealth++;
                    tempoDecorrido = 0;
                }
            }
            else {
                tempoDecorrido = 0;
            }
        }
    }

    private void RemoveHealth() {
        if(currentHealth > 0) {
            // Se a barra de FOME estiver em 0.
            // -1 HP a cada 4 segundos.            
            if(hunger.currentHunger == 0) {
                tempoDecorrido += Time.deltaTime;

                // Se o tempo decorrido for maior ou igual a 4 segundos, chama o método GanharVida() e reseta o contador
                if (tempoDecorrido >= 4.0f) {
                    currentHealth--;
                    tempoDecorrido = 0;
                }
                else {
                    tempoDecorrido = 0;
                }
            }
        }
    }

    /*
    // Método para reduzir os pontos de vida do jogador
    private void TomarDano(int dano) {
        currentHealth -= dano;
        HealthBarUpdate();
    }

    // Método para aumentar os pontos de vida do jogador
    private void GanharVida(int vida) {
        currentHealth += vida;
        HealthBarUpdate();
    }

    // Método para aumentar os pontos de vida do jogador
    public void RegenerarVida() {
        currentHealth++;
        HealthBarUpdate();
    }

    // Método para remover um ponto de vida do jogador
    public void RemoverVida() {
        currentHealth--;
        HealthBarUpdate();
    }
    */
}
