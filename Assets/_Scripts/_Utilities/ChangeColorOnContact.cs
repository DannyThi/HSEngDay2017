using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorOnContact : MonoBehaviour {

	public Color32 idleColor = new Color32(199, 86, 196, 255); // magenta
	public Color32 onHitColor = new Color32 (42, 236, 107, 255); // green

	[Tooltip("Wait time till color returns to idle color")]
	public float waitTime = 1;

	private new Renderer renderer;

	void Awake() {
		renderer = gameObject.GetComponentInChildren<Renderer>();
		if (renderer == null) {
			Debug.Log("ChangeColorOnContact: Cannot find renderer.");
		}
	}

	void OnTriggerEnter (Collider other) {
		renderer.material.color = onHitColor;
		StartCoroutine (ReturnToIdleColor ());
	}

	IEnumerator ReturnToIdleColor() {
		yield return new WaitForSeconds (waitTime);
		renderer.material.color = idleColor;
	}
}
