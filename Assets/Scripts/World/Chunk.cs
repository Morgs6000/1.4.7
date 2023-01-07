using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour {
    private Mesh voxelMesh;

    private MeshCollider meshCollider;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private List<Vector3> vertices = new List<Vector3>();
    private enum VoxelSide { RIGHT, LEFT, TOP, BOTTOM, FRONT, BACK }

    private List<int> triangles = new List<int>();
    private int vertexIndex;

    private List<Vector2> uv = new List<Vector2>();
    [SerializeField] private Material material;

    public static Vector3 ChunkSizeInVoxels = new Vector3(16, 256, 16);

    private VoxelType[,,] voxelMap = new VoxelType[(int)ChunkSizeInVoxels.x, (int)ChunkSizeInVoxels.y, (int)ChunkSizeInVoxels.z];

    private VoxelType voxelType;

    private static List<Chunk> chunkData = new List<Chunk>();
    
    private void Start() {
        meshCollider = GetComponent<MeshCollider>();
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();

        chunkData.Add(this);

        ChunkVoxelMap();

        //ClearBorderVoxels();

        //GenerateNewChunk();

        GrassColor();
    }

    private void Update() {
        
    }

    private void GrassColor() {
        // Grass Block
        // Grass
        // Tall Grass
        // Fern
        // Large Fern
        // Potted Fern
        // Sugar Cane

        // Cor referente apenas ao bioma "Forest"
        Color color;
        ColorUtility.TryParseHtmlString("#79C05A", out color);
        //meshRenderer.material.color = color;
    }

    private void FoliageColor() {
        // Oak Leaves
        // Jungle Leaves
        // Acacia Leaves
        // Dark Oak Leaves
        // Vines

        // Cor referente apenas ao bioma "Forest"
        Color color;
        ColorUtility.TryParseHtmlString("#59AE30", out color);

        //----------
        
        // Spruce Leaves
        Color color0;
        ColorUtility.TryParseHtmlString("#619961", out color0);

        // Birch Leaves
        Color color1;
        ColorUtility.TryParseHtmlString("#80A755", out color1);
    }

    /*
    private void GenerateNewChunk() {
        // Código para gerar a nova chunk...

        // Atualiza todas as chunks existentes
        foreach(Chunk chunk in chunkData) {
            chunk.ChunkRenderer();
        }
    }
    */

    /*
    private void ClearBorderVoxels() {
        for(int x = 0; x < ChunkSizeInVoxels.x; x++) {
            for(int y = 0; y < ChunkSizeInVoxels.y; y++) {
                SetBlock(new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z), VoxelType.air);
                SetBlock(new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z + ChunkSizeInVoxels.z - 1), VoxelType.air);
            }
        }

        for(int y = 0; y < ChunkSizeInVoxels.y; y++) {
            for(int z = 0; z < ChunkSizeInVoxels.z; z++) {
                SetBlock(new Vector3(transform.position.x, transform.position.y + y, transform.position.z + z), VoxelType.air);
                SetBlock(new Vector3(transform.position.x + ChunkSizeInVoxels.x - 1, transform.position.y + y, transform.position.z + z), VoxelType.air);
            }
        }

        for(int z = 0; z < ChunkSizeInVoxels.z; z++) {
            for(int x = 0; x < ChunkSizeInVoxels.x; x++) {
                SetBlock(new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z), VoxelType.air);
                SetBlock(new Vector3(transform.position.x + x, transform.position.y + ChunkSizeInVoxels.y - 1, transform.position.z + z), VoxelType.air);
            }
        }
    }
    */

    public void SetBlock(Vector3 worldPos, VoxelType b) {
        Vector3 localPos = worldPos - transform.position;

        voxelMap[
            Mathf.FloorToInt(localPos.x), 
            Mathf.FloorToInt(localPos.y), 
            Mathf.FloorToInt(localPos.z)
        ] = b;

        ChunkRenderer();
    }

    public static Chunk GetChunk(Vector3 pos) {        
        for(int i = 0; i < chunkData.Count; i++) {            
            Vector3 chunkPos = chunkData[i].transform.position;

            if(
                pos.x < chunkPos.x || pos.x >= chunkPos.x + ChunkSizeInVoxels.x || 
                pos.y < chunkPos.y || pos.y >= chunkPos.y + ChunkSizeInVoxels.y || 
                pos.z < chunkPos.z || pos.z >= chunkPos.z + ChunkSizeInVoxels.z
            ) {
                continue;
            }

            return chunkData[i];
        }

        return null;
    }

    private void TreeGen(Vector3 offset) {
        int x = (int)offset.x;
        int y = (int)offset.y;
        int z = (int)offset.z;

        float _x = x + transform.position.x;
        float _y = y + transform.position.y;
        float _z = z + transform.position.z;

        _x += (World.WorldSizeInVoxels.x);
        //_y += (World.WorldSizeInVoxels.y);
        _z += (World.WorldSizeInVoxels.z);

        if(
            Random.Range(0, 100) < 1 &&
            _y == Noise.Perlin(_x, _z) + 1
        ) {            
            //int leavesWidth = 5;
            int leavesHeight = Random.Range(3, 5);

            int iter = 0;
            
            for(int yL = y + 0; yL < y + leavesHeight; yL++) {
                for(int xL = x - 2 + iter / 2; xL <  x + 3 - iter / 2; xL++) {                
                    for(int zL = z - 2 + iter / 2; zL <  z + 3 - iter / 2; zL++) {
                        if(
                            xL >= 0 && xL < ChunkSizeInVoxels.x &&
                            yL >= 0 && yL < ChunkSizeInVoxels.y &&
                            zL >= 0 && zL < ChunkSizeInVoxels.z
                        ) {
                            voxelMap[xL, yL + 3, zL] = VoxelType.oak_leaves;
                        } 
                    }                   
                }

                iter++;
            }

            int treeHeight = Random.Range(3, 5);

            for(int i = 0; i < treeHeight; i++) {
                if(y + i < ChunkSizeInVoxels.y) {
                    voxelMap[x, y + i, z] = VoxelType.oak_log;
                }
            }
        }
    }
    
    //*
    private void VoxelLayers(Vector3 offset) {
        int x = (int)offset.x;
        int y = (int)offset.y;
        int z = (int)offset.z;

        float _x = x + transform.position.x;
        float _y = y + transform.position.y;
        float _z = z + transform.position.z;

        _x += (World.WorldSizeInVoxels.x);
        //_y += (World.WorldSizeInVoxels.y);
        _z += (World.WorldSizeInVoxels.z);

        if(
            _y == 0 ||
            _y <= 4 && 
            Random.Range(0, 100) < 50            
        ) {
            voxelMap[x, y, z] = VoxelType.bedrock;
        }
        else if(
            _y >= 6 &&
            Noise.Perlin3D(_x * 0.05f, _y * 0.05f, _z * 0.05f) >= 0.5f &&
            _y < Noise.Perlin(_x, _z) - 5
        ) {
            voxelMap[x, y, z] = VoxelType.air;
        }
        else if(_y < Noise.Perlin(_x, _z) - 4) {
            voxelMap[x, y, z] = VoxelType.stone;
        }
        else if(_y < Noise.Perlin(_x, _z)) {
            voxelMap[x, y, z] = VoxelType.dirt;
        }
        else if(_y == Noise.Perlin(_x, _z)) {
            voxelMap[x, y, z] = VoxelType.grass_block;
        }
        else {
            voxelMap[x, y, z] = VoxelType.air;
        }
    }
    //*/

    /*
    private VoxelType VoxelLayers(Vector3 offset) {
        int x = (int)offset.x;
        int y = (int)offset.y;
        int z = (int)offset.z;

        if(
            y == 0 ||
            y <= 4 && 
            Random.Range(0, 100) < 50            
        ) {
            return VoxelType.bedrock;
        }
        else if(
            y >= 6 &&
            Noise.Perlin3D(x * 0.05f, y * 0.05f, z * 0.05f) >= 0.5f &&
            y < Noise.Perlin(x, z) - 5
        ) {
            return VoxelType.air;
        }
        else if(y < Noise.Perlin(x, z) - 4) {
            return VoxelType.stone;
        }
        else if(y < Noise.Perlin(x, z)) {
            return VoxelType.dirt;
        }
        else if(y == Noise.Perlin(x, z)) {
            return VoxelType.grass_block;
        }
        else {
            return VoxelType.air;
        }
    }
    //*/

    private void ChunkVoxelMap() {
        for(int x = 0; x < ChunkSizeInVoxels.x; x++) {
            for(int y = 0; y < ChunkSizeInVoxels.y; y++) {
                for(int z = 0; z < ChunkSizeInVoxels.z; z++) {
                    //voxelMap[x, y, z] = VoxelType.stone;
                    VoxelLayers(new Vector3(x, y, z));
                    //voxelMap[x, y, z] = VoxelLayers(new Vector3(x, y, z) + transform.position);                    
                    TreeGen(new Vector3(x, y, z));
                }
            }
        }

        ChunkRenderer();
    }

    private void ChunkRenderer() {
        voxelMesh = new Mesh();
        voxelMesh.name = "Chunk";

        vertices.Clear();
        triangles.Clear();
        uv.Clear();

        vertexIndex = 0;

        for(int x = 0; x < ChunkSizeInVoxels.x; x++) {
            for(int y = 0; y < ChunkSizeInVoxels.y; y++) {
                for(int z = 0; z < ChunkSizeInVoxels.z; z++) {
                    if(voxelMap[x, y, z] != VoxelType.air) {
                        VoxelGen(new Vector3(x, y, z));
                    }
                }
            }
        }

        MeshGen();
    }

    void MeshGen() {
        voxelMesh.vertices = vertices.ToArray();
        voxelMesh.triangles = triangles.ToArray();
        voxelMesh.uv = uv.ToArray();

        voxelMesh.RecalculateBounds();
        voxelMesh.RecalculateNormals();
        voxelMesh.Optimize();

        meshCollider.sharedMesh = voxelMesh;
        meshFilter.mesh = voxelMesh;
        meshRenderer.material = material;
    }

    private void VoxelGen(Vector3 offset) {
        int x = (int)offset.x;
        int y = (int)offset.y;
        int z = (int)offset.z;

        voxelType = voxelMap[x, y, z];

        if(!HasSolidNeighbor(new Vector3(1, 0, 0) + offset)) {
            VerticesAdd(VoxelSide.RIGHT, offset);
        }
        if(!HasSolidNeighbor(new Vector3(-1, 0, 0) + offset)) {
            VerticesAdd(VoxelSide.LEFT, offset);
        }
        if(!HasSolidNeighbor(new Vector3(0, 1, 0) + offset)) {
            VerticesAdd(VoxelSide.TOP, offset);
        }
        if(!HasSolidNeighbor(new Vector3(0, -1, 0) + offset)) {
            VerticesAdd(VoxelSide.BOTTOM, offset);
        }
        if(!HasSolidNeighbor(new Vector3(0, 0, 1) + offset)) {
            VerticesAdd(VoxelSide.FRONT, offset);
        }
        if(!HasSolidNeighbor(new Vector3(0, 0, -1) + offset)) {
            VerticesAdd(VoxelSide.BACK, offset);
        }
    }

    bool HasSolidNeighbor(Vector3 offset) {
        int x = (int)offset.x;
        int y = (int)offset.y;
        int z = (int)offset.z;

        //*
        if(
            x < 0 || x > ChunkSizeInVoxels.x - 1 ||
            y < 0 || y > ChunkSizeInVoxels.y - 1 ||
            z < 0 || z > ChunkSizeInVoxels.z - 1
        ) {
            return false;
        }
        if(voxelMap[x, y, z] == VoxelType.air) {
            return false;
        }

        //return true;
        //*/

        //----------

        /*
        // Verifica se o índice x está fora dos limites da chunk
        if(x < 0 || x >= ChunkSizeInVoxels.x - 1) {
            return false;
        }

        // Verifica se o índice y está fora dos limites da chunk
        if(y < 0 || y >= ChunkSizeInVoxels.y - 1) {
            return false;
        }

        // Verifica se o índice z está fora dos limites da chunk
        if(z < 0 || z >= ChunkSizeInVoxels.z - 1) {
            return false;
        }
        */

        /*
        // Verifica se o voxel (x, y, z) tem um vizinho sólido na direção "right" (lado direito)
        if(x < ChunkSizeInVoxels.x - 1 && voxelMap[x + 1, y, z] != VoxelType.air) {
            return true;
        }

        // Verifica se o voxel (x, y, z) tem um vizinho sólido na direção "left" (lado esquerdo)
        if(x > 0 && voxelMap[x - 1, y, z] != VoxelType.air) {
            return true;
        }

        // Verifica se o voxel (x, y, z) tem um vizinho sólido na direção "top" (parte de cima)
        if(y < ChunkSizeInVoxels.y - 1 && voxelMap[x, y + 1, z] != VoxelType.air) {
            return true;
        }

        // Verifica se o voxel (x, y, z) tem um vizinho sólido na direção "bottom" (parte de baixo)
        if(y > 0 && voxelMap[x, y - 1, z] != VoxelType.air) {
            return true;
        }

        // Verifica se o voxel (x, y, z) tem um vizinho sólido na direção "front" (parte da frente)
        if(z < ChunkSizeInVoxels.z - 1 && voxelMap[x, y, z + 1] != VoxelType.air) {
            return true;
        }

        // Verifica se o voxel (x, y, z) tem um vizinho sólido na direção "back" (parte de trás)
        if(z > 0 && voxelMap[x, y, z - 1] != VoxelType.air) {
            return true;
        }

        // Verifica se o voxel (x, y, z) tem um vizinho sólido na direção "right" que pertence a outra chunk
        if(x == ChunkSizeInVoxels.x - 1) {
            Chunk rightChunk = GetChunk(new Vector3(transform.position.x + ChunkSizeInVoxels.x, transform.position.y, transform.position.z));
            if(rightChunk.voxelMap[0, y, z] != VoxelType.air) {
                return true;
            }
        }
        
        // Verifica se o voxel (x, y, z) tem um vizinho sólido na direção "left" que pertence a outra chunk
        if(x == 0) {
            Chunk leftChunk = GetChunk(new Vector3(transform.position.x - ChunkSizeInVoxels.x, transform.position.y, transform.position.z));
            if(leftChunk.voxelMap[(int)ChunkSizeInVoxels.z - 1, y, z] != VoxelType.air) {
                return true;
            }
        }

        // Verifica se o voxel (x, y, z) tem um vizinho sólido na direção "front" que pertence a outra chunk
        if(z == ChunkSizeInVoxels.z - 1) {
            Chunk frontChunk = GetChunk(new Vector3(transform.position.x, transform.position.y, transform.position.z + ChunkSizeInVoxels.z));
            if(frontChunk.voxelMap[x, y, 0] != VoxelType.air) {
                return true;
            }
        }

        // Verifica se o voxel (x, y, z) tem um vizinho sólido na direção "back" que pertence a outra chunk
        if(z == 0) {
            Chunk backChunk = GetChunk(new Vector3(transform.position.x, transform.position.y, transform.position.z - ChunkSizeInVoxels.z));
            if(backChunk.voxelMap[x, y, (int)ChunkSizeInVoxels.z - 1] != VoxelType.air) {
                return true;
            }
        }
        //*/

        //return false;
        return true;
    }

    private void VerticesAdd(VoxelSide side, Vector3 offset) {
        switch(side) {
            case VoxelSide.RIGHT: {
                vertices.Add(new Vector3(1, 0, 0) + offset);
                vertices.Add(new Vector3(1, 1, 0) + offset);
                vertices.Add(new Vector3(1, 1, 1) + offset);
                vertices.Add(new Vector3(1, 0, 1) + offset);
                break;
            }
            case VoxelSide.LEFT: {
                vertices.Add(new Vector3(0, 0, 1) + offset);
                vertices.Add(new Vector3(0, 1, 1) + offset);
                vertices.Add(new Vector3(0, 1, 0) + offset);
                vertices.Add(new Vector3(0, 0, 0) + offset);
                break;
            }
            case VoxelSide.TOP: {
                vertices.Add(new Vector3(0, 1, 0) + offset);
                vertices.Add(new Vector3(0, 1, 1) + offset);
                vertices.Add(new Vector3(1, 1, 1) + offset);
                vertices.Add(new Vector3(1, 1, 0) + offset);
                break;
            }
            case VoxelSide.BOTTOM: {
                vertices.Add(new Vector3(0, 0, 1) + offset);
                vertices.Add(new Vector3(0, 0, 0) + offset);
                vertices.Add(new Vector3(1, 0, 0) + offset);
                vertices.Add(new Vector3(1, 0, 1) + offset);
                break;
            }
            case VoxelSide.FRONT: {
                vertices.Add(new Vector3(1, 0, 1) + offset);
                vertices.Add(new Vector3(1, 1, 1) + offset);
                vertices.Add(new Vector3(0, 1, 1) + offset);
                vertices.Add(new Vector3(0, 0, 1) + offset);
                break;
            }
            case VoxelSide.BACK: {
                vertices.Add(new Vector3(0, 0, 0) + offset);
                vertices.Add(new Vector3(0, 1, 0) + offset);
                vertices.Add(new Vector3(1, 1, 0) + offset);
                vertices.Add(new Vector3(1, 0, 0) + offset);
                break;
            }
        }

        TrianglesAdd();

        UVCoordinate(side);
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

    private void UVAdd(Vector2 textureCoordinate) {
        Vector2 offset = new Vector2(
            1, 
            1
        );

        Vector2 textureSizeInTiles = new Vector2(
            16 + offset.x,
            16 + offset.y
        );
        
        float x = textureCoordinate.x + offset.x;
        float y = textureCoordinate.y + offset.y;

        float _x = 1.0f / textureSizeInTiles.x;
        float _y = 1.0f / textureSizeInTiles.y;

        y = (textureSizeInTiles.y - 1) - y;

        x *= _x;
        y *= _y;

        uv.Add(new Vector2(x, y));
        uv.Add(new Vector2(x, y + _y));
        uv.Add(new Vector2(x + _x, y + _y));
        uv.Add(new Vector2(x + _x, y));
    }

    private void UVCoordinate(VoxelSide side) {
        // Pre-Classic | rd-131655 (Cave game tech test)
        
        // AIR
        // STONE
        if(voxelType == VoxelType.stone) {
            UVAdd(new Vector2(1, 0));
        }

        // GRASS BLOCK
        if(voxelType == VoxelType.grass_block) {
            if(side == VoxelSide.TOP) {
                UVAdd(new Vector2(0, 0));
                return;
            }
            if(side == VoxelSide.BOTTOM) {
                UVAdd(new Vector2(2, 0));
                return;
            }

            UVAdd(new Vector2(3, 0));
        }

        // Pre-Classic | rd-20090515

        // DIRT
        if(voxelType == VoxelType.dirt) {
            UVAdd(new Vector2(2, 0));
        }

        // OAK PLANK
        if(voxelType == VoxelType.oak_planks) {
            UVAdd(new Vector2(4, 0));
        }

        // COBBLESTONE
        if(voxelType == VoxelType.cobblestone) {
            UVAdd(new Vector2(0, 1));
        }

        // Pre-Classic | rd-161348

        // OAK SAPLING
        if(voxelType == VoxelType.oak_sapling) {
            UVAdd(new Vector2(15, 0));
        }

        // Classic | Early Classic | 0.0.12a

        // BEDROCK
        if(voxelType == VoxelType.bedrock) {
            UVAdd(new Vector2(1, 1));
        }

        // WATER
        if(voxelType == VoxelType.water) {
            UVAdd(new Vector2(13, 12));
        }

        // LAVA
        if(voxelType == VoxelType.lava) {
            UVAdd(new Vector2(13, 14));
        }

        // Classic | Early Classic | 0.0.14a

        // SAND
        if(voxelType == VoxelType.sand) {
            UVAdd(new Vector2(2, 1));
        }

        // GRAVEL
        if(voxelType == VoxelType.gravel) {
            UVAdd(new Vector2(3, 1));
        }

        // COAL ORE
        if(voxelType == VoxelType.coal_ore) {
            UVAdd(new Vector2(2, 2));
        }

        // IRON ORE
        if(voxelType == VoxelType.iron_ore) {
            UVAdd(new Vector2(1, 2));
        }

        // GOLD ORE
        if(voxelType == VoxelType.gold_ore) {
            UVAdd(new Vector2(0, 2));
        }

        // OAK LOG
        if(voxelType == VoxelType.oak_log) {
            if(
                side == VoxelSide.TOP || 
                side == VoxelSide.BOTTOM
            ) {
                UVAdd(new Vector2(5, 1));
                return;
            }

            UVAdd(new Vector2(4, 1));
        }
        
        // OAK LEAVES
        if(voxelType == VoxelType.oak_leaves) {
            //UVAdd(new Vector2(4, 3));
            UVAdd(new Vector2(5, 3));
        }

        /*
        // Classic | Multiplayer Test | 0.0.19a

        // SPONGE
        if(voxelType == VoxelType.sponge) {
            UVAdd(new Vector2(0, 3));
        }

        // GLASS
        if(voxelType == VoxelType.glass) {
            UVAdd(new Vector2(1, 3));
        }

        // Classic | Multiplayer Test | 0.0.20a

        // WHITE CLOTH
        // LIGHT GRAY CLOTH
        // DARK GRAY CLOTH
        // RED CLOTH
        // ORANGE CLOTH
        // YELLOW CLOTH
        // CHARTREUSE CLOTH
        // GREEN CLOTH
        // SPRING GREEN CLOTH
        // CYAN CLOTH
        // CAPRI CLOTH
        // ULTRAMARINE CLOTH
        // VIOLET CLOTH
        // PURPLE CLOTH
        // MAGENTA CLOTH
        // ROSE CLOTH

        // BLOCK OF GOLD
        if(voxelType == VoxelType.block_of_gold) {
            UVAdd(new Vector2(7, 1));
        }

        // DANDELION
        if(voxelType == VoxelType.dandelion) {
            UVAdd(new Vector2(13, 0));
        }

        // ROSE
        if(voxelType == VoxelType.rose) {
            UVAdd(new Vector2(12, 0));
        }

        // RED MUSHROOM
        if(voxelType == VoxelType.red_mushroom) {
            UVAdd(new Vector2(12, 1));
        }

        // BROWN MUSHROOM
        if(voxelType == VoxelType.brown_mushroom) {
            UVAdd(new Vector2(13, 1));
        }

        // Classic | Survival Test | 0.26 SURVIVAL TEST

        // SMOOTH STONE SLAB        
        if(voxelType == VoxelType.smooth_stone_slab) {
            if(
                side == VoxelSide.TOP || 
                side == VoxelSide.BOTTOM
            ) {
                UVAdd(new Vector2(6, 0));
                return;
            }

            UVAdd(new Vector2(5, 0));
        }

        // BLOCK OF IRON
        if(voxelType == VoxelType.block_of_iron) {
            UVAdd(new Vector2(6, 1));
        }

        // TNT
        if(voxelType == VoxelType.tnt) {
            if(side == VoxelSide.TOP) {
                UVAdd(new Vector2(9, 0));
                return;
            }
            if(side == VoxelSide.BOTTOM) {
                UVAdd(new Vector2(10, 0));
                return;
            }

            UVAdd(new Vector2(8, 0));
        }


        // MOSSY COBBLESTONE
        if(voxelType == VoxelType.mossy_cobblestone) {
            UVAdd(new Vector2(4, 2));
        }

        // BRICKS
        if(voxelType == VoxelType.bricks) {
            UVAdd(new Vector2(7, 0));
        }

        // BOOKSHELF
        if(voxelType == VoxelType.bookshelf) {
            if(
                side == VoxelSide.TOP || 
                side == VoxelSide.BOTTOM
            ) {
                UVAdd(new Vector2(4, 0));
                return;
            }

            UVAdd(new Vector2(3, 2));
        }

        // Classic | Late Classic | 0.28

        // OBSIDIAN
        if(voxelType == VoxelType.obsidian) {
            UVAdd(new Vector2(5, 2));
        }

        // Indev | 0.31 | 20091223-2

        // TORCHE
        if(voxelType == VoxelType.torche) {
            UVAdd(new Vector2(0, 5));
        }

        // Indev | 0.31 | 20100109

        // FIRE

        // Indev | 0.31 | 20100114

        // WATER SPAWNER

        // Indev | 0.31 | 20100122

        // LAVA SPAWNER

        // Indev | 0.31 | 20100124

        // CHEST

        // Indev | 0.31 | 20100128

        // DIAMOND ORE
        if(voxelType == VoxelType.diamond_ore) {
            UVAdd(new Vector2(2, 3));
        }

        // BLOCK OF DIAMOND
        if(voxelType == VoxelType.block_of_diamond) {
            UVAdd(new Vector2(8, 1));
        }

        // GEAR

        // Indev | 0.31 | 20100130

        // CRAFTING TABLE
        if(voxelType == VoxelType.crafting_table) {
            if(side == VoxelSide.TOP) {
                UVAdd(new Vector2(11, 2));
                return;
            }
            if(side == VoxelSide.BOTTOM) {
                UVAdd(new Vector2(4, 0));
                return;
            }
            if(
                side == VoxelSide.FRONT ||
                side == VoxelSide.BACK 
            ) {
                UVAdd(new Vector2(11, 3));
                return;
            }
            if(
                side == VoxelSide.RIGHT ||
                side == VoxelSide.LEFT 
            ) {
                UVAdd(new Vector2(12, 3));
                return;
            }
        }

        // Indev | 20100206

        // FARMLAND
        if(voxelType == VoxelType.farmland) {
            if(side == VoxelSide.TOP) {
                UVAdd(new Vector2(7, 5));
                return;
            }

            UVAdd(new Vector2(2, 0));
        }

        // WHEAT CROP
        if(voxelType == VoxelType.wheat_crop) {
            UVAdd(new Vector2(15, 5));
        }

        // Indev | 20100219

        // FURNACE
        if(voxelType == VoxelType.furnace) {
            if(
                side == VoxelSide.TOP || 
                side == VoxelSide.BOTTOM
            ) {
                UVAdd(new Vector2(0, 1));
                return;
            }
            if(side == VoxelSide.FRONT) {
                UVAdd(new Vector2(12, 2));
                return;
            }
            
            UVAdd(new Vector2(13, 2));
        }

        // Indev | 20100607

        // LADDER
        if(voxelType == VoxelType.ladder) {
            UVAdd(new Vector2(3, 5));
        }

        // OAK SING
        // OAK DOOR

        // Infdev | 20100618 (Seecret Friday 1)

        // RAIL
        if(voxelType == VoxelType.rail) {
            UVAdd(new Vector2(0, 8));
        }

        // Infdev | 20100624
            // Removidos todos os tipos de tecidos coloridos do jogo, embora o branco ainda exista.
            // Classic | Multiplayer Test | 0.0.20a

        // Infdev | 20100625-2 (Seecret Friday 2)

        // SPAWNER
        if(voxelType == VoxelType.spawner) {
            UVAdd(new Vector2(1, 4));
        }

            // Removidos os blocos geradores de água e lava.
            // Indev | 0.31 | 20100114
            // Indev | 0.31 | 20100122
        
        // Infdev | 20100629

        // OAK STAIRS
        // COBBLESTONE STAIRS

        // Alpha | 1.0 | 1.0.1 (Seecret Friday 3)

        // RESSTONE ORE
        if(voxelType == VoxelType.redstone_ore) {
            UVAdd(new Vector2(3, 3));
        }

        // REDSTONE WIRE
        if(voxelType == VoxelType.redstone_wire) {
            UVAdd(new Vector2(5, 10));
        }

        // REDSTONE TORCHE
        if(voxelType == VoxelType.redstone_torche) {
            UVAdd(new Vector2(3, 6));
        }

        // OAK PRESSURE PLATE
        // STONE PRESSURE PLATE
        // STONE BUTTON
        // LEVER
        // IRON DOOR
            // Engrenagens removidas.
            // Indev | 0.31 | 20100128
        
        // Alpha | 1.0 | 1.0.4 (Seecret Friday 4)

        // SNOW
        // SNOW BLOCK
        // ICE

        // Alpha | 1.0 | 1.0.6 (Seecret Friday 5)

        // CACTUS

        // Alpha | 1.0 | 1.0.11 (Seecret Friday 6)

        // CLAY
        // SUGAR CANE

        // Alpha | 1.0 | 1.0.14 (Seecret Friday 7)

        // JUKEBOXE

        // Alpha | 1.0 | 1.0.17 (Seecret Friday 8)

        // OAK FENCE

        // Alpha | 1.2 Halloween Update | 1.2.0

        // NETHERRACK
        // SOUL SAND
        // GLOWSTONE
        // CARVED PUMPKIN
        // JACK o'LANTERN
        // NETHER PORTAL BLOCK

        // Beta | 1.2

        // WHITE WOOL
        // LIGHT GRAY WOOL
        // GRAY WOOL
        // BLACK WOOL
        // BROWN WOOL
        // RED WOOL
        // ORANGE WOOL
        // YELLOW WOOL
        // LIME WOOL
        // GREEN WOOL
        // CYAN WOOL
        // LIGHT BLUE WOOL
        // BLUE WOOL
        // MAGENTA WOOL
        // PURPLE WOOL
        // PINK WOOL
        // CAKE
        // DISPENSER
        // LAPIS LAZULI ORE
        // BLOCK OF LAPIS LAZULI
        // NOTE BLOCK
        // SANDSTONE
        // BIRCH LOG
        // BIRCH LEAVES
        // SPRUCE LOG
        // SPRUCE LEAVES

        // Beta | 1.3

        // COBBLESTON SLAB
        // PETRIFIED OAK SLAB
        // SANDSTONE SLAB
        // RED BAD
        // REDSTONE REPEATER

        // Beta | 1.4

        // LOCKED CHEST

        // Beta | 1.5

        // BIRCH SAPLING
        // SPRUCE SAPLING
        // POWERED RAIL
        // DETECTOR RAIL
        // COBWEB

        // Beta | 1.6

        // DEAD BUSH
        // SHURB
        // GRASS
        // FERN
        // OAK TRAPDOOR

        // Beta | 1.7

        // PISTON
        // STICKY PISTON

        // Beta | 1.8 Adventure Update (Part 1)

        // STONE BRICK
        // CRACKED STONE BRICK
        // MOSSY STONE BRICK
        // INFESTED STONE
        // INFESTED COBBLESTONE
        // INFESTED STONE BRICK
        // BRICK SLAB
        // STONE BRICK SLAB
        // BRICK STAIRS
        // STONE BRICK STAIRS
        // GLASS PLANE
        // IRON BARS
        // OAK FENCE GATE
        // VINES
        // MUSHROOM STEM
        // RED MUSHROOM BLOCK
        // BROWN MUSHROOM BLOCK
        // PUMPKIN STEM
        // MELON STEM
        // MELON

        // 1.0 Adventure Update (Part 2) | 1.0.0 | Beta 1.9 Prerelease

        // NETHER BRICK
        // NETHER BRICK STAIRS
        // NETHER BRICK FENCE
        // NETHER WART
        // MYCELIUM
        // LILY PAD

        // 1.0 Adventure Update (Part 2) | 1.0.0 | Beta 1.9 Prerelease 3

        // BREWING STAND
        // ENCHANTING TABLE
        // END PORTAL FRAME
        // END PORTAL BLOCK
        // CAULDRON

        // 1.0 Adventure Update (Part 2) | 1.0.0 | Beta 1.9 Prerelease 4

        // END STONE

        // 1.0 Adventure Update (Part 2) | 1.0.0 | Beta 1.9 Prerelease 6

        // DRAGON EGG

        // 1.2 | 1.2.1 | 12w03a

        // JUNGLE LOG
        // JUNGLE LEAVES

        // 1.2 | 1.2.1 | 12w04a

        // JUNGLE SAPLING

        // 1.2 | 1.2.1 | 12w07a

        // REDSTONE LAMP

        // 1.2 | 1.2.1 | 1.2

        // CHISELD STONE BRICK

        // 1.2 | 1.2.4

        // BIRCH PLANK
        // SPRUCE PLANK
        // JUNGLE PLANK
        // CUT SANDSTONE
        // CHISELED SANDSTONE

        // 1.3 | 1.3.1 | 12w17a

        // OAK SLAB
        // BIRCH SLAB
        // SPRUCE SLAB
        // JUNGLE SLAB

        // 1.3 | 1.3.1 | 12w19a

        // COCOA

        // 1.3 | 1.3.1 | 12w21a

        // EMERALD ORE
        // ENDER CHEST
        // SANDSTONE STAIRS

        // 1.3 | 1.3.1 | 12w22a

        // TRIPWIRE
        // TRIPWIRE HOOK
        // BLOCK OF EMERALD

        // 1.3 | 1.3.1 | 12w25a

        // BIRCH STAIRS
        // SPRUCE STAIRS
        // JUNGLE STAIRS

        // 1.3 | 1.3.1 | 12w30d

        // OAK WOOD
        // BIRCH WOOD
        // SPRUCE WOOD
        // JUNGLE WOOD

        // 1.4 Pretty Scary Update | 1.4.2 | 12w32a

        // BEACON
        // COMMAND BLOCK

        // 1.4 Pretty Scary Update | 1.4.2 | 12w34a

        // CARRONT
        // POTATO
        // FLOWER POT
        // OAK BUTTON
        // COBBLESTONE WALL
        // MOSSY COBBLESTONE WALL

        // 1.4 Pretty Scary Update | 1.4.2 | 12w36a

        // CREEPER HEAD
        // PLAYER HEAD
        // SKELETON SKULL
        // WITHER SKELETON SKULL
        // ZOMBIE HEAD

        // 1.4 Pretty Scary Update | 1.4.2 | 12w41a

        // ANVIL
        // CHIPPED ANVIL
        // DAMAGE ANVIL

        // 1.4 Pretty Scary Update | 1.4.6 | 12w49a

        // NETHER BRICK SLAB
        */
    }
}
