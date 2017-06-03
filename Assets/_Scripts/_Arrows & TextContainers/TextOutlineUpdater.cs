using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextOutlineUpdater : MonoBehaviour {

	public float fadeTime = 1;

	private TextMeshPro outlineText;
	private List<string> wordList = new List<string> ();
	private bool firstWord = true;

	private TMP_Text m_TextComponent;
	//private float alpha = 0.0f;
	//private TMP_TextInfo textInfo;
	//private Color32 characterColor;


	void Awake() {

	}

	void Start() {
		outlineText = GetComponent<TextMeshPro> ();
		if (outlineText == null) {
			Debug.Log(name + ": Cannot find TextMeshPro.");
		}

		outlineText.overrideColorTags = false;
		//textInfo = wordContainer.textInfo;
		//characterColor = wordContainer.color;
	}

	// This will get the next word in the list and display it.
	private void UpdateTextOutline() {
		
		if (wordList.Count >= 1) {
			outlineText.SetText(wordList[1]);
			wordList.RemoveAt (0);
			//StartCoroutine (FadeInText ());
		} else {
			// This will remove the outline text for the last word.
			outlineText.SetText ("");
		}
	}

	// Called by the delegate when it is created.
	private void AddWordToList(bool isLastArrow, string word) {
		// "Add" will add the word to the END of the list.
		wordList.Add (word);

		// THIS IS THE CULPRIT that fucks up the game on build!!!
		if (firstWord == true) {
			outlineText.SetText (word);
			firstWord = false;
		}
	}

	// Called by the delegate when it is destroyed.
	private void RemoveWordFromList(string word) {
//		wordList.Remove (word);
//		UpdateTextOutline ();
//
		if (wordList.Count > 1) {
			Debug.Log (wordList.Count);
			outlineText.SetText(wordList[1]);
			wordList.RemoveAt(0);
			//wordList.Remove (word);
		} else {
			// This will remove the outline text for the last word.
			outlineText.SetText ("");
		}
	}


	void OnEnable() {
		TextSpawnController.OnTextSpawned += AddWordToList;
		WordContainer.OnContainerDespawn += RemoveWordFromList;
	}

	void onDisable() {
		TextSpawnController.OnTextSpawned -= AddWordToList;
		WordContainer.OnContainerDespawn -= RemoveWordFromList;
	}





	// Deprecated for now
//	private IEnumerator FadeInText() {
//		yield return new WaitForEndOfFrame ();
//
//		alpha = Mathf.Lerp (alpha, 255, Time.deltaTime / fadeTime);
//		characterColor = new Color32 ((byte)characterColor.r, (byte)characterColor.g, (byte)characterColor.g, (byte)alpha);
//
//		for (int i = 0; i < textInfo.characterCount; i++) {
//			int materialIndex = textInfo.characterInfo [i].materialReferenceIndex;
//			Color32[] newVertexColors = textInfo.meshInfo [materialIndex].colors32;
//			int vertexIndex = textInfo.characterInfo [i].vertexIndex;
//
//			newVertexColors [vertexIndex + 0] = characterColor;
//			newVertexColors [vertexIndex + 1] = characterColor;
//			newVertexColors [vertexIndex + 2] = characterColor;
//			newVertexColors [vertexIndex + 3] = characterColor;
//		}
//
//		wordContainer.UpdateVertexData (TMP_VertexDataUpdateFlags.Colors32);
//		yield return new WaitForEndOfFrame();
//	}


}
