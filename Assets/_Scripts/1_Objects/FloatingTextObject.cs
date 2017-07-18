using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ArrowDirection {NotSet, Up, Down, Left, Right};

[System.Serializable]
public class FloatingTextObject : TimedObject, IComparable<FloatingTextObject> {

	public string textToDisplay;
	public bool replaceTextWithDirection = false;
	public ArrowDirection arrowDirection; // if nothing, set random
	public Vector2 coordinates; // this is set by the manager

	// constructors
	public FloatingTextObject(float time, string text, ArrowDirection direction) : base(time) {
		textToDisplay = text;
		arrowDirection = direction;
	}

	public FloatingTextObject(float time, string text) : base(time) {
		textToDisplay = text;
	}

	public int CompareTo(FloatingTextObject other) {
		return base.CompareTo (other);
	}
}
