using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour {

	// Public
	public float forwardThrust = 5.0f;
	public bool invertYAxis = false;
	public float movementSpeed = 1.0f;
	public float rotationSpeed = 50.0f;

	[Tooltip("Ship tilt when the ship moves left/right.")]
	public float movementTilt = 3;

	// Private
	private Rigidbody rb;
	private int yAxisOrientation;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		if (invertYAxis) { yAxisOrientation = -1; } else { yAxisOrientation = 1; }
	}
	
	// Update is called once per frame
	void Update () {
//		float horizontal = Input.GetAxis ("Horizontal");
//		float vertical  = Input.GetAxis ("Vertical");
//
//		Vector3 movement2D = new Vector3 (horizontal, vertical * yAxisOrientation, 0.0f);
//		transform.position += movement2D * movementSpeed * Time.deltaTime;
//		transform.rotation = Quaternion.RotateTowards (
//			transform.rotation, 
//			Quaternion.LookRotation (movement2D),
//			rotationSpeed * Mathf.Deg2Rad
//		);


	}

	void FixedUpdate() {
		PlayerMovement();
	}

	private void PlayerMovement () {
		float horizontalMovement = Input.GetAxis ("Horizontal") * movementSpeed;
		float verticalMovement  = Input.GetAxis ("Vertical") * movementSpeed;
		Vector3 movement = new Vector3 (
			horizontalMovement,
			verticalMovement * yAxisOrientation,
			forwardThrust
		);
			
		rb.velocity = movement;

		//Rotation and tilt
//		rb.rotation = Quaternion.RotateTowards (
//			rb.rotation, 
//			Quaternion.LookRotation (new Vector3 (horizontalMovement, verticalMovement * yAxisOrientation, 0)),
//			rotationSpeed * Mathf.Deg2Rad
//		);
//
		rb.rotation = Quaternion.Euler (
			horizontalMovement, 
			verticalMovement * yAxisOrientation, 
			rb.velocity.x * -movementTilt
		);
	}

}
