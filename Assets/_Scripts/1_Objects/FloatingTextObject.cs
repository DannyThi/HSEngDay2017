using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextObject : TimedObject, IComparable<GUITextObject> {

	public string textToDisplay;

	public FloatingTextObject(float time, string textToDisplay) : base(time) {
		
	}

	public int CompareTo(GUITextObject other) {
		return base.CompareTo (other);
	}
}
