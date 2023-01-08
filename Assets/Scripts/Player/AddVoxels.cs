using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddVoxels : MonoBehaviour {
    [SerializeField] private Transform player;

    [SerializeField] private Transform cam;
    private float rangeHit = 5.0f;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private Toolbar toolbar;

    [SerializeField] private float currentTime;

    private bool openMenu;
    
    private void Start() {
        
    }

    private void Update() {
        openMenu = CanvasManager.openMenu;

        //currentTime = 0.25f;
        
        if(!openMenu) {
            Add();
        }
    }

    private void Add() {
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
}
