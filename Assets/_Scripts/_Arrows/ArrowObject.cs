using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowObject : MonoBehaviour {

	[Tooltip("Graphic to show the user the arrow was destroyed.")]
	public GameObject onContactFeedback;

	public FloatingTextObject textObject;

	void OnTriggerEnter() {
		Instantiate (onContactFeedback, transform.position, transform.rotation);
	}

	public delegate void ArrowDespawnNotification ();
	public static event ArrowDespawnNotification arrowDespawn;

	void OnDestroy() {
		if (arrowDespawn != null) {
			arrowDespawn();
		}
	}
}
