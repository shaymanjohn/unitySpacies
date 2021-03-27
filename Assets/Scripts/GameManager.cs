using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static string tagAlien = "Alien";
    public static string tagAlienBottom = "AlienBottom";
    public static string tagAlienMiddle = "AlienMiddle";
    public static string tagAlienTop = "AlienTop";
    public static string tagBomb = "Bomb";

    public static float starSpeedMultiplier = 1.0f;
    public static Rect boundsRect = new Rect(-14.0f, -8.0f, 28.0f, 16.0f);

    private static int _score;
    public static int score {
        get {
            return _score;
        }
        set {
            _score = value;
            log("score: " + _score);
        }
    }

    void Start() {
        score = 0;
    }

    void Update() {        
    }

    public static void log(string message) {
        Debug.Log(message);
    }
}
