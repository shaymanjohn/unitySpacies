using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StarfieldController : MonoBehaviour {
    public GameObject starObject;

    private const int numberOfStarsPerLayer = 150;
    private GameObject[] stars1 = new GameObject[numberOfStarsPerLayer];
    private GameObject[] stars2 = new GameObject[numberOfStarsPerLayer];
    private GameObject[] stars3 = new GameObject[numberOfStarsPerLayer];

    void Start() {
        for (int ix = 0; ix < numberOfStarsPerLayer; ix++) {
            Vector2 position1 = new Vector2(0, 0);
            Vector2 position2 = new Vector2(0, 0);
            Vector2 position3 = new Vector2(0, 0);

            GameObject star1 = Instantiate(starObject, position1, new Quaternion(0, 0, 0, 0));
            stars1[ix] = star1;

            GameObject star2 = Instantiate(starObject, position2, new Quaternion(0, 0, 0, 0));
            stars2[ix] = star2;

            GameObject star3 = Instantiate(starObject, position3, new Quaternion(0, 0, 0, 0));
            stars3[ix] = star3;
        }
    }

    void Update() {
    }
}
