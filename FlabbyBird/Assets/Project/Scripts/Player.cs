using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float jumpingSpeed = 5f;
    public float movementSpeed = 5f;
    private Rigidbody2D playerRigidbody;
	// Use this for initialization
	void Start () {
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        // vertical movement
		if (Input.GetAxis("Fire1") == 1f){
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpingSpeed);
        }
        // horizontal movement
        playerRigidbody.velocity = new Vector2(movementSpeed, playerRigidbody.velocity.y);
	}

    void OnTriggerEnter2D (Collider2D otherCollider)
    {
        if (otherCollider.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }
}
