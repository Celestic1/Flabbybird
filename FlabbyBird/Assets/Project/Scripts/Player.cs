using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public delegate void ScoreHandler();
    public delegate void KillHandler();
    public event ScoreHandler OnScore;
    public event KillHandler OnKill;

    public float jumpingSpeed = 5f;
    public float movementSpeed = 5f;
    public float verticalLimit;
    public float gravityScale;
    private bool isMoving;

    private Rigidbody2D playerRigidbody;
	// Use this for initialization
	void Start () {
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        playerRigidbody.gravityScale = 0;
	}

    // Update is called once per frame
    void Update() {

        //keep within bounds
        if (transform.position.y > verticalLimit)
        {
            transform.position = new Vector3(transform.position.x, verticalLimit, transform.position.z);
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
        }
        else if (transform.position.y < -verticalLimit)
        {
            Kill();
        }

        // vertical movement
        if (Input.GetAxis("Fire1") == 1f) {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpingSpeed);
            
            if(isMoving == false)
            {
                isMoving = true;
                StartMoving();
            }
        }
    }

void StartMoving()
    {
        playerRigidbody.velocity = new Vector2(movementSpeed, playerRigidbody.velocity.y);
        playerRigidbody.gravityScale = gravityScale;
    }


void OnTriggerEnter2D (Collider2D otherCollider)
    {
        if (otherCollider.gameObject.tag == "Obstacle")
        {
            Kill();
        } else if (otherCollider.gameObject.tag == "ScoreArea")
        {
            Destroy(otherCollider.gameObject);
            if(OnScore != null)
            {
                OnScore();
            }
        }
    }
    void Kill()
    {
        Destroy(gameObject);
        if (OnKill != null)
        {
            OnKill();
        }
    }
}
