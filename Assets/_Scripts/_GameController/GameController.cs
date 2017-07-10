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

	[Header("Scenes")]
	[Tooltip("A list of scene names which will be called in sequence.")]
	public string[] scenes;
	public float[] sceneStartTimes;
	public float[] delays;


	public float arrowTravelTime = 8.78f;
	public Vector2 lastKnownLocation = new Vector2 (0, 0);
	public float currentTime = 0;


	private int sceneCounter = 0;
//	private int sceneStartTimeCounter = 0;
//	private int delaysCounter = 0;
//

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
		Debug.Log ("GameController.LastKnownLocation: " + lastKnownLocation);

		//LoadScenes ();
		LoadNextScene ();
	}

	void Update() {
		currentTime = Time.time;
	}

	private void LoadScenes() {
		
	}

	public void LoadNextScene() {
		if (scenes.Length > 0) {
			if (sceneCounter < scenes.Length) {
				StartCoroutine (PressKeyToStart ());
			} else {
				Debug.Log (name + ": All scenes have been loaded.");
				// kill hazards
				if (OnGameEnd != null) {
					OnGameEnd ();
				}
			}
		} else {
			Debug.Log (name + ": No scenes to load.");
		}
	}



	private void LoadScene() {
		SceneManager.LoadScene (scenes [sceneCounter], LoadSceneMode.Additive);
		pressAnyKeyObject.SetText("");
	}

	private IEnumerator PressKeyToStart() {
		while (true) {
			if (pauseOnSceneChange == true) {
				pressAnyKeyObject.SetText("Press Enter key to begin");
				if (Input.GetKey("return")) {
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

	public delegate void GameEndNotification();
	public static event GameEndNotification OnGameEnd;

	public static void InitialGameEndNotification() {
		if (OnGameEnd != null) {
			OnGameEnd ();
		}
	}
}
