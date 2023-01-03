using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voxel : MonoBehaviour {
    protected List<Vector3> vertices = new List<Vector3>();
    protected enum VoxelSide { RIGHT, LEFT, TOP, BOTTOM, FRONT, BACK }

    protected List<int> triangles = new List<int>();
    protected int vertexIndex;

    protected List<Vector2> uv = new List<Vector2>();

    protected VoxelType voxelType;
    
    private void Start() {
        
    }

    private void Update() {
        
    }

    protected void VerticesAdd(VoxelSide side, Vector3 offset) {
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

    protected void TrianglesAdd() {
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
            0, 
            0
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

    protected void UVCoordinate(VoxelSide side) {
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

        // Classic | Multiplayer Test | 0.0.19a

        // SPONGE
        // GLASS

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
        // DANDELION
        // ROSE
        // RED MUSHROOM
        // BROWN MUSHROOM

        // Classic | Survival Test | 0.26 SURVIVAL TEST

        // SMOOTH STONE SLAB
        // BLOCK OF IRON
        // TNT
        // MOSSY COBBLESTONE
        // BRICKS
        // BOOKSHELF

        // Classic | Late Classic | 0.28

        // OBSIDIAN

        // Indev | 0.31 | 20091223-2

        // TORCHE

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
        // BLOCK OF DIAMOND
        // GEAR

        // Indev | 0.31 | 20100130

        // CRAFTING TABLE

        // Indev | 20100206

        // FARMLAND
        // WHEAT CROP

        // Indev | 20100219

        // FURNACE

        // Indev | 20100607

        // LADDER
        // OAK SING
        // OAK DOOR

        // Infdev | 20100618 (Seecret Friday 1)

        // RAIL

        // Infdev | 20100624
            // Removidos todos os tipos de tecidos coloridos do jogo, embora o branco ainda exista.
            // Classic | Multiplayer Test | 0.0.20a

        // Infdev | 20100625-2 (Seecret Friday 2)

        // SPAWNER
            // Removidos os blocos geradores de água e lava.
            // Indev | 0.31 | 20100114
            // Indev | 0.31 | 20100122
        
        // Infdev | 20100629

        // OAK STAIRS
        // COBBLESTONE STAIRS

        // Alpha | 1.0 | 1.0.1 (Seecret Friday 3)

        // RESSTONE ORE
        // REDSTONE WIRE
        // REDSTONE TORCHE
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
    }
}
