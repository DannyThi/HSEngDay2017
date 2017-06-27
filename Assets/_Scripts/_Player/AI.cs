
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

	public Boundary boundary;

	public bool playerControl = false;
	public float movementTilt = 3;
	public float maneuverTime = 2;
	public float dodgeDistance = 6;
	public float smoothing = 200;

	private Rigidbody rb;
	private Vector3 targetManeuver;
	private bool lerpToCenter = true;

	void Awake () {
		rb = GetComponent<Rigidbody> ();
	}

	void Start() {
		// get the maneuver time from the floating text time interval
		maneuverTime = (maneuverTime - 1f) / 2;
	}

	void HandleDodgeEvent(Vector3 direction) {
		StartCoroutine (Evade (direction));
	}

	public IEnumerator Evade(Vector3 direction) {
		lerpToCenter = false;
		targetManeuver = new Vector3 (direction.x * dodgeDistance, direction.y * (dodgeDistance * 0.8f) , direction.z);
		yield return new WaitForSeconds (maneuverTime);
		targetManeuver = new Vector3 (direction.x * -dodgeDistance, direction.y * (-dodgeDistance * 0.8f), direction.z);
		yield return new WaitForSeconds (maneuverTime);

		targetManeuver = Vector3.zero;
		lerpToCenter = true;

		//yield return new WaitForSeconds (maneuverTime / 2);
		//targetManeuver = new Vector3 (-direction.x * dodgeDistance, -direction.y * dodgeDistance, direction.z);

		//yield return new WaitForSeconds (maneuverTime / 2);
	}

	void FixedUpdate() {
		Debug.Log ("TargetManeuver: " + targetManeuver);

		Vector3 newManeuver = Vector3.MoveTowards (rb.velocity, targetManeuver, smoothing * Time.deltaTime);
		rb.velocity = newManeuver;
		rb.rotation = Quaternion.Euler (rb.velocity.y * -movementTilt, 0, rb.velocity.x * -movementTilt);

		if (lerpToCenter == true) {
			rb.position = Vector3.Lerp (rb.position, Vector3.zero, smoothing * Time.deltaTime);
			rb.rotation = Quaternion.Euler (rb.position.y * -movementTilt, 0, rb.position.x * -movementTilt);
		}

	}


	private Vector3 ClampToBoundary(Vector3 position) {
		Vector3 newPosition = new Vector3 (
			Mathf.Clamp (position.x, boundary.xMin, boundary.xMax),
			Mathf.Clamp (position.y, boundary.yMin, boundary.yMax),
			position.z
		);
		return newPosition;
	}

	void OnEnable() {
		Arrow.DodgeEventTriggerer += HandleDodgeEvent;
	}

	void OnDisable() {
		Arrow.DodgeEventTriggerer -= HandleDodgeEvent;
	}
}



