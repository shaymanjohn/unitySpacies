using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public GameObject playerObject;
    public GameObject playerBullet;

    public float minBound;
    public float maxBound;
    public float speed;

    private GameObject player;
    private GameObject bullet;
    private int playerStartDelay = 180;
    private bool firing = false;
    
    void Start() {
        player = Instantiate(playerObject, new Vector2(-8, -6), new Quaternion(0, 0, 0, 0));
        player.SetActive(false);

        bullet = Instantiate(playerBullet);
        bullet.SetActive(false);
    }

    void Update() {
        if (!firing) {
            bullet.transform.position = new Vector2(player.transform.position.x + 0.6f, player.transform.position.y + 0.6f);
            checkFire();
        } else {
            bullet.transform.position = new Vector2(bullet.transform.position.x, bullet.transform.position.y + 0.12f);
            if (bullet.transform.position.y > 6.5) {
                firing = false;
            }
        }        
    }

    void FixedUpdate() {
        if (playerStartDelay > 0) {
            playerStartDelay--;
            if (playerStartDelay == 0) {                
                player.SetActive(true);
                bullet.SetActive(true);
            }
            return;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 position = player.transform.position;
        
        if (position.x < minBound && horizontalInput < 0) {
            return;
        }
        
        if (position.x > maxBound && horizontalInput > 0) {
            return;
        }

        player.transform.position += Vector3.right * horizontalInput * speed;        
    }

    private void checkFire() {
        if (!firing && Input.GetKeyDown(KeyCode.Space)) {
            firing = true;
        }
    }
}
