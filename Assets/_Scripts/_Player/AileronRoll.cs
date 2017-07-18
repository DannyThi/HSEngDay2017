using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AileronRoll : MonoBehaviour {
	
	[Range(0.0f, 1.0f)]
	public float barrelRollFrequency = 0.20f;
	public float rollTime = 1; // roll time needs to be set dynamically

	private GameObject playerModel;

	void Awake() {
		playerModel = GameObject.Find ("PlayerModel");
	}

	private IEnumerator DoABarrelRoll(float direction) {
		iTween.RotateBy (playerModel, iTween.Hash ("z", -direction, "time", rollTime));
		yield return new WaitForSeconds (rollTime);
	}

	private void HandleAileronRollNotification(float direction) {
		if (Random.Range (0.0f, 1.0f) <= barrelRollFrequency) {
			StartCoroutine (DoABarrelRoll (direction));
		}
	}

	void OnEnable() {
		PlayerEvasionSystem.triggerAileronRoll += HandleAileronRollNotification;
	}

	void OnDisable() {
		PlayerEvasionSystem.triggerAileronRoll -= HandleAileronRollNotification;

	}
}
