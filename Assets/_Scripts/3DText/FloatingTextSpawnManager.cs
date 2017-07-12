using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextSpawnManager : MonoBehaviour {

	public delegate void FloatingTextBroadcaster(FloatingTextObject textObject);
	public static event FloatingTextBroadcaster FloatingTextNotification;

	public List<FloatingTextObject> listOfText = new List<FloatingTextObject> { };


	private float currentTime = 0;
	private const float prewarmTime = 8.7f;	//The time it takes for the object to lerp to target.


	void Start() {
		listOfText.Sort();
	}

	void Update() {
		if (listOfText.Count != 0) {
			currentTime = Time.time - prewarmTime;
			Mathf.Clamp (currentTime, 0, listOfText [0].spawnTime);
			if (currentTime >= listOfText [0].spawnTime) {
				BroadcastText (listOfText [0]);
			}
		}
	}

	private void BroadcastText(FloatingTextObject textObject) {
		if (FloatingTextNotification != null) {
			FloatingTextNotification(textObject);
			listOfText.Remove (textObject);
		}
	}
}
