using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseFunction : MonoBehaviour {

	public delegate void GamePaused (float timeScale);
	public static event GamePaused OnGamePauseNotification;

	public static bool isEnabled = true;
	private static float originalTimeScale = 1;

	private static bool isPaused {
		get {
			return _isPaused;
		}
		set {
			_isPaused = value;
			if (_isPaused == false) {
				Time.timeScale = originalTimeScale;
			} else {
				originalTimeScale = Time.timeScale;
				Time.timeScale = 0;
			}
		}
	}
	private static bool _isPaused = false;

	void Start() {
		originalTimeScale = Time.timeScale;
	}

	// Toggles game pause
	void Update () {
		if (isEnabled == true) {
			if (Input.GetKeyUp("p")) {
				isPaused = !isPaused;
				Debug.Log (name + ": isPaused => " + isPaused);

				if (OnGamePauseNotification != null) {
					OnGamePauseNotification (Time.timeScale);
				} else {
					Debug.Log (name + ": No OnGamePauseNotification");
				}
			} 

		}
	}
}
