
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

	public Boundary boundary;

	public bool playerControl = false;
	public float movementTilt = 3;
	public float maneuverTime = 2;
	public float dodgeDistance = 30;
	public float smoothing = 10;

	private Rigidbody rb;
	private Vector3 targetManeuver;

	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
		

	void HandleDodgeEvent(Vector3 direction) {
		StartCoroutine (Evade (direction));
	}

	public IEnumerator Evade(Vector3 direction) {
		direction = new Vector3 (direction.x * dodgeDistance * 100, direction.y * dodgeDistance, direction.z);
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
		Arrow.DodgeEventTriggered += HandleDodgeEvent;
	}

	void OnDisable() {
		Arrow.DodgeEventTriggered -= HandleDodgeEvent;
	}
}



