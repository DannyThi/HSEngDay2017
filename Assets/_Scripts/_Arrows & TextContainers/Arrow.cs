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
	void OnDestroy() {
		// Broadcast message to tell listeners that the last arrow was destroyed.
		if (isLastArrow == true) {
			if (OnLastArrowDestroyed != null) {
				OnLastArrowDestroyed ();
			}
		}
	}

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
