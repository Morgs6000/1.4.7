using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpriteCreator : MonoBehaviour {
    [SerializeField] private Texture2D atlasTexture;
    private Sprite sprite;
    
    private void OnValidate() {
        SpriteGen(new Vector2(0, 0));      

        AssetDatabase.CreateAsset(sprite, "Assets/Textures/gui/crosshair.asset");
    }
    
    private void Start() {
        
    }

    private void Update() {
        
    }

    private void SpriteGen(Vector2 textureCoordinate) {
        Vector2 textureSizeInPixels = new Vector2(
            atlasTexture.width,
            atlasTexture.height
        );
        
        Vector2 textureSizeInTiles = new Vector2(
            16,
            16
        );
        
        float x = textureCoordinate.x;
        float y = textureCoordinate.y;

        float width = textureSizeInPixels.x / textureSizeInTiles.x;
        float height = textureSizeInPixels.y / textureSizeInTiles.y;

        y = (textureSizeInTiles.y - 1) - y;

        x *= width;
        y *= height;

        Rect rect = new Rect(x, y, width, height);

        sprite = Sprite.Create(atlasTexture, rect, Vector2.zero);
    }
}
