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

        TestLeft();
        TestRight();
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

    private void TestLeft() {
        Vector3 playerPos = new Vector3(
            player.position.x - 0.5f,
            player.position.y - 0.5f - 0.6f + 1.0f,
            player.position.z - 0.5f
        );

        Vector3 chunkPos = new Vector3(
            playerPos.x / Chunk.ChunkSizeInVoxels.x,
            playerPos.y / Chunk.ChunkSizeInVoxels.y,
            playerPos.z / Chunk.ChunkSizeInVoxels.z
        );

        debugTextLeft = (
            "Minecraft Clone 1.4.7 (" + frameRate + " fps, " + "0" + " chunk updates)" + "\n" +
            "C: 0/0. F: 0, O: 0. E: 0" + "\n" +
            "E: 0/0. B: 0, I: 0" + "\n" +
            "P: 0. T: All: 0" + "\n" +
            "MultiplayerChunkChache: 0" + "\n\n" +

            "x: " + (playerPos.x).ToString() + 
            " (" + (playerPos.x).ToString("F0") + ") // " + 
            "c: " + (chunkPos.x).ToString("F0") + " (0)" + "\n" +
            
            "y: " + (playerPos.y).ToString() + 
            " (feet pos, " + (playerPos.y + 1.62f).ToString() + " eyes pos)" + "\n" +
            
            "z: " + (playerPos.z).ToString() + 
            " (" + (playerPos.z).ToString("F0") + ") // " + 
            "c: " + (chunkPos.z).ToString("F0") + " (0)" + "\n" +
            
            "f: " + "2" + " (" + "NORTH" + ") / " + "0" + "\n" +
            
            "lc: " + "0" + 
            " b: " + "Eternal WindowsXP Hills" + 
            " bl: " + "0" + 
            " sl: " + "0" + 
            " rl: " + "0" + "\n" +
            
            "ws: " + "0" + 
            ", fs: " + "0" + 
            ", g: " + "false" + 
            ", fl: " + "0"
        );

        textLeft.text = debugTextLeft;
    }

    private void TestRight() {
        debugTextRight = (
            "Used memory: 0% (0MB) of 0MB" + "\n" +
            "Allocated momory: 0% (0MB)"
        );

        textRight.text = debugTextRight;
    }
}
