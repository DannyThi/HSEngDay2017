using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {


	public float delayStart = 10;

	private float time = 0;

	private AudioSource audioSource;

	void Awake() {
		audioSource = GetComponent<AudioSource> ();
	}

	void Start() {
		time = Time.time;
	}

	void Update() {
		time = Time.time - time;
		if (time >= delayStart) {
			audioSource.Play ();
		}
	}
}
