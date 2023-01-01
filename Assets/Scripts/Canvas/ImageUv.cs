using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageUv : MonoBehaviour {
    private Image image;

    [SerializeField] private Texture2D atlas;

    private Sprite sprite;
    
    private void Start() {
        image = GetComponent<Image>();

        CreateSpriteFromTopLeftTile(new Vector2(0, 0));
        image.sprite = sprite;
        image.sprite.name = "crosshair";
    }

    private void Update() {
        
    }

    private void CreateSpriteFromTopLeftTile(Vector2 textureCoordinate) {
        Vector2 textureSizeInPixels = new Vector2(
            256,
            256
        );
        
        Vector2 textureSizeInTiles = new Vector2(
            16,
            16
        );

        float x = textureCoordinate.x;
        float y = textureCoordinate.y;

        float _x = /*1.0f / */textureSizeInTiles.x;
        float _y = /*1.0f / */textureSizeInTiles.y;

        y = (textureSizeInTiles.y - 1) - y;

        x *= _x;
        y *= _y;

        Vector2 TilesSize = new Vector2(
            textureSizeInPixels.x / textureSizeInTiles.x,
            textureSizeInPixels.y / textureSizeInTiles.y
        );

        // Crie uma nova Sprite a partir da nova textura
        sprite = Sprite.Create(atlas, new Rect(x, y, TilesSize.x, TilesSize.y), Vector2.zero);
    }
}
