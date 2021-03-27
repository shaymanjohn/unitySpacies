using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour {
    public GameObject basePrefab;

    void Start() {
        float basex = -7.0f;
        float basey = -4.0f;

        for (int ix = 0; ix < 4; ix++) {
            Instantiate(basePrefab, new Vector2(basex, basey), new Quaternion(0, 0, 0, 0));
            basex += 4.6f;
        }
    }
    
    void Update() {        
    }
}
