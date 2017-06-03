using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour {
	
	public float tilt;
	public float smoothing;
	public float maxManeuverDistance;
	public SimpleRange initialWaitTime;
	public SimpleRange maneuverTime;
	public SimpleRange nextManeuverWaitTime;
	public Boundry boundry;

	private float moveToPointX;
	private Rigidbody rb;
	private float currentZSpeed;


	void Start() {
		rb = GetComponent<Rigidbody> ();
		currentZSpeed = rb.velocity.z;
		StartCoroutine (Evade ());
	}

	void FixedUpdate() {
		float newManeuver = Mathf.MoveTowards (rb.velocity.x, moveToPointX, Time.deltaTime * smoothing);
		rb.velocity = new Vector3 (newManeuver, 0.0f, currentZSpeed);

		rb.position = new Vector3 (
			Mathf.Clamp(rb.position.x, boundry.minX, boundry.maxX),
			0.0f,
			rb.position.z//Mathf.Clamp ()
		);

		rb.rotation = rb.rotation = Quaternion.Euler (0, 0, rb.velocity.x * -tilt);
	}

	IEnumerator Evade () {
		// wait a while before moving.
		yield return new WaitForSeconds (Random.Range(initialWaitTime.min, initialWaitTime.max));
		while(true) {
			// Mathf.Sign() takes a value and returns the 'sign' of that value ('-' or '+').
			// Writing -Mathf.Sign() will flip the 'sign' ('-' becomes '+' and vice versa).
			// Eg. A -Mathf.Sign(-2) = '+2'. We do this because the coordinate system has '0' 
			// placed in the center of the world space. A positive value will move our object
			// to the right, and a negative value will move our object tothe left. This 
			// prevents the object from moving to the right if its already on the right, and
			// left if it's already on the left. Though, the object will be clamped to the
			// screen, this will prevent it from potentially sticking to one side.
			moveToPointX = Random.Range (1, maxManeuverDistance) * -Mathf.Sign (transform.position.x);
			yield return new WaitForSeconds (Random.Range (maneuverTime.min, maneuverTime.max));
			moveToPointX = 0;
			yield return new WaitForSeconds (
				Random.Range (
					nextManeuverWaitTime.min, 
					nextManeuverWaitTime.max
				)
			);
		} // loop
	}
}

[System.Serializable]
public struct SimpleRange {
	public float min;
	public float max;
}
