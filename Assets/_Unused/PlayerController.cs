using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
//public class Boundry {
//	public float minX, maxX, minZ, maxZ;
//}

public class PlayerController : MonoBehaviour {

	// Public variables
	//[Tooltip("-1 = Inverted. 1 = Normal")]
	public bool invertYAxis = false;

	public float thrust = 10;
	public float movementSpeed = 5;
	public float tilt = 3;

	public GameObject bolt;
	public Transform shotSpawn;
	public float fireRate;

	// Private variables
	private Rigidbody rb;
	private int yAxisOrientation;

	private float nextFire;
	private float rotationSpeed = 50;


	void Start () {
		rb = GetComponent<Rigidbody> ();

		if (invertYAxis) {
			yAxisOrientation = -1;
		} else {
			yAxisOrientation = 1;
		}

	}

	void Update () {
		if (Input.GetButton("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate(bolt, transform.position + shotSpawn.position, shotSpawn.rotation);
		}
	}

	private float yaw = 0.0f;
	private float pitch = 0.0f;

	void FixedUpdate () {
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (horizontal, 0.0f, vertical);
		rb.velocity = movement * thrust;

//		float zMovement = Input.GetAxis ("Vertical");
//		float yMovement = Input.GetAxis("Mouse Y");
//		float xMovement = Input.GetAxis ("Mouse X");

		// Horizontal should barrel roll
		//float horizontalMovement = Input.GetAxis ("Horizontal");

		//Vector3 movement = new Vector3 (xMovement, yMovement, zMovement);
		//rb.velocity = movement * speed;
		//rb.velocity = new Vector3 (0.0f, 0.0f, zMovement * speed);
		//rb.position = Vector3.MoveTowards (rb.position, mousePosition, speed);

		//rb.velocity = new Vector3 (xMovement * movementSpeed, yMovement * movementSpeed, zMovement * thrust);

		pitch -= Input.GetAxis ("Mouse Y") * Time.deltaTime * rotationSpeed;
		yaw += Input.GetAxis ("Mouse X") * Time.deltaTime * rotationSpeed;
		rb.rotation = Quaternion.Euler (pitch, yaw * yAxisOrientation, rb.velocity.x * -tilt);

		// some sort of clamp for pitch and yaw
	}
		
}
