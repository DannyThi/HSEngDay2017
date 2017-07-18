using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUICenterTextDisplayer : MonoBehaviour {
	
	public float textDisplayTime = 2;

	private TextMeshProUGUI textContainer;
	private static IEnumerator coroutine;

	void Awake() {
		textContainer = GetComponent<TextMeshProUGUI> ();
	}

	private IEnumerator UpdateText(string text) {
		textContainer.text = text;
		yield return new WaitForSeconds(textDisplayTime);
		textContainer.text = "";
	}

	private void HandleDisplayTextNotification(GUITextObject textObject) {
/*		if (coroutine != null) {
			StopCoroutine (coroutine);
		}*/

		// This fixes the linefeed not parsing.
		string tempText = textObject.textToSpawn.Replace("\\n", "\n");
		coroutine = UpdateText (tempText);
		StartCoroutine (coroutine);

//		coroutine = UpdateText (textObject.textToSpawn);
//		StartCoroutine (coroutine);
	}

	void OnEnable() {
		GUITextDisplayManager.DisplayTextNotification += HandleDisplayTextNotification;
	}

	void OnDisable() {
		GUITextDisplayManager.DisplayTextNotification -= HandleDisplayTextNotification;
	}
}
