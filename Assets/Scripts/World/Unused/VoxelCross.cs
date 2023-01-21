using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelCross : MonoBehaviour {
    private Mesh voxelMesh;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private List<Vector3> vertices = new List<Vector3>();
    private enum VoxelSide { FRONT_RIGHT, BACK_RIGHT, BACK_LEFT, FRONT_LEFT }

    private List<int> triangles = new List<int>();
    private int vertexIndex;
    
    private void Start() {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();

        voxelMesh = new Mesh();
        voxelMesh.name = "Voxel";

        VoxelGen();

        MeshGen();
    }

    private void Update() {
        
    }

    private void MeshGen() {
        voxelMesh.vertices = vertices.ToArray();
        voxelMesh.triangles = triangles.ToArray();

        voxelMesh.RecalculateBounds();
        voxelMesh.RecalculateNormals();
        voxelMesh.Optimize();

        meshFilter.mesh = voxelMesh;
    }

    private void VoxelGen() {
        VerticesAdd(VoxelSide.FRONT_RIGHT);
        VerticesAdd(VoxelSide.BACK_RIGHT);
        VerticesAdd(VoxelSide.BACK_LEFT);
        VerticesAdd(VoxelSide.FRONT_LEFT);
    }

    private void VerticesAdd(VoxelSide side) {
        switch(side) {
            case VoxelSide.FRONT_RIGHT: {
                vertices.Add(new Vector3(1, 0, 0));
                vertices.Add(new Vector3(1, 1, 0));
                vertices.Add(new Vector3(0, 1, 1));
                vertices.Add(new Vector3(0, 0, 1));
                break;
            }
            case VoxelSide.BACK_RIGHT: {
                vertices.Add(new Vector3(0, 0, 0));
                vertices.Add(new Vector3(0, 1, 0));
                vertices.Add(new Vector3(1, 1, 1));
                vertices.Add(new Vector3(1, 0, 1));
                break;
            }
            case VoxelSide.BACK_LEFT: {
                vertices.Add(new Vector3(0, 0, 1));
                vertices.Add(new Vector3(0, 1, 1));
                vertices.Add(new Vector3(1, 1, 0));
                vertices.Add(new Vector3(1, 0, 0));
                break;
            }
            case VoxelSide.FRONT_LEFT: {
                vertices.Add(new Vector3(1, 0, 1));
                vertices.Add(new Vector3(1, 1, 1));
                vertices.Add(new Vector3(0, 1, 0));
                vertices.Add(new Vector3(0, 0, 0));
                break;
            }
        }

        TrianglesAdd();
    }

    private void TrianglesAdd() {
        // Primeiro Tiangulo
        triangles.Add(0 + vertexIndex);
        triangles.Add(1 + vertexIndex);
        triangles.Add(2 + vertexIndex);

        // Segundo Triangulo
        triangles.Add(0 + vertexIndex);
        triangles.Add(2 + vertexIndex);
        triangles.Add(3 + vertexIndex);

        vertexIndex += 4;
    }
}
