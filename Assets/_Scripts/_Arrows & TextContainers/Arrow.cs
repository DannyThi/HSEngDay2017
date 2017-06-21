using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	[Tooltip("Graphic to show the user the arrow was destroyed.")]
	public GameObject onContactFeedback;

	// Feedback to show the 3d arrow hit the flat arrow
	void OnTriggerEnter() {
		Instantiate (onContactFeedback, transform.position, transform.rotation);
	}

	private bool isLastArrow = false;
	public void setLastArrow() {
		isLastArrow = true;
	}

	public delegate void DestroyHandler();
	public static event DestroyHandler OnLastArrowDestroyed;

	public delegate void DodgeEvent(Vector3 direction);
	public static event DodgeEvent DodgeEventTriggered;

	void OnDestroy() {
		// Trigger a dodge event
		if (DodgeEventTriggered != null) {
			//Debug.Log ("Arrow Direction: " + gameObject.tag);

			DodgeEventTriggered (GetDodgeDirection ());
		} else {
			Debug.Log(name + ": No DodgeEventTriger.");
		}

		// Broadcast message to tell listeners that the last arrow was destroyed.
		if (isLastArrow == true) {
			if (OnLastArrowDestroyed != null) {
				OnLastArrowDestroyed ();
			}
		}
	}

	private Vector3 GetDodgeDirection() {
		switch (gameObject.tag) {
		case ("Up_3d"):
			return new Vector3 (0, 1, 0);
		case ("Down_3d"):
			return new Vector3 (0, -1, 0);
		case ("Left_3d"):
			return new Vector3 (-1, 0, 0);
		case ("Right_3d"):
			return new Vector3 (1, 0, 0);
		default:
			return Vector3.zero;
		}
	}

	// Deprecated
	// calculate the time for customTimings.
	// currently 8.68 seconds.
	private float intervalLength = 0;
	void OnEnable() {
		intervalLength = Time.time;
	}

	void OnDisable() {
		intervalLength = Time.time - intervalLength;
		//Debug.Log ("Length = " + intervalLength);
	}
}
