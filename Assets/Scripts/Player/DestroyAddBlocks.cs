using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAddBlocks : MonoBehaviour {
    [SerializeField] private Transform player;

    [SerializeField] private Transform cam;
    private float rangeHit = 5.0f;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private Toolbar toolbar;

    [SerializeField] private float currentTime = 0.25f;
    
    private void Start() {
        
    }

    private void Update() {
        DestroyBlocks();
        AddBlocks();
        //StartCoroutine(AddBlocks());
    }

    private void DestroyBlocks() {
        if(Input.GetMouseButtonDown(0)) {
            RaycastHit hit;

            if(Physics.Raycast(cam.position, cam.forward, out hit, rangeHit, groundMask)) {
                Vector3 pointPos = hit.point - hit.normal / 2;

                Chunk c = Chunk.GetChunk(new Vector3(
                    Mathf.FloorToInt(pointPos.x),
                    Mathf.FloorToInt(pointPos.y),
                    Mathf.FloorToInt(pointPos.z)
                ));

                c.SetBlock(pointPos, VoxelType.air);
            }
        }
    }

    private void AddBlocks() {
        if(Input.GetMouseButton(1)) {
            currentTime += Time.deltaTime;
            
            RaycastHit hit;

            if(currentTime >= 0.25f) {
                if(Physics.Raycast(cam.position, cam.forward, out hit, rangeHit, groundMask)) {
                    Vector3 pointPos = hit.point + hit.normal / 2;

                    float distance = 0.81f;
                    float playerDistance = Vector3.Distance(player.position, pointPos);
                    float camDistance = Vector3.Distance(cam.position, pointPos);

                    if(playerDistance < distance || camDistance < distance) {
                        return;
                    }
                    if(player.position.y - 1 > Chunk.ChunkSizeInVoxels.y) {
                        return;
                    }
                    
                    Chunk c = Chunk.GetChunk(new Vector3(
                        Mathf.FloorToInt(pointPos.x),
                        Mathf.FloorToInt(pointPos.y),
                        Mathf.FloorToInt(pointPos.z)
                    ));

                    c.SetBlock(pointPos, toolbar.GetCurrentItem());               
                }

                currentTime = 0.0f;
            }
        }
        else {
            currentTime = 0.25f;
        }
    }

    /*
    private IEnumerator AddBlocks() {
        while(Input.GetMouseButton(1)) {
            RaycastHit hit;

            if(Physics.Raycast(cam.position, cam.forward, out hit, rangeHit, groundMask)) {
                Vector3 pointPos = hit.point + hit.normal / 2;

                bool isValidPosition = 
                    Vector3.Distance(player.position, pointPos) > 0.81f &&
                    Vector3.Distance(cam.position, pointPos) > 0.81f;

                if(player.position.y - 1 > Chunk.ChunkSizeInVoxels.y) {
                    yield break;
                }
                else if(isValidPosition) {
                    Chunk c = Chunk.GetChunk(new Vector3(
                        Mathf.FloorToInt(pointPos.x),
                        Mathf.FloorToInt(pointPos.y),
                        Mathf.FloorToInt(pointPos.z)
                    ));

                    c.SetBlock(pointPos, toolbar.GetCurrentItem());
                }                
            }
            
            //yield return new WaitForSeconds(0.25f);

            float delay = 0.25f;
            float elapsedTime = 0f;
            while (elapsedTime < delay) {
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
    */
}
