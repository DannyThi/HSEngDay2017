using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingTextContainer : MonoBehaviour {

	public FloatingTextObject floatingTextObject;

	public delegate void WordContainerDespawn(FloatingTextObject floatingTextObject);
	public static event WordContainerDespawn OnContainerDespawn;

	private TextMeshPro tmPro;

	void Start () {
		tmPro = GetComponentInChildren<TextMeshPro> ();
		tmPro.text = floatingTextObject.textToDisplay;
	}

	void OnDestroy() {
		if (OnContainerDespawn != null) {
			OnContainerDespawn (floatingTextObject);
		} else {
			Debug.Log(name + ": No OnContainerDespawn delegate.");
		}
	}
}
