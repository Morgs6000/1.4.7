using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject loadScreen;

    public static Vector3 WorldSizeInVoxels = new Vector3(256, 256, 256);

    private Vector3 WorldSizeInChunks = new Vector3(
        WorldSizeInVoxels.x / Chunk.ChunkSizeInVoxels.x,
        WorldSizeInVoxels.y / Chunk.ChunkSizeInVoxels.y,
        WorldSizeInVoxels.z / Chunk.ChunkSizeInVoxels.z
    );

    [SerializeField] private GameObject chunkPrefab;
    private List<GameObject> chunks = new List<GameObject>();

    [SerializeField] private GameObject player;

    private int viewDistance = 4;
    
    private void Start() {
        //mainCamera.SetActive(true);
        //loadScreen.SetActive(true);

        //player.SetActive(false);

        //StartCoroutine(InitialWorldGen());
    }

    private void Update() {
        StartCoroutine(WorldGen());     
    }

    private IEnumerator InitialWorldGen() {
        for(int x = -(viewDistance / 2); x < (viewDistance / 2); x++) {
            for(int y = 0; y < WorldSizeInChunks.y; y++) {
                for(int z = -(viewDistance / 2); z < (viewDistance / 2); z++) {
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

    private IEnumerator WorldGen() {
        int posX = Mathf.FloorToInt(player.transform.position.x / Chunk.ChunkSizeInVoxels.x);
        int posZ = Mathf.FloorToInt(player.transform.position.z / Chunk.ChunkSizeInVoxels.z);

        for(int x = (posX - viewDistance / 2); x < (posX + viewDistance / 2); x++) {
            for(int y = 0; y < WorldSizeInChunks.y; y++) {
                for(int z = (posZ - viewDistance / 2); z < (posZ + viewDistance / 2); z++) {
                    Vector3 chunkOffset = new Vector3(
                        x * Chunk.ChunkSizeInVoxels.x,
                        y * Chunk.ChunkSizeInVoxels.y,
                        z * Chunk.ChunkSizeInVoxels.z
                    );

                    Chunk c = Chunk.GetChunk(new Vector3(
                        Mathf.FloorToInt(chunkOffset.x),
                        Mathf.FloorToInt(chunkOffset.y),
                        Mathf.FloorToInt(chunkOffset.z)
                    ));

                    if(c == null) {
                        GameObject chunk = Instantiate(chunkPrefab);
                        chunk.transform.position = chunkOffset;
                        chunk.transform.SetParent(transform);
                        chunk.name = "Chunk: " + x + ", " + z;
                    }

                    yield return null;
                }
            }
        }
    }
}
