using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyVoxels : MonoBehaviour {
    [SerializeField] private Transform player;

    [SerializeField] private Transform cam;
    private float rangeHit = 5.0f;
    [SerializeField] private LayerMask groundMask;
    
    private void Start() {
        
    }

    private void Update() {   
        Destroy();
    }

    private void Destroy() {
        if(Input.GetMouseButtonDown(0)) {
            RaycastHit hit;

            if(Physics.Raycast(cam.position, cam.forward, out hit, rangeHit, groundMask)) {
                Vector3 pointPos = hit.point - hit.normal / 2;

                /*
                Chunk c = Chunk.GetChunk(new Vector3(
                    Mathf.FloorToInt(pointPos.x),
                    Mathf.FloorToInt(pointPos.y),
                    Mathf.FloorToInt(pointPos.z)
                ));

                c.SetBlock(pointPos, VoxelType.air);
                */
            }
        }
    }
}
