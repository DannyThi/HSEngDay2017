using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingTextPrompter : MonoBehaviour {

	[Range(0.0f, 1.0f)]
	public float textPromptPopUpDelay = 0.50f;

	private List<FloatingTextObject> listOfTextPrompts = new List<FloatingTextObject> ();
	private TextMeshPro textPrompt;

	void OnEnable() {
		FloatingTextSpawnManager.FloatingTextNotification += AddWordToList;
		FloatingTextContainer.OnContainerDespawn += RemoveWordFromList;
	}

	void onDisable() {
		FloatingTextSpawnManager.FloatingTextNotification += AddWordToList;
		FloatingTextContainer.OnContainerDespawn -= RemoveWordFromList;
	}

	void Awake() {
		textPrompt = GetComponentInChildren<TextMeshPro> ();
		if (textPrompt == null) {
			Debug.Log(name + ": Cannot find TextMeshPro.");
		}
	}

	void Start() {
		textPromptPopUpDelay = Mathf.Clamp01 (textPromptPopUpDelay);
	}

	void Update() {
		UpdateTextPrompter (Time.time);
	}

	private void UpdateTextPrompter(float currentTime) {
		if (listOfTextPrompts.Count > 0) {
			if (currentTime >= listOfTextPrompts[0].spawnTime - 
				(FloatingTextSpawnManager.prewarmTime * textPromptPopUpDelay)) {
				textPrompt.SetText(listOfTextPrompts[0].textToDisplay);
			}
		}
	}

	private void AddWordToList(FloatingTextObject floatingText) {
		listOfTextPrompts.Add (floatingText);
	}

	private void RemoveWordFromList(FloatingTextObject floatingText) {
		if ( listOfTextPrompts.Count > 1) {
			listOfTextPrompts.RemoveAt(0);
		}
	}
}
