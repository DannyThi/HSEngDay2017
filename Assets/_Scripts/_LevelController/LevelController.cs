using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using TMPro;
using UnityEngine.SceneManagement;


public class LevelController : MonoBehaviour {

	[Header("Optional Text")]
	[Tooltip("Optional intro text before the game starts.")]
	public TextAsset intro;
	[Tooltip("Optional outtro text before the game ends.")]
	public TextAsset outtro;
	[Tooltip("Countdown before the game begins.")]
	public bool useCountdown = false;
	[Tooltip("Countdown (whole seconds) before the game begins.")]
	public int countdownLength = 3;

	[Header("Timing")]
	[Tooltip("Pause the game before the start of the level.")]
	public bool pauseOnStart = false;
	[Tooltip("Delay the start of the text spawns (seconds).")]
	public float delayStart = 0;
	[Tooltip("Delay the end of this class (seconds).")]
	public float delayEnd = 0;
	public float optionalTextDisplayInterval = 1;


	// Text arrays from the textfiles
	private string[] introText;
	private string[] outtroText;


	// GameObjects
	private TextMeshProUGUI textContainer2D;
	private TextSpawnController spawnController;
	private ScreenOverlay overlay;


	// Delegates
	public delegate void SceneEnded();
	public static event SceneEnded OnSceneEnded;


	void Awake() {
		textContainer2D = GameObject.FindGameObjectWithTag ("TextContainer").GetComponent<TextMeshProUGUI> ();
		if (textContainer2D == null) Debug.Log (name + ": No textContainer2D");

		spawnController = GetComponent<TextSpawnController> ();
		//if (spawnController == null) Debug.Log(name + ": No TextSpawnController");

		overlay = GameObject.Find ("FadeOverlay").GetComponent<ScreenOverlay> ();
		if (overlay == null) Debug.Log (name + ": No Screen overlay.");
		
		// check if there is a text file to convert.
		if (intro != null) introText = Utilities.ConvertText (intro);
		if (outtro != null) outtroText = Utilities.ConvertText (outtro);
	}


	void Start() {
		//GameController gc = Utilities.GetGameController ();
		//gc.pauseOnSceneChange = pauseOnStart;


		StartCoroutine (BeginLevel ());
	}


	IEnumerator BeginLevel() {
		// ***********************************
		// Need to Fix Fade in here? Or have it a part of the game controller?

		yield return new WaitForSeconds (delayStart);
		if (introText != null) 		yield return StartCoroutine (DisplayOptionalText (introText));
		if (useCountdown == true)	yield return StartCoroutine (DisplayCountdown ());
		//yield return new WaitForSeconds (textDisplayInterval);
		textContainer2D.SetText ("");

		// ***********************************
		// Game Starts Here
		// ***********************************

		if (spawnController != null) {
			yield return StartCoroutine (spawnController.StartTextSpawn ());
		}

		if (outtroText != null) yield return StartCoroutine (DisplayOptionalText (outtroText));
		textContainer2D.SetText ("");
		yield return new WaitForSeconds (delayEnd);


		// ***********************************
		// Add coroutine to fadeout/radial blur/boost
		// use seperate script
		// ***********************************

		// exit delegate
		// check if there is a script for ending.



		if (OnSceneEnded != null) OnSceneEnded (); else Debug.Log(name + ": No OnSceneLoaded delegate.");
	}


	// This handles the display of any optional text before and/or after the main game
	IEnumerator DisplayOptionalText(string[] text) {
		foreach (string word in text) {
			textContainer2D.SetText (word);
			yield return new WaitForSeconds (optionalTextDisplayInterval);
		}
	}


	// This will add a countdown timer after introText and before the game starts
	IEnumerator DisplayCountdown() {
		for (int i = 0; i < countdownLength; i++) {
			textContainer2D.SetText ((countdownLength - i).ToString());
			yield return new WaitForSeconds (1);
		}
		textContainer2D.SetText ("");
	}
}


