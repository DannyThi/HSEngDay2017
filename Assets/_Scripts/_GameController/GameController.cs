using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class Boundry {
	public float minX = -16, maxX = 16, minZ = -10, maxZ = 10;
}


public class GameController : MonoBehaviour {

	// A singleton of the GameController.
	public static GameController sharedGameController;

	[Tooltip("A list of scene names which will be called in sequence.")]
	public string[] scenes;
	private int sceneCounter = 0;

	private AudioSource audioSource;
	private TextMeshProUGUI pauseScreen;
	private LevelController level;

	public bool pauseOnSceneChange = false;
	private TextMeshProUGUI pressAnyKeyObject;

	void Awake() {
		if (sharedGameController == null) {
			sharedGameController = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}

		audioSource = GetComponent<AudioSource> ();
		pauseScreen = GameObject.FindGameObjectWithTag ("PauseScreen").GetComponent<TextMeshProUGUI> ();
		pressAnyKeyObject = GameObject.FindGameObjectWithTag ("PressAnyKeyText").GetComponent<TextMeshProUGUI> ();
	}

	void Start() {
		LoadNextScene ();
	}

	public void LoadNextScene() {

		if (scenes.Length > 0) {
			if (sceneCounter < scenes.Length) {

				StartCoroutine (PressKeyToStart ());

			} else {
				Debug.Log (name + ": All scenes have been loaded.");
			}
		} else {
			Debug.Log (name + ": No scenes to load.");
		}
	}

	private IEnumerator PressKeyToStart() {
		while (true) {
			if (pauseOnSceneChange == true) {

				pressAnyKeyObject.SetText("Press any key");

				if (Input.anyKey) {
					LoadScene ();
					yield break;
				}
			} else {
				LoadScene ();
				yield break;
			}
			yield return new WaitForEndOfFrame ();
		}
	}

	private void LoadScene() {
		SceneManager.LoadScene (scenes [sceneCounter], LoadSceneMode.Additive);
		pressAnyKeyObject.SetText("");
	}

	void OnEnable() {
		PauseFunction.OnGamePauseNotification += ManagePauseNotification;
		LevelController.OnSceneEnded += HandleSceneEnded;
	}

	void OnDisable() {
		PauseFunction.OnGamePauseNotification -= ManagePauseNotification;
		LevelController.OnSceneEnded -= HandleSceneEnded;
	}


	private void ManagePauseNotification(float timeScale) {
		if (timeScale == 0) {
			audioSource.Pause();
			pauseScreen.SetText ("Paused");
		} else {
			audioSource.Play();
			pauseScreen.SetText ("");
		}
	}


	private void HandleSceneEnded() {
		// Unload scene and increment the sceneCounter so its ready to be called by LoadNextScene.
		SceneManager.UnloadSceneAsync (scenes[sceneCounter]);
		sceneCounter++;

		LoadNextScene();
	}
}
