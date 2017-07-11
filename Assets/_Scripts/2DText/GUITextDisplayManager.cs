using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUITextDisplayManager : MonoBehaviour {

	public List<GUITextObject> listOfText = new List<GUITextObject> {};

	private float currentTime = 0;

	public delegate void TextBroadcaster(GUITextObject textObject);
	public static event TextBroadcaster DisplayTextNotification;

	void Start() {
		listOfText.Sort();

	}

	void Update() {
		if (listOfText.Count != 0) {
			currentTime = Time.time;
			if (currentTime >= listOfText [0].spawnTime) {
				BroadcastText (listOfText [0]);
			}
		}
	}

	private void BroadcastText(GUITextObject textObject) {
		if (DisplayTextNotification != null) {
			DisplayTextNotification(textObject);
			listOfText.Remove (textObject);
		}
	}


	// get current time
	// if time is larger than the next in array.
	// trigger notification and remove that object.
}
