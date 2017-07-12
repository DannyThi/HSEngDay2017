using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloatingTextObject : TimedObject, IComparable<FloatingTextObject> {

	public string textToDisplay;
	public string arrowDirection; // if nothing, set random
	public Vector2 coordinates; // this is set by the manager


	// constructors
	public FloatingTextObject(float time, string text, string direction) : base(time) {
		textToDisplay = text;
		arrowDirection = direction;
	}

	public FloatingTextObject(float time, string text) : base(time) {
		textToDisplay = text;
	}

	public int CompareTo(FloatingTextObject other) {
		return base.CompareTo (other);
	}


	// find out exactly how the text and arrows are spawned.
	// find out what happens to the text outline updater.
	// the floating text spawn object should handle all the calls.
}
