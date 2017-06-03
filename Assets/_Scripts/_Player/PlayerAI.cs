using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour {
	
	public Boundary boundary;

	public float forwardMovement = 10;
	public float horizontalMovement;
	public float verticalMovement;
	public float movementTilt = 3;
	public float movementMultiplier = 6;

	public bool playerControl = false;

	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	void FixedUpdate () {
		if (playerControl) {
			Vector3 movement = new Vector3 (
				Input.GetAxis ("Horizontal") * movementMultiplier,
				Input.GetAxis ("Vertical") * movementMultiplier,
				forwardMovement
			);
			rb.velocity = movement;
			rb.position = ClampToBoundary (rb.position);

		} else {
			Vector3 movement = new Vector3 (horizontalMovement, verticalMovement, forwardMovement);
			rb.velocity = movement;
			rb.position = ClampToBoundary (rb.position);
		}

		rb.rotation = Quaternion.Euler (rb.velocity.y * -movementTilt, 0, rb.velocity.x * -movementTilt);

	}

	private Vector3 ClampToBoundary(Vector3 position) {
		Vector3 newPosition = new Vector3 (
			                      Mathf.Clamp (position.x, boundary.xMin, boundary.xMax),
			                      Mathf.Clamp (position.y, boundary.yMin, boundary.yMax),
			                      position.z
		                      );
		return newPosition;
	}
}

[System.Serializable]
public class Boundary 
{
	public float xMin, xMax, yMin, yMax;
}
