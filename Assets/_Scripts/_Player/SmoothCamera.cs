using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour {

	public GameObject cameraTarget;
	public Vector3 cameraOffset = new Vector3 (0.0f, 1.0f, -4.0f);
	public float dampening = 5.0f;

	//public float xRotationOffset = 4.0f;
	//public bool smoothFollow = true;

	private Transform target;

	void Awake() {
		target = cameraTarget.transform;
	}
		
	void FixedUpdate() {
		if (cameraTarget) {
			Vector3 toPosition = target.position + (target.rotation * cameraOffset);
			Vector3 currentPosition = Vector3.Lerp (transform.position, toPosition, dampening * GetDeltaTime());
			transform.position = currentPosition;

			Quaternion offSettedRotation = new Quaternion (
				transform.rotation.x,
				transform.rotation.y,
				transform.rotation.z,
				transform.rotation.w
			);
				
			Quaternion toRotation = Quaternion.LookRotation (target.position - transform.position, target.up);
			Quaternion currentRotation = Quaternion.Slerp (offSettedRotation, toRotation, dampening * GetDeltaTime());

			if (disableCameraRotation == false) {
				transform.rotation = currentRotation;
			}
		}	
	}

	private float GetDeltaTime() {
		return Time.smoothDeltaTime;
	}

	private bool disableCameraRotation = false;
	private void DisableCameraRotation(bool rotation) {
		disableCameraRotation = rotation;
	}

	private void HandleGameEndNotification() {
		Debug.Log (name + ": GameEnd notification received.");
	}

	void OnEnable() {
		AI.onDodge += DisableCameraRotation;
		//GameController.OnGameEnd += HandleGameEndNotification;
	}
	void OnDisable() {
		AI.onDodge -= DisableCameraRotation;
		//GameController.OnGameEnd -= HandleGameEndNotification;
	}
}
