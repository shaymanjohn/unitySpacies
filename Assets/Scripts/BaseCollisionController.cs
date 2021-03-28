using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollisionController : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag.Contains(GameManager.tagBomb)) {
            GameManager.log("bomb hit base");
        }
    }
}
