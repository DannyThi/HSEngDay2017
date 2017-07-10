using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLevel : MonoBehaviour {

	public float delayStart = 0;
	public MovieTexture movieTexture;
	public bool isEscapable = false;


	private AudioSource audioSource;

	void Awake() {
		audioSource = GetComponent<AudioSource> ();
	}

	void Start() {
		GameController.InitialGameEndNotification ();

		Debug.Log(name + " has been loaded.");
		audioSource.clip = movieTexture.audioClip;
		StartCoroutine (PlayMovie ());
	}

	private IEnumerator PlayMovie() {
		yield return new WaitForSeconds (delayStart);
		if (movieTexture != null) {
			movieTexture.Play ();
			audioSource.Play ();

			Screen.fullScreen = true;

		} else {
			Debug.Log ("No movie has been assigned to play.");
		}
	}


	void OnGUI() {
		if (movieTexture != null && movieTexture.isPlaying) {
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), movieTexture, ScaleMode.ScaleToFit);
		}
	}

	void Update() {
		if (isEscapable == true) {
			if (Input.GetKeyUp(KeyCode.Escape)) {
				Screen.fullScreen = false;
				movieTexture.Stop ();
				audioSource.Stop ();
			}
		}
	}
}
