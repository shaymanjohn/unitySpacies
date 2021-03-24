using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static string tagAlien = "Alien";
    public static float starSpeedMultiplier = 1.0f;

    void Start() {        
    }

    void Update() {        
    }

    public static void log(string message) {
        Debug.Log(message);
    }
}
