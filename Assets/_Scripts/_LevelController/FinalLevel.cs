using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLevel : MonoBehaviour {

	public float delayStartForTransition = 2;

	public MovieTexture movieTexture;
	public bool isEscapable = false;

	private GameObject movieQuad;
	private GameObject backgroundQuad;
	private AudioSource audioSource;

	public delegate void TransitionNotification();
	public static event TransitionNotification OnTransitionTrigger;

	void Awake() {
		movieQuad = GameObject.FindGameObjectWithTag ("MovieQuad");
		backgroundQuad = GameObject.FindGameObjectWithTag ("BackgroundQuad");
		audioSource = GetComponent<AudioSource> ();
	}

	void Start() {
		Debug.Log(name + " has been loaded.");
		GameController.InitialGameEndNotification ();
		audioSource.clip = movieTexture.audioClip;
		movieQuad.GetComponent<Renderer>().material.mainTexture = movieTexture;

		StartCoroutine (PlayMovie ());
	}

	private IEnumerator PlayMovie() {
		if (OnTransitionTrigger != null) {
			OnTransitionTrigger ();
		} else {
			Debug.Log(name + ": no OnTransitionTrigger.");
		}

		yield return new WaitForSeconds (delayStartForTransition);
		movieQuad.GetComponent<MeshRenderer> ().enabled = true;
		backgroundQuad.GetComponent<MeshRenderer> ().enabled = true;

		if (movieTexture != null) {
			movieTexture.Play ();
			audioSource.Play ();
			Screen.fullScreen = true;

		} else {
			Debug.Log ("No movie has been assigned to play.");
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
