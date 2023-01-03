using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugScreen : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI textLeft;
    [SerializeField] private TextMeshProUGUI textRight;

    private string debugTextLeft;
    private string debugTextRight;

    private float frameRate;
    private float timer;

    [SerializeField] private Transform player;

    private void Start() {
        
    }

    private void Update() {
        FrameRate();

        TextLeft();
        TextRight();
    }

    private void FrameRate() {
        if(timer > 1.0f) {
            frameRate = (int)(1.0f / Time.unscaledDeltaTime);
            timer = 0;
        }
        else {
            timer += Time.deltaTime;
        }
    }

    private void TextLeft() {
        

        debugTextLeft = (
            "Minecraft Clone 1.4.7 (" + frameRate + " fps, " + "0" + " chunk updates)" + "\n" +
            "C: 0/0. F: 0, O: 0. E: 0" + "\n" +
            "E: 0/0. B: 0, I: 0" + "\n" +
            "P: 0. T: All: 0" + "\n" +
            "MultiplayerChunkChache: 0"
        );

        debugTextLeft += "\n\n";

        PlayerPosition();

        debugTextLeft += "\n";

        PlayerDirection();

        debugTextLeft += "\n";

        debugTextLeft += (
            "lc: " + "0" + 
            " b: " + "Eternal WindowsXP Hills" + 
            " bl: " + "0" + 
            " sl: " + "0" + 
            " rl: " + "0" + 
            "\n" +
            
            "ws: " + "0" + 
            ", fs: " + "0"
        );

        PlayerIsGrounded();

        debugTextLeft += (
            ", fl: " + "0"
        );

        textLeft.text = debugTextLeft;
    }

    private void PlayerPosition() {
        Vector3 playerPos = new Vector3(
            player.position.x - 0.5f,
            player.position.y - 0.5f,
            player.position.z - 0.5f
        );

        Vector3 chunkPos = new Vector3(
            playerPos.x / Chunk.ChunkSizeInVoxels.x,
            playerPos.y / Chunk.ChunkSizeInVoxels.y,
            playerPos.z / Chunk.ChunkSizeInVoxels.z
        );

        debugTextLeft += (
            // Posição x do jogador
            "x: " + playerPos.x + 
            // Posição x inteira do jogador
            " (" + (playerPos.x).ToString("F0") + ") // " + 
            // Chunk que o jogador esta pisando em x
            "c: " + (chunkPos.x).ToString("F0") + 
            " (0)" + 
            "\n" +
            
            // Posição y do jogador
            "y: " + playerPos.y +  " (feet pos, " + 
            // Posição y do jogador + altura dos olhos
            (playerPos.y + 1.62f).ToString() + " eyes pos)" + 
            "\n" +
            
            // Posição z do jogador
            "z: " + playerPos.z + 
            // Posição z inteira do jogador
            " (" + (playerPos.z).ToString("F0") + ") // " + 
            // Chunk que o jogador esta pisando em z
            "c: " + (chunkPos.z).ToString("F0") + 
            " (0)"
        );
    }

    private void PlayerDirection() {
        float aguloInclinacao = player.rotation.eulerAngles.y;

        if(aguloInclinacao > 180) {
            aguloInclinacao -= 360;
        }

        int valor = 0;
        string direction = "SOUTH";

        
        if(aguloInclinacao > -45 || aguloInclinacao < 45) {
            valor = 0;
            direction = "SOUTH";
        }
        if(aguloInclinacao > 45 && aguloInclinacao < 135) {
            valor = 1;
            direction = "WEST";
        }
        if(aguloInclinacao > 135 || aguloInclinacao < -135) {
            valor = 2;
            direction = "NORTH";
        }
        if(aguloInclinacao > -135 && aguloInclinacao < -45) {
            valor = 3;
            direction = "EAST";
        }
        
        debugTextLeft += (
            // Direção cardeal que o jogador esta olhando
            "f: " + valor + " (" + direction + ") / " + aguloInclinacao
        );
    }

    private void PlayerIsGrounded() {
        bool isGrounded = Movement.isGrounded;
        
        debugTextLeft += (
            // Se o jogador esta pisando no chão
            ", g: " + isGrounded.ToString().ToLower()
        );
    }

    private void TextRight() {
        debugTextRight = (
            "Used memory: 0% (0MB) of 0MB" + "\n" +
            "Allocated momory: 0% (0MB)"
        );

        textRight.text = debugTextRight;
    }
}
