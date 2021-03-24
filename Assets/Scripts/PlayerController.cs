using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public GameObject playerPrefab;
    public GameObject bulletPrefab;
    public float speed;

    private GameObject player;
    private Bullet bullet;
    private int playerStartDelay = 180;
    
    void Start() {
        player = Instantiate(playerPrefab, new Vector2(GameManager.boundsRect.xMin + 4.0f, GameManager.boundsRect.yMin + 2.0f), new Quaternion(0, 0, 0, 0));
        player.SetActive(false);
        bullet = new Bullet(bulletPrefab);
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
        
        if (position.x < (GameManager.boundsRect.xMin + 4.0f) && horizontalInput < 0) {
            return;
        }
        
        if (position.x > (GameManager.boundsRect.xMax - 4.0f - 1.0f) && horizontalInput > 0) {
            return;
        }

        player.transform.position += Vector3.right * horizontalInput * speed;        
    }
}
