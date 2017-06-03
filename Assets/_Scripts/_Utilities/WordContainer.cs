using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordContainer : MonoBehaviour {

	private TextMeshPro container;

	void Start () {
		container = GetComponentInChildren<TextMeshPro> ();
	}
	
	public delegate void WordContainerDespawn(string word);
	public static event WordContainerDespawn OnContainerDespawn;

	void OnDisable() {
		if (OnContainerDespawn != null) {
			OnContainerDespawn (container.text);
		} else {
			Debug.Log(name + ": No OnContainerDespawn delegate.");
		}
	}
}
