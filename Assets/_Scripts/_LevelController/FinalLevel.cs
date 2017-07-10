using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLevel : MonoBehaviour {

	public float delayStartForTransition = 0;

	public MovieTexture movieTexture;
	public bool isEscapable = false;


	private GameObject movieQuad;
	private AudioSource audioSource;


	public delegate void TransitionNotification();
	public static event TransitionNotification OnTransitionTrigger;

	void Awake() {
		movieQuad = GameObject.FindGameObjectWithTag ("MovieQuad");
		audioSource = GetComponent<AudioSource> ();
		movieQuad.GetComponent<Renderer>().material.mainTexture = movieTexture;
	}

	void Start() {
		Debug.Log(name + " has been loaded.");
		GameController.InitialGameEndNotification ();
		audioSource.clip = movieTexture.audioClip;

		// change aspect fit
		//movieQuad.GetComponent<Renderer>().material.SetTextureScale

		StartCoroutine (PlayMovie ());
	}

	private IEnumerator PlayMovie() {
		if (OnTransitionTrigger != null) {
			OnTransitionTrigger ();
		} else {
			Debug.Log(name + ": no OnTransitionTrigger.");
		}

		yield return new WaitForSeconds (delayStartForTransition);
	
		// glitch camera notification


		// enable movieQuad renderer
		movieQuad.GetComponent<MeshRenderer> ().enabled = true;

		if (movieTexture != null) {
			movieTexture.Play ();
			audioSource.Play ();

			Screen.fullScreen = true;

		} else {
			Debug.Log ("No movie has been assigned to play.");
		}
	}

//
//	void OnGUI() {
//		if (movieTexture != null && movieTexture.isPlaying) {
//			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), movieTexture, ScaleMode.ScaleToFit);
//		}
//	}

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
