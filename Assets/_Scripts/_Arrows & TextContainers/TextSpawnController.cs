using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextSpawnController : MonoBehaviour {

	[Header("Text")]
	[Tooltip("This file will be read line by line and displayed by the controller.")]
	public TextAsset textFile;
	[Tooltip("If this is checked, the controller will read the text in sequence. If unchecked, it will be read at random.")]
	public bool useLinearWordProgression = false;
	[Tooltip("Instances of this object will be spawned by the controller and float towards the player.")]
	public GameObject wordContainerObject;

	[Header("Spawn Variables")]
	[Tooltip("Delay the start of the text spawns (seconds).")]
	public float delayStart = 0;
	[Tooltip("Time between each spawn (seconds).")]
	public float spawnInterval = 2;

	private List<string> textToDisplay = new List<string>(); // Use list for adding and removing.
	private int textCounter = 0;

	public delegate void ArrowHandler (bool isLastArrow, string word);
	public static event ArrowHandler OnTextSpawned;

	void Awake() {
		ConvertTextToList ();
	}

	public IEnumerator StartTextSpawn() {
		yield return new WaitForSeconds (delayStart);
		while (true) {
			DisplayText ();
			if (textToDisplay.Count == 0) {
				break;
			}
			yield return new WaitForSeconds (spawnInterval);
		}
		yield return new WaitUntil( () => lastArrowDestoyed == true);
		Debug.Log (name + ": Last arrow destroyed.");
	}


	// This will spawn wordContainers with a word pulled from the List. 
	// It will then remove that word from the list so it isn't repeated.
	private void DisplayText() {
		if (textToDisplay.Count > 0) {
			if (useLinearWordProgression == false) {
				textCounter = Random.Range (0, textToDisplay.Count);
			}
			string word = textToDisplay [textCounter];
			textToDisplay.RemoveAt (textCounter);

			GameObject wordObject = Instantiate (wordContainerObject);
			wordObject.GetComponentInChildren<TextMeshPro> ().SetText (word);

			// This broadcasts a message letting listeners know that a word has been displayed.
			// The bool value of true lets listeners know that this is the last word to spawn.
			if (OnTextSpawned != null) {
				if (textToDisplay.Count != 0) {
					OnTextSpawned (false, word);
				} else {
					OnTextSpawned (true, word);
				}
			} else {
				Debug.Log (name + ": No OnTextSpawned.");
			}
		}
	}
		


	// This will convert the textfile into a string array, and then 
	// convert that to a List so we can add/remove from it.
	private void ConvertTextToList() {
		string[] rawText = Utilities.ConvertText (textFile);
		if (rawText != null) {
			for (int i = 0; i < rawText.Length; i++) {
				textToDisplay.Add (rawText [i]);
			}
		} else {
			Debug.Log("TextDisplayManager: No text to load.");
		}
	}

	// Register and deregister arrow listeners.
	void OnEnable() {
		Arrow.OnLastArrowDestroyed += HandleLastArrowDestroyed;
	}

	void OnDisable() {
		Arrow.OnLastArrowDestroyed -= HandleLastArrowDestroyed;
	}

	private bool lastArrowDestoyed = false;
	private void HandleLastArrowDestroyed() {
		lastArrowDestoyed = true;
	}
}
