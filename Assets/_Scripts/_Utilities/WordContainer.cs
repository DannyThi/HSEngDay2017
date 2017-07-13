using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordContainer : MonoBehaviour {

	public delegate void WordContainerDespawn(string word);
	public static event WordContainerDespawn OnContainerDespawn;

	private TextMeshPro tmPro;

	void Start () {
		tmPro = GetComponentInChildren<TextMeshPro> ();
		//tmPro.text = floatingTextObject.textToDisplay;
	}

	void OnDestroy() {
		if (OnContainerDespawn != null) {
			OnContainerDespawn (tmPro.text);
		} else {
			Debug.Log(name + ": No OnContainerDespawn delegate.");
		}
	}
}
