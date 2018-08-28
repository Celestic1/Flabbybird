using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public GameObject target;
    public Vector2 offset;

    private Rigidbody2D cameraRigidbody;
	// Use this for initialization
	void Start () {
        cameraRigidbody = GetComponent <Rigidbody2D> ();

        transform.position = new Vector3(
            target.transform.position.x + offset.x,
            target.transform.position.y + offset.y,
            transform.position.z
        );
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (target != null)
        {
            cameraRigidbody.velocity = new Vector2(target.GetComponent<Rigidbody2D>().velocity.x, 0);
        } else
        {
            cameraRigidbody.velocity = Vector2.zero;
        }

	}
}
