using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public GameObject playerObject;
    public GameObject bulletObject;

    public float minBound;
    public float maxBound;
    public float speed;

    private GameObject player;
    private Bullet bullet;
    private int playerStartDelay = 180;
    
    void Start() {
        player = Instantiate(playerObject, new Vector2(-8, -6), new Quaternion(0, 0, 0, 0));
        player.SetActive(false);
        bullet = new Bullet(bulletObject);
    }

    void Update() {
        if (player.activeSelf) {
            bullet.updatePosition(new Vector2(player.transform.position.x + 0.6f, player.transform.position.y + 0.6f));
        }
    }

    void FixedUpdate() {
        if (playerStartDelay > 0) {
            playerStartDelay--;
            if (playerStartDelay == 0) {                
                player.SetActive(true);
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
}
