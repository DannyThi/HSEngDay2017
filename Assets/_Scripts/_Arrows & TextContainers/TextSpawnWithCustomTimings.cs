using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public class SpawnTiming {
	public float timing;
	public bool isPause = false;

	public SpawnTiming(float timing, bool isPause) {
		this.timing = timing;
		this.isPause = isPause;
	}
}

public class TextSpawnWithCustomTimings : MonoBehaviour {
	
	public TextAsset floatingText;
	public TextAsset spawnTimings;

	private readonly static float rampupTime = 8.68f; 	// The time it takes between an arrow spawn and despawn
	private AudioSource audio;
	private float currentTime;

	private SpawnTiming[] timings;
	private int loopIndex;


	void Awake() {
		audio = GameObject.FindGameObjectWithTag ("GameController").GetComponent<AudioSource> ();
	}

	void Start() {
		timings = Utilities.ConvertCVS (spawnTimings);
		StartCoroutine (BeginSpawn ());
	}
		

	IEnumerator BeginSpawn() {
		audio.Play ();
		currentTime = Time.time;
		while (true) {
			currentTime += Time.time - currentTime;
			Debug.Log (currentTime);

			if (timings.Length > 0) {
				Debug.Log("gnrjkbsls");
				if (timings [loopIndex].timing - rampupTime >= currentTime) {
					if (timings [loopIndex].isPause == false) {

						// spawn text




					}

					loopIndex++;
					if (loopIndex >= timings.Length) {
						break;
					}
					yield return new WaitForFixedUpdate ();
				}
			} else {
				Debug.Log("Timings array too short.");
			}

		}

	}
		

}
