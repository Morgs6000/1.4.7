using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VoxelType {
    // Pre-Classic | rd-131655 (Cave game tech test)
    air,
    stone,
    grass_block,

    // Pre-Classic | rd-20090515
    dirt,
    oak_planks,
    cobblestone,

    // Pre-Classic | rd-161348
    oak_sapling,

    // Classic | Early Classic | 0.0.12a
    bedrock,
    water,
    lava,

    // Classic | Early Classic | 0.0.14a
    sand,
    gravel,
    coal_ore,
    iron_ore,
    gold_ore,
    oak_log,
    oak_leaves,

    // Classic | Multiplayer Test | 0.0.19a
    sponge,
    glass,

    // Classic | Multiplayer Test | 0.0.20a
    white_cloth,
    light_gray_cloth,
    dark_gray_cloth,
    red_cloth,
    orange_cloth,
    yellow_cloth,
    chartreuse_cloth,
    green_cloth,
    spring_green_cloth,
    cyan_cloth,
    capri_cloth,
    ultramarine_cloth,
    violet_cloth,
    purple_cloth,
    magenta_cloth,
    rose_cloth,
    block_of_gold,
    dandelion,
    rose,
    red_mushroom,
    brown_mushroom,

    // Classic | Survival Test | 0.26 SURVIVAL TEST
    smooth_stone_slab,
    block_of_iron,
    tnt,
    mossy_cobblestone,
    bricks,
    bookshelf,

    // Classic | Late Classic | 0.28
    obsidian,

    // Indev | 0.31 | 20091223-2
    torche,

    // Indev | 0.31 | 20100109
    fire,

    // Indev | 0.31 | 20100114
    water_spawner,

    // Indev | 0.31 | 20100122
    lava_spawner,

    // Indev | 0.31 | 20100124
    chest,

    // Indev | 0.31 | 20100128
    diamond_ore,
    block_of_diamond,
    gear,

    // Indev | 0.31 | 20100130
    crafting_table,

    // Indev | 20100206
    farmland,
    wheat_crop,

    // Indev | 20100219
    furnace,

    // Indev | 20100607
    ladder,
    oak_sing,
    oak_door,

    // Infdev | 20100618 (Seecret Friday 1)
    rail,

    // Infdev | 20100624
        // Removidos todos os tipos de tecidos coloridos do jogo, embora o branco ainda exista.
        // Classic | Multiplayer Test | 0.0.20a

    // Infdev | 20100625-2 (Seecret Friday 2)
    spawner,
        // Removidos os blocos geradores de água e lava.
        // Indev | 0.31 | 20100114
        // Indev | 0.31 | 20100122
    
    // Infdev | 20100629
    oak_stairs,
    cobblestone_stairs,

    // Alpha | 1.0 | 1.0.1 (Seecret Friday 3)
    redstone_ore,
    redstone_wire,
    redstone_torche,
    oak_pressure_plate,
    stone_pressure_plate,
    stone_button,
    lever,
    iron_door,
        // Engrenagens removidas.
        // Indev | 0.31 | 20100128
    
    // Alpha | 1.0 | 1.0.4 (Seecret Friday 4)
    snow,
    snow_block,
    ice,

    // Alpha | 1.0 | 1.0.6 (Seecret Friday 5)
    cactus,

    // Alpha | 1.0 | 1.0.11 (Seecret Friday 6)
    clay,
    sugar_cane,

    // Alpha | 1.0 | 1.0.14 (Seecret Friday 7)
    jukebox,

    // Alpha | 1.0 | 1.0.17 (Seecret Friday 8)
    oak_fence,

    // Alpha | 1.2 Halloween Update | 1.2.0
    netherrack,
    soul_sand,
    glowstone,
    carved_pumpkin,
    jack_o_lantern,
    nether_portal_block,

    // Beta | 1.2
    white_wool,
    light_gray_wool,
    gray_wool,
    black_wool,
    brown_wool,
    red_wool,
    orange_wool,
    yellow_wool,
    lime_wool,
    green_wool,
    cyan_wool,
    light_blue_wool,
    blue_wool,
    magenta_wool,
    purple_wool,
    pink_wool,
    cake,
    dispenser,
    lapis_lazuli_ore,
    block_of_lapis_lazuli,
    note_block,
    sandstone,
    birch_log,
    birch_leaves,
    spruce_log,
    spruce_leaves,

    // Beta | 1.3
    cobblestone_slab,
    petrified_oak_slab,
    sandstone_slab,
    smooth_stone,
    red_bad,
    redstone_repeater,

    // Beta | 1.4
    locked_chest,

    // Beta | 1.5
    birch_sapling,
    spruce_sapling,
    powered_rail,
    detector_rail,
    cobweb,

    // Beta | 1.6
    dead_bush,
    shurb,
    grass,
    fern,
    oak_trapdoor,

    // Beta | 1.7
    piston,
    sticky_piston,

    // Beta | 1.8 Adventure Update (Part 1)
    stone_brick,
    cracked_stone_brick,
    mossy_stone_brick,
    infested_stone,
    infested_cobblestone,
    infested_stone_brick,
    brick_slab,
    stone_brick_slab,
    brick_stairs,
    stone_brick_stairs,
    glass_pane,
    iron_bars,
    oak_fance_gate,
    vines,
    mushroom_stem,
    red_mushroom_block,
    brown_mushroom_block,
    pumpkin_stem,
    melon_stem,
    melon,

    // 1.0 Adventure Update (Part 2) | 1.0.0 | Beta 1.9 Prerelease
    nether_brick,
    nether_brick_stairs,
    nether_brick_fence,
    nether_wart,
    mycelium,
    lily_pad,

    // 1.0 Adventure Update (Part 2) | 1.0.0 | Beta 1.9 Prerelease 3
    brewing_stand,
    enchanting_table,
    end_portal_frame,
    end_portal_block,
    cauldron,

    // 1.0 Adventure Update (Part 2) | 1.0.0 | Beta 1.9 Prerelease 4
    end_stone,

    // 1.0 Adventure Update (Part 2) | 1.0.0 | Beta 1.9 Prerelease 6
    dragon_egg,

    // 1.2 | 1.2.1 | 12w03a
    jungle_log,
    jungle_leaves,

    // 1.2 | 1.2.1 | 12w04a
    jungle_sapling,

    // 1.2 | 1.2.1 | 12w07a
    redstone_lamp,

    // 1.2 | 1.2.1 | 1.2
    chiseled_stone_brick,

    // 1.2 | 1.2.4
    birch_plank,
    spruce_plank,
    jungle_plank,
    cut_sandstone,
    chiseled_sandstone,

    // 1.3 | 1.3.1 | 12w17a
    oak_slab,
    birch_slab,
    spruce_slab,
    jungle_slab,

    // 1.3 | 1.3.1 | 12w19a
    cocoa,

    // 1.3 | 1.3.1 | 12w21a
    emerald_ore,
    ender_chest,
    sandstone_stairs,

    // 1.3 | 1.3.1 | 12w22a
    tripwire,
    tripwire_hook,
    block_of_emerald,

    // 1.3 | 1.3.1 | 12w25a
    birch_stairs,
    spruce_stairs,
    jungle_stairs,

    // 1.3 | 1.3.1 | 12w30d
    oak_wood,
    birch_wood,
    spruce_wood,
    jungle_wood,

    // 1.4 Pretty Scary Update | 1.4.2 | 12w32a
    beacon,
    command_block,

    // 1.4 Pretty Scary Update | 1.4.2 | 12w34a
    carrot,
    potato,
    flower_pot,
    oak_butoon,
    cobblestone_wall,
    mossy_cobblestone_wall,

    // 1.4 Pretty Scary Update | 1.4.2 | 12w36a
    creeper_head,
    player_head,
    skeleton_skull,
    wither_skeleton_skull,
    zombie_head,

    // 1.4 Pretty Scary Update | 1.4.2 | 12w41a
    anvil,
    chipped_anvil,
    damage_anvil,

    // 1.4 Pretty Scary Update | 1.4.6 | 12w49a
    nether_brick_slab,
}
