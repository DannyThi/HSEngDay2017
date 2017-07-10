using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

	public float delayStart = 10;

	private AudioSource music;

	void Awake() {
		music = GetComponent<AudioSource> ();
		if (music == null) {
			Debug.Log (name + " : no AudioSource on gameobject.");
		}
	}

	void Start() {
		StartCoroutine (StartAudio());
	}

	IEnumerator StartAudio() {
		yield return new WaitForSecondsRealtime (delayStart);
		music.Play ();
	}
}
