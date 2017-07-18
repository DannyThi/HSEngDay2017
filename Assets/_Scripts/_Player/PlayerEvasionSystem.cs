using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvasionSystem : MonoBehaviour {
	
	public float maneuverTime = 0.5f; // this will need to change dynamically during gameplay
	public float smoothing = 3;
	public float movementTilt = 20;
	public bool lerpToCenter = true;

	private Rigidbody rb;
	private Vector3 maneuverToTarget;
	private Vector3 currentRotation;
	private Vector3 rotateTo;

	public delegate void AileronRollNotification(float direction);
	public static event AileronRollNotification triggerAileronRoll;

	void Awake () {
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate() {
		if (lerpToCenter == true) {
			rb.position = Vector3.Lerp (rb.position, maneuverToTarget, smoothing * Time.deltaTime);
			currentRotation = Vector3.Lerp (currentRotation, rotateTo, smoothing * Time.deltaTime);
			rb.rotation = Quaternion.Euler (
				currentRotation.y * -movementTilt, 
				0, 
				currentRotation.x * -movementTilt
			);
		}
	}

	private IEnumerator Evade(string locationName, Vector3 direction) {
		maneuverToTarget = GameObject.Find (locationName).transform.position;
		rotateTo = direction;

		if (direction.x != 0) {
			if (triggerAileronRoll != null) {
				triggerAileronRoll (direction.x);
			}
		}

		yield return new WaitForSeconds (maneuverTime * 0.5f);
		rotateTo = Vector3.zero;
	}

	private Vector3 GetMoveVector(ArrowDirection direction) {
		Vector3 moveVector;
		switch(direction) {
		case ArrowDirection.Up:
			moveVector = new Vector3 (0, 1, 0);
			break;
		case ArrowDirection.Down:
			moveVector = new Vector3 (0, -1, 0);
			break;
		case ArrowDirection.Left:
			moveVector = new Vector3 (-1, 0, 0);
			break;
		case ArrowDirection.Right:
			moveVector = new Vector3 (1, 0, 0);
			break;
		default:
			moveVector = Vector3.zero;
			Debug.Log (name + ": ArrowDirection has not been set");
			break;
		}
		return moveVector;
	}

	private void TriggerEvasion(FloatingTextObject textObject) {
		string locationName = 
			"Loc (" + textObject.coordinates.x + ", " + textObject.coordinates.y + ")";
		StartCoroutine (Evade(locationName, GetMoveVector(textObject.arrowDirection)));
	}

	void OnEnable() {
		ArrowObject.arrowDespawn += TriggerEvasion;
	}

	void OnDisable() {
		ArrowObject.arrowDespawn -= TriggerEvasion;
	}
}
