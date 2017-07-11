using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public class GUITextObject : TimedObject, IComparable<GUITextObject> {

	public string textToSpawn;

	public GUITextObject(float time, string text) : base(time) {
		textToSpawn = text;
	}

	public int CompareTo(GUITextObject other) {
		return base.CompareTo (other);
	}
}
