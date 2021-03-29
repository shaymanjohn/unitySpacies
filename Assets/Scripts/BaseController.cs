using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour {
    public GameObject basePrefab;

    private Base[] bases = new Base[4];

    void Start() {
        bases[0] = createBase(new Vector2(-4, -4), Color.white);
        bases[1] = createBase(new Vector2(-1, -4), Color.blue);
        bases[2] = createBase(new Vector2( 2, -4), Color.red);
        bases[3] = createBase(new Vector2( 5, -4), Color.magenta);
    }

    private Base createBase(Vector2 position, Color colour) {
        GameObject baseObj = Instantiate(basePrefab, position, new Quaternion(0, 0, 0, 0));

        SpriteRenderer renderer = baseObj.GetComponent<SpriteRenderer>();
        Texture2D baseTexture = renderer.sprite.texture;

        Texture2D thisBaseTexture = new Texture2D(baseTexture.width, baseTexture.height);
        thisBaseTexture.filterMode = FilterMode.Point;

        bool[,] collisionGrid = new bool[baseTexture.width, baseTexture.height];

        for (int ix = 0; ix < baseTexture.width; ix++) {
            for (int iy = 0; iy < baseTexture.height; iy++) {
                Color pixel = baseTexture.GetPixel(ix, iy);
                if (pixel.a > 0) {
                    thisBaseTexture.SetPixel(ix, iy, colour);
                    collisionGrid[ix, iy] = true;
                } else {
                    thisBaseTexture.SetPixel(ix, iy, pixel);
                    collisionGrid[ix, iy] = false;
                }
            }
        }
        thisBaseTexture.Apply();

        Rect rect = new Rect(0, 0, baseTexture.width, baseTexture.height);
        renderer.sprite = Sprite.Create(thisBaseTexture, rect, Vector2.zero);

        Base thisBase = new Base();
        thisBase.gameObject = baseObj;
        thisBase.solid = collisionGrid;
        return thisBase;
    }

    public void hasBombHitBase(GameObject baseObject, GameObject bomb) {
        for (int ix = 0; ix < bases.Length; ix++) {
            if (bases[ix].gameObject == baseObject) {
                GameManager.log("Bomb hit base: " + ix);
                //
                // Convert bomb position into base coordinates.
                // Check if any of that area is solid.
                // If yes, kill bomb and remove pixels, update solid array.
                //
                return;
            }
        }
    }
}

class Base {
    public GameObject gameObject;
    public bool[,] solid;
}
