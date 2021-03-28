using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour {
    public Texture2D baseTexture;
    public SpriteRenderer base1Renderer;
    public SpriteRenderer base2Renderer;
    public SpriteRenderer base3Renderer;
    public SpriteRenderer base4Renderer;

    void Start() {
        createBase(base1Renderer);
        createBase(base2Renderer);
        createBase(base3Renderer);
        createBase(base4Renderer);
    }

    private void createBase(SpriteRenderer baseRenderer) {
        Texture2D thisBase = new Texture2D(baseTexture.width, baseTexture.height);
        thisBase.filterMode = FilterMode.Point;

        for (int ix = 0; ix < baseTexture.width; ix++) {
            for (int iy = 0; iy < baseTexture.height; iy++) {
                Color pixel = baseTexture.GetPixel(ix, iy);
                thisBase.SetPixel(ix, iy, pixel);
            }
        }

        thisBase.Apply();

        Rect rect = new Rect(0, 0, baseTexture.width, baseTexture.height);
        baseRenderer.sprite = Sprite.Create(thisBase, rect, Vector2.zero);
    }
    
    void Update() {        
    }
}
