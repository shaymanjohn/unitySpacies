using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfieldController : MonoBehaviour {
    public GameObject starPrefab;

    private const int numberOfStarsPerLayer = 100;
    private const float slowestStarSpeed = 0.012f;
    private Star[] stars = new Star[numberOfStarsPerLayer * 3];

    void Start() {
        int index = 0;
        for (int ix = 0; ix < numberOfStarsPerLayer; ix++) {
            stars[index++] = new Star(starPrefab, new Color(0.5f, 0.3f, 0.5f, 1.0f), slowestStarSpeed * 1.0f, 0.8f);
            stars[index++] = new Star(starPrefab, new Color(0.3f, 0.5f, 0.5f, 1.0f), slowestStarSpeed * 2.0f, 1.0f);
            stars[index++] = new Star(starPrefab, new Color(0.7f, 0.7f, 0.7f, 1.0f), slowestStarSpeed * 3.0f, 1.2f);
        }
    }

    void Update() {
        foreach (Star star in stars) {
            star.move();
        }
    }
}

class Star {
    private GameObject star;
    private float starSpeed;

    public Star(GameObject starObject, Color colour, float speed, float scale) {
        star = GameObject.Instantiate(starObject, randomStarPosition, new Quaternion(0, 0, 0, 0));

        SpriteRenderer renderer = star.GetComponent<SpriteRenderer>();
        renderer.color = colour;

        Transform transform = star.GetComponent<Transform>();
        transform.localScale = new Vector2(transform.localScale.x * scale, transform.localScale.y * scale);

        starSpeed = speed;
    }

    public void move() {
        float updatedY = star.transform.position.y - (starSpeed * GameManager.starSpeedMultiplier);
        if (updatedY < GameManager.boundsRect.yMin) {
            updatedY += GameManager.boundsRect.height;
        }
        star.transform.position = new Vector2(star.transform.position.x, updatedY);
    }

    private Vector2 randomStarPosition {
        get {
            float x = Random.Range(GameManager.boundsRect.xMin, GameManager.boundsRect.xMax);
            float y = Random.Range(GameManager.boundsRect.yMin, GameManager.boundsRect.yMax);
            return new Vector2(x, y);
        }
    }
}