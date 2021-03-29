using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollisionController : MonoBehaviour {

    private BaseController baseController;

    void Start() {
        GameObject baseManager = GameObject.Find("BaseManager");
        if (baseManager != null) {
            baseController = baseManager.GetComponent<BaseController>();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag.Contains(GameManager.tagBomb)) {
            baseController.hasBombHitBase(gameObject, other.gameObject);
        }
    }
}
