using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndController : MonoBehaviour {

	public float delayStart = 3.0f;
	public string sceneName;

	void Start() {
		
	}

	private IEnumerator InitiateEndSequence() {
		yield return new WaitForSeconds (delayStart);
		SceneManager.LoadScene (sceneName);



		yield return null;
	}


	void HandleEndSequenceCall() {
		Debug.Log(name + ": GameEnd notification received.");
		StartCoroutine (InitiateEndSequence ());
	}

	void OnEnable() {
		//LevelController.OnSceneEnded += HandleEndSequenceCall;
	}

	void OnDisable() {
		//LevelController.OnSceneEnded -= HandleEndSequenceCall;
	}
}
