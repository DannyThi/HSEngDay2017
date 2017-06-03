using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLoader : MonoBehaviour {

	[Tooltip("The point (in seconds) in the music track to which you want the start of the game to sync.")]
	public float dropPoint = 44; 

	private float totalTimeTillGameStart;
	private float waitTime = 0;

	private float timer = 0;
	private new AudioSource audio;
	private GameController gameController;

	void Start () {
		audio = GetComponent<AudioSource> ();
		waitTime = SyncDropPoint ();
		StartCoroutine (PlayMusic());
	}

	IEnumerator PlayMusic() {
		Debug.Log ("(MusicLoader) Final delay time: "+ waitTime);
		yield return new WaitUntil (() => timer >= waitTime);
		audio.Play ();
	}

	private float SyncDropPoint() {
		// get game controller
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		if (gameController == null) {
			
		} else {
			Debug.Log ("Cannot find Game Controller");
		}

		Debug.Log("(MusicLoader) GameStartTime: " + totalTimeTillGameStart);
		float waitTime = totalTimeTillGameStart - dropPoint; // +6

		return Mathf.Max(0, waitTime);
	}

	void Update() {
		timer += Time.deltaTime;
	}

	public void SetWaitTime(float time) {
		totalTimeTillGameStart = time;
	}
}
