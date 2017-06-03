using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFeedback : MonoBehaviour {

	public GameObject onContactFeedback;
	public Vector3 positionAdjustment;

	void Start() {
		if (onContactFeedback == null) {
			Debug.Log("TriggerFeedback: No gameobject to give instantiate feedback");
		}
	}
		
	void OnTriggerEnter() {
		Instantiate (
			onContactFeedback,
			new Vector3 (
				transform.position.x + positionAdjustment.x,
				transform.position.y + positionAdjustment.y,
				transform.position.z + positionAdjustment.z
			), 
			transform.rotation
		);
	}
}
