using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public class GUITextObject : IComparable<GUITextObject> {

	public float spawnTime;
	public string textToSpawn;

	public GUITextObject(float time, string text) {
		spawnTime = time;
		textToSpawn = text;
	}

	public int CompareTo(GUITextObject other) {
		if (other == null) return 1;

		float difference = spawnTime - other.spawnTime;
		if (difference == 0.0f) {
			return 0;
		} else {
			return (int)Mathf.Sign (difference);
		}
	}
}
