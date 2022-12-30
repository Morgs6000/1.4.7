using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject loadScreen;

    public static Vector3 WorldSizeInVoxels = new Vector3(32, 256, 32);

    private Vector3 WorldSizeInChunks = new Vector3(
        WorldSizeInVoxels.x / Chunk.ChunkSizeInVoxels.x,
        WorldSizeInVoxels.y / Chunk.ChunkSizeInVoxels.y,
        WorldSizeInVoxels.z / Chunk.ChunkSizeInVoxels.z
    );

    [SerializeField] private GameObject chunkPrefab;
    [SerializeField] private GameObject player;
    
    private void Start() {
        mainCamera.SetActive(true);
        loadScreen.SetActive(true);

        player.SetActive(false);

        StartCoroutine(WorldGen());
    }

    private void Update() {
        
    }

    private IEnumerator WorldGen() {
        for(int x = -(int)(WorldSizeInChunks.x / 2); x < (WorldSizeInChunks.x / 2); x++) {
            for(int y = 0; y < WorldSizeInChunks.y; y++) {
                for(int z = -(int)(WorldSizeInChunks.z / 2); z < (WorldSizeInChunks.z / 2); z++) {
                    Vector3 chunkOffset = new Vector3(
                        x * Chunk.ChunkSizeInVoxels.x,
                        y * Chunk.ChunkSizeInVoxels.y,
                        z * Chunk.ChunkSizeInVoxels.z
                    );

                    GameObject chunk = Instantiate(chunkPrefab);
                    chunk.transform.position = chunkOffset;
                    chunk.transform.SetParent(transform);
                    chunk.name = "Chunk: " + x + ", " + z;
                    
                    yield return null;
                }
            }
        }

        PlayerSpawn();
    }

    private void PlayerSpawn() {
        Vector3 spawn = new Vector3(
            0,
            WorldSizeInVoxels.y,
            0
        );

        player.transform.position = spawn;

        player.SetActive(true);

        mainCamera.SetActive(false);
        loadScreen.SetActive(false);
    }
}
