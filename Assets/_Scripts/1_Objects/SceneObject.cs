using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneObject : TimedObject {

	public string sceneName;

	public SceneObject(float time, string text) : base(time) {
		sceneName = text;
	}

	public int CompareTo(GUITextObject other) {
		return base.CompareTo (other);
	}
}
