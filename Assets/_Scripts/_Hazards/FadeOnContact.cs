using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOnContact : MonoBehaviour {

	private Collider c;
	void Awake() {
		c = GetComponent<Collider> ();
	}

	void OnTriggerEnter(Collider other) {
		AutoTransparency transparency = other.GetComponent<AutoTransparency> ();
		if (transparency != null) {
			Debug.Log ("Fading");
			transparency.FadeToMinAlpha ();
		}
	}

	private void HandleEventNotification(string eventName) {
		if (eventName == "StartGlamCam") {
			c.enabled = false;
		} else if (eventName == "StopGlamCam") {
			c.enabled = true;
		}
	}

	void OnEnable() {
		NotificationManager.eventNotification += HandleEventNotification;
	}

	void OnDisable() {
		NotificationManager.eventNotification -= HandleEventNotification;
	}
}
