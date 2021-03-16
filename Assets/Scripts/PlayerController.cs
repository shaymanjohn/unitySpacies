using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform player;
    public float minBound;
    public float maxBound;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (player.position.x < minBound && horizontalInput < 0) {
            return;
        } else if (player.position.x > maxBound && horizontalInput > 0) {
            return;
        }

        player.position += Vector3.right * horizontalInput * speed;
    }
}
