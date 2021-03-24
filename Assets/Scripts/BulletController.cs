using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == GameManager.tagAlien) {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}

class Bullet {
    public bool firing;

    private GameObject bulletPrefab;
    private GameObject bullet;    
    private float bulletSpeed = 0.15f;

    public Bullet(GameObject bulletObject) {
        bulletPrefab = bulletObject;
        firing = false;
    }

    public void updatePosition(Vector2 position) {
        if (bullet == null) {
            bullet = GameObject.Instantiate(bulletPrefab);
            firing = false;
        }
        
        if (!firing) {
            bullet.transform.position = position;
            checkFire();
        } else {
            bullet.transform.position = new Vector2(bullet.transform.position.x, bullet.transform.position.y + bulletSpeed);
            if (bullet.transform.position.y > 6.5) {
                firing = false;
            }
        }                
    }

    private void checkFire() {
        if (bullet.activeSelf && Input.GetKey(KeyCode.Space)) {
            firing = true;
        }
    }
}