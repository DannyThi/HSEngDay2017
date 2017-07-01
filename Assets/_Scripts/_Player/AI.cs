
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

	public Boundary boundary;

	public bool playerControl = false;
	public float movementTilt = 3;
	public float dodgeDistance = 6;
	public float smoothing = 200;

	[Range(0.0f, 1.0f)]
	public float barrelRollFrequency = 0.20f;

	private Rigidbody rb;
	private Vector3 targetManeuver;
	private bool lerpToCenter = true;
	private float maneuverTime = 2; 


	public delegate void BarrelRollNotification(bool rotation);
	public static event BarrelRollNotification onDodge;

	void Awake () {
		rb = GetComponent<Rigidbody> ();
	}

	void Start() {
		barrelRollFrequency = Mathf.Clamp01(barrelRollFrequency);
	}

	public void SetManeuverTime(float time) {
		maneuverTime = (time - 1f) / 2;
		Debug.Log ("ManeuverTime set to " + time);
	}

	void HandleDodgeEvent(Vector3 direction) {
		StartCoroutine (Evade (direction));
	}

	public IEnumerator Evade(Vector3 direction) {
		lerpToCenter = false;
		targetManeuver = new Vector3 (direction.x * dodgeDistance, direction.y * (dodgeDistance * 0.8f) , direction.z);

		if (targetManeuver.x != 0) {
			if (Random.Range (0.0f, 1.0f) <= barrelRollFrequency) {
				
				StartCoroutine (DoABarrelRoll (direction.x));
			}
		}

		yield return new WaitForSeconds (maneuverTime);
		targetManeuver = new Vector3 (direction.x * -dodgeDistance, direction.y * (-dodgeDistance * 0.8f), direction.z);
		yield return new WaitForSeconds (maneuverTime);

		targetManeuver = Vector3.zero;
		lerpToCenter = true;
	}
		
	void FixedUpdate() {
		Vector3 newManeuver = Vector3.MoveTowards (rb.velocity, targetManeuver, smoothing * Time.deltaTime);
		rb.velocity = newManeuver;
		rb.rotation = Quaternion.Euler (rb.velocity.y * -movementTilt, 0, rb.velocity.x * -movementTilt);

		if (lerpToCenter == true) {
			rb.position = Vector3.Lerp (rb.position, Vector3.zero, smoothing * Time.deltaTime);
			rb.rotation = Quaternion.Euler (rb.position.y * -movementTilt, 0, rb.position.x * -movementTilt);
		}
	}




	// ******* BARREL ROLL *******

	//private bool inBarrelRoll;
	private float barrelRollDuration {
		get {
			return maneuverTime / 2;
		}
	}

	IEnumerator DoABarrelRoll(float direction) {
		//inBarrelRoll = true;
		onDodge (true);
		float t = 0.0f;
		Vector3 initialRotation = transform.rotation.eulerAngles;
		Vector3 rotationGoal = initialRotation;

		rotationGoal.z += direction * -180f;

		Vector3 currentRotation = initialRotation;

		while (t < barrelRollDuration) {
			currentRotation.z = Mathf.Lerp (initialRotation.z, rotationGoal.z, t / barrelRollDuration);
			transform.rotation = Quaternion.Euler (currentRotation);
			t += Time.deltaTime;
			yield return null;
		}

		t = 0;

		initialRotation = transform.rotation.eulerAngles;
		rotationGoal = initialRotation;

		rotationGoal.z += direction * -180f;

		while (t < barrelRollDuration) {
			currentRotation.z = Mathf.Lerp (initialRotation.z, rotationGoal.z, t / barrelRollDuration);
			transform.rotation = Quaternion.Euler (currentRotation);
			t += Time.deltaTime;
			yield return null;
		}

		yield return new WaitForSeconds (maneuverTime);
		//inBarrelRoll = false;
		onDodge (false);
	}




	void OnEnable() {
		Arrow.DodgeEventTriggerer += HandleDodgeEvent;
	}

	void OnDisable() {
		Arrow.DodgeEventTriggerer -= HandleDodgeEvent;
	}

	//	private Vector3 ClampToBoundary(Vector3 position) {
	//		Vector3 newPosition = new Vector3 (
	//			Mathf.Clamp (position.x, boundary.xMin, boundary.xMax),
	//			Mathf.Clamp (position.y, boundary.yMin, boundary.yMax),
	//			position.z
	//		);
	//		return newPosition;
	//	}
}



