using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelCrop : MonoBehaviour {
    private Mesh voxelMesh;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private List<Vector3> vertices = new List<Vector3>();
    private enum VoxelSide { RIGHT_FRONT, RIGHT_BACK, LEFT_FRONT, LEFT_BACK, FRONT_FRONT, FRONT_BACK, BACK_FRONT, BACK_BACK }

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
        VerticesAdd(VoxelSide.RIGHT_FRONT);
        VerticesAdd(VoxelSide.RIGHT_BACK);
        VerticesAdd(VoxelSide.LEFT_FRONT);
        VerticesAdd(VoxelSide.LEFT_BACK);
        VerticesAdd(VoxelSide.FRONT_FRONT);
        VerticesAdd(VoxelSide.FRONT_BACK);
        VerticesAdd(VoxelSide.BACK_FRONT);
        VerticesAdd(VoxelSide.BACK_BACK);
    }

    private void VerticesAdd(VoxelSide side) {
        switch(side) {
            case VoxelSide.RIGHT_FRONT: {
                vertices.Add(new Vector3(0.75f, 0, 0));
                vertices.Add(new Vector3(0.75f, 1, 0));
                vertices.Add(new Vector3(0.75f, 1, 1));
                vertices.Add(new Vector3(0.75f, 0, 1));
                break;
            }
            case VoxelSide.RIGHT_BACK: {
                vertices.Add(new Vector3(0.25f, 0, 0));
                vertices.Add(new Vector3(0.25f, 1, 0));
                vertices.Add(new Vector3(0.25f, 1, 1));
                vertices.Add(new Vector3(0.25f, 0, 1));
                break;
            }
            case VoxelSide.LEFT_FRONT: {
                vertices.Add(new Vector3(0.25f, 0, 1));
                vertices.Add(new Vector3(0.25f, 1, 1));
                vertices.Add(new Vector3(0.25f, 1, 0));
                vertices.Add(new Vector3(0.25f, 0, 0));
                break;
            }
            case VoxelSide.LEFT_BACK: {
                vertices.Add(new Vector3(0.75f, 0, 1));
                vertices.Add(new Vector3(0.75f, 1, 1));
                vertices.Add(new Vector3(0.75f, 1, 0));
                vertices.Add(new Vector3(0.75f, 0, 0));
                break;
            }
            case VoxelSide.FRONT_FRONT: {
                vertices.Add(new Vector3(1, 0, 0.75f));
                vertices.Add(new Vector3(1, 1, 0.75f));
                vertices.Add(new Vector3(0, 1, 0.75f));
                vertices.Add(new Vector3(0, 0, 0.75f));
                break;
            }
            case VoxelSide.FRONT_BACK: {
                vertices.Add(new Vector3(1, 0, 0.25f));
                vertices.Add(new Vector3(1, 1, 0.25f));
                vertices.Add(new Vector3(0, 1, 0.25f));
                vertices.Add(new Vector3(0, 0, 0.25f));
                break;
            }
            case VoxelSide.BACK_FRONT: {
                vertices.Add(new Vector3(0, 0, 0.25f));
                vertices.Add(new Vector3(0, 1, 0.25f));
                vertices.Add(new Vector3(1, 1, 0.25f));
                vertices.Add(new Vector3(1, 0, 0.25f));
                break;
            }
            case VoxelSide.BACK_BACK: {
                vertices.Add(new Vector3(0, 0, 0.75f));
                vertices.Add(new Vector3(0, 1, 0.75f));
                vertices.Add(new Vector3(1, 1, 0.75f));
                vertices.Add(new Vector3(1, 0, 0.75f));
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
