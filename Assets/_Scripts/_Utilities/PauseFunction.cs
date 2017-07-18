using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseFunction : MonoBehaviour {

	public delegate void PauseNotification(float timeScale);
	public static event PauseNotification onGamePauseNotification;

	private static float timeScale = 1;
	private AudioSource audioSource;

	void Awake() {
		audioSource = GetComponent<AudioSource> ();
	}
		
	void Start() {
		timeScale = Time.timeScale;
	}

	void Update () {
		if (Input.GetKeyUp ("p")) {
			if (Time.timeScale == 0) {			// Play
				Time.timeScale = timeScale;
				audioSource.Play ();
				if (onGamePauseNotification != null) {
					onGamePauseNotification (timeScale);
				}
			} else {						// Pause
				timeScale = Time.timeScale;
				Time.timeScale = 0;
				audioSource.Pause ();
				if (onGamePauseNotification != null) {
					onGamePauseNotification (0);
				}
			}


		}
	}
}
