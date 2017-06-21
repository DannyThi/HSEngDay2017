
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

	public Boundary boundary;

	public bool playerControl = false;
	public float movementTilt = 3;
	public float maneuverTime = 1;
	public float dodgeDistance = 3;
	public float smoothing = 10;

	private Rigidbody rb;
	private Vector3 targetManeuver;

	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
		
	// This needs to be in anotehr file
	public delegate void DodgeEvent(Vector3 direction);
	public static event DodgeEvent DodgeEventTriggered;

	void HandleDodgeEvent(Vector3 direction) {
		StartCoroutine (Evade (direction));
	}

	public IEnumerator Evade(Vector3 direction) {
		targetManeuver = direction;
		yield return new WaitForSeconds (maneuverTime);
		targetManeuver = Vector3.zero;
	}

	void FixedUpdate() {
		Vector3 newManeuver = Vector3.MoveTowards (rb.velocity, targetManeuver, smoothing * Time.deltaTime);
		rb.velocity = newManeuver;
		rb.position = ClampToBoundary (rb.position);
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

	void OnEnable() {
		DodgeEventTriggered += HandleDodgeEvent;
	}

	void OnDisable() {
		DodgeEventTriggered -= HandleDodgeEvent;
	}
}



