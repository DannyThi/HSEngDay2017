using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextSpawnManager : MonoBehaviour {

	public GameObject wordContainer;

	public delegate void FloatingTextBroadcaster(FloatingTextObject textObject);
	public static event FloatingTextBroadcaster FloatingTextNotification;

	public List<FloatingTextObject> listOfText = new List<FloatingTextObject> { };

	private float currentTime = 0;
	public const float prewarmTime = 8.7f;	//The time it takes for the object to lerp to target.


	void Start() {
		listOfText.Sort();
	}

	void Update() {
		if (listOfText.Count != 0) {
			currentTime = Time.time;
			if (currentTime >= listOfText [0].spawnTime - prewarmTime) {
				BroadcastText (listOfText [0]);
			}
		}
	}

	private void BroadcastText(FloatingTextObject textObject) {
		GameObject container = Instantiate (wordContainer);
		container.GetComponent<FloatingTextContainer>().floatingTextObject = textObject;

		if (FloatingTextNotification != null) {
			FloatingTextNotification(textObject);
		} else {
			Debug.Log(name + ": No FloatingTextNotification.");
		}

		listOfText.Remove (textObject);
	}
}
