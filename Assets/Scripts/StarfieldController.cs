using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfieldController : MonoBehaviour {
    public GameObject starObject;

    private const int numberOfStarsPerLayer = 100;
    private const float slowestStarSpeed = 0.012f;
    private Star[] stars = new Star[numberOfStarsPerLayer * 3];

    void Start() {
        int index = 0;
        for (int ix = 0; ix < numberOfStarsPerLayer; ix++) {
            stars[index++] = new Star(starObject, new Color(0.5f, 0.3f, 0.5f, 1.0f), slowestStarSpeed * 1.0f, 0.8f);
            stars[index++] = new Star(starObject, new Color(0.3f, 0.5f, 0.5f, 1.0f), slowestStarSpeed * 2.0f, 1.0f);
            stars[index++] = new Star(starObject, new Color(0.7f, 0.7f, 0.7f, 1.0f), slowestStarSpeed * 3.0f, 1.2f);
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

    private const float minX = -14.0f;
    private const float maxX = 14.0f;
    private const float minY = -10.0f;
    private const float maxY = 7.0f;

    public Star(GameObject starObject, Color colour, float speed, float scale) {
        star = GameObject.Instantiate(starObject, randomPosition(), new Quaternion(0, 0, 0, 0));

        SpriteRenderer renderer = star.GetComponent<SpriteRenderer>();
        renderer.color = colour;

        Transform transform = star.GetComponent<Transform>();
        transform.localScale = new Vector2(transform.localScale.x * scale, transform.localScale.y * scale);

        starSpeed = speed;
    }

    public void move() {
        float updatedY = star.transform.position.y - starSpeed;
        if (updatedY < minY) {
            updatedY += -minY + maxY;
        }
        star.transform.position = new Vector2(star.transform.position.x, updatedY);
    }

    private Vector2 randomPosition() {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        return new Vector2(x, y);
    }
}