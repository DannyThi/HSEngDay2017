using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;
using UnityEngine;

public class SceneEndController : MonoBehaviour {

	// check for last arrow delegate call
	// reference camera for radial blur

	// FadeInOut to white
	// delegate call to tell level controller to wait.

	private GameObject targetCamera;

	void Awake() {
		targetCamera = GameObject.FindGameObjectWithTag ("FollowCam");
	}

	void Start() {
		// enable compnents
	}

	// maybe have this called from outside.
	private IEnumerator InitiateEndSequence() {
		while (true) {
			// lerp values here
			yield return new WaitForEndOfFrame();
		}
	}


	void HandleEndSequenceCall() {
		//StartCoroutine (InitiateEndSequence());
	}

	void OnEnable() {
		LevelController.OnSceneEnded += HandleEndSequenceCall;
	}

	void OnDisable() {
		LevelController.OnSceneEnded -= HandleEndSequenceCall;
	}
}
