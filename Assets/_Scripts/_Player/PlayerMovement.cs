using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	
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

//
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class PlayerAI : MonoBehaviour {
//
//	public Boundary boundary;
//
//	public bool playerControl = false;
//	public float tilt = 3;
//	public float maneuverTime = 1;
//	public float dodgeDistance = 3;
//	public float smoothing = 10;
//
//	private Rigidbody rb;
//	private Vector3 targetManeuver;
//
//	void Start () {
//		rb = GetComponent<Rigidbody> ();
//	}
//
//	public IEnumerator Evade(Vector3 direction) {
//		targetManeuver = direction;
//		yield return new WaitForSeconds (maneuverTime);
//		targetManeuver = Vector3.zero;
//	}
//
//	void FixedUpdate() {
//		Vector3 newManeuver = Vector3.MoveTowards (rb.velocity, targetManeuver, smoothing * Time.deltaTime);
//		rb.velocity = newManeuver;
//		rb.position = ClampToBoundary (rb.position);
//
//		//		GetComponent<Rigidbody>().position = new Vector3
//		//			(
//		//				Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
//		//				0.0f, 
//		//				Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
//		//			);
//		//
//		//		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0, 0, GetComponent<Rigidbody>().velocity.x * -tilt);
//	}
//
//
//	private Vector3 ClampToBoundary(Vector3 position) {
//		Vector3 newPosition = new Vector3 (
//			Mathf.Clamp (position.x, boundary.xMin, boundary.xMax),
//			Mathf.Clamp (position.y, boundary.yMin, boundary.yMax),
//			position.z
//		);
//		return newPosition;
//	}
//
//
//	//	public Done_Boundary boundary;
//	//	public float tilt;
//	//	public float dodge;
//	//	public float smoothing;
//	//	public Vector2 startWait;
//	//	public Vector2 maneuverTime;
//	//	public Vector2 maneuverWait;
//	//
//	//	private float currentSpeed;
//	//	private float targetManeuver;
//	//
//	//	void Start ()
//	//	{
//	//		currentSpeed = GetComponent<Rigidbody>().velocity.z;
//	//		StartCoroutine(Evade());
//	//	}
//	//
//	//	IEnumerator Evade ()
//	//	{
//	//		yield return new WaitForSeconds (Random.Range (startWait.x, startWait.y));
//	//		while (true)
//	//		{
//	//			yield return new WaitForSeconds (Random.Range (maneuverTime.x, maneuverTime.y));
//	//			targetManeuver = 0;
//	//			yield return new WaitForSeconds (Random.Range (maneuverWait.x, maneuverWait.y));
//	//		}
//	//	}
//	//
//	//	void FixedUpdate ()
//	//	{
//	//		float newManeuver = Mathf.MoveTowards (GetComponent<Rigidbody>().velocity.x, targetManeuver, smoothing * Time.deltaTime);
//	//		GetComponent<Rigidbody>().velocity = new Vector3 (newManeuver, 0.0f, currentSpeed);
//	//		GetComponent<Rigidbody>().position = new Vector3
//	//			(
//	//				Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
//	//				0.0f, 
//	//				Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
//	//			);
//	//
//	//		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0, 0, GetComponent<Rigidbody>().velocity.x * -tilt);
//	//	}
//}
//
//[System.Serializable]
//public class Boundary 
//{
//	public float xMin, xMax, yMin, yMax;
//}

