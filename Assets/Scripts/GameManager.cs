using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static string tagAlien = "Alien";
    public static float starSpeedMultiplier = 1.0f;
    public static Rect boundsRect = new Rect(-14.0f, -8.0f, 28.0f, 16.0f);

    void Start() {
    }

    void Update() {        
    }

    public static void log(string message) {
        Debug.Log(message);
    }
}
