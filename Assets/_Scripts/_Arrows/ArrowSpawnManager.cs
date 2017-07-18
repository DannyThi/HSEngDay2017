using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawnManager : MonoBehaviour {

	public GameObject[] arrows; // dictionary is better

	private List<FloatingTextObject> listOfTextObjects;
	private Vector2 lastPosition = new Vector2 (0, 0);

	private enum Axis { x, y };

	void Awake() {
		GameObject manager = GameObject.Find("FloatingTextSpawnManager");
		listOfTextObjects = manager.GetComponent<FloatingTextSpawnManager> ().listOfText;
	}

	void Start() {
		PlotCoordinates ();
	}
		
	private void PlotCoordinates() {
		for (int i = 0; i < listOfTextObjects.Count; i++) {
			if (listOfTextObjects[i].arrowDirection == ArrowDirection.NotSet) {
				lastPosition = AssignCoordinatesAndArrowDirection (listOfTextObjects[i], lastPosition);
			}
		}
	}

	private Vector2 AssignCoordinatesAndArrowDirection(FloatingTextObject textObject, Vector2 currentPosition) {
		Vector2 newMoveLocation;
		int randomDirectionValue = (int)Mathf.Sign (Random.Range (-1, 1)); // returns  -1 or 1

		// Choose a random axis
		if (Random.Range (0, 2) == 0) { 
			// x axis
			if (randomDirectionValue + currentPosition.x > 2 || randomDirectionValue + currentPosition.x < -2) {
				randomDirectionValue = -randomDirectionValue;
			}
			newMoveLocation = new Vector2 (randomDirectionValue + currentPosition.x, currentPosition.y);
			textObject.arrowDirection = AssignArrowDirection (randomDirectionValue, Axis.x);
		} else {
			// y axis
			if (randomDirectionValue + currentPosition.y > 2 || randomDirectionValue + currentPosition.y < -2) {
				randomDirectionValue = -randomDirectionValue;
			}
			newMoveLocation = new Vector2 (currentPosition.x, randomDirectionValue + currentPosition.y);
			textObject.arrowDirection = AssignArrowDirection (randomDirectionValue, Axis.y);
		}

		if (textObject.replaceTextWithDirection == true) {
			ReplaceTextWithDirection (textObject);
		}

		textObject.coordinates = newMoveLocation;
		return newMoveLocation;
	}

	private ArrowDirection AssignArrowDirection(int vector, Axis axis) {
		if (axis == Axis.x) {
			if (vector == -1) {
				return ArrowDirection.Left;
			} else {
				return ArrowDirection.Right;
			}
		} else if (axis == Axis.y) {
			if (vector == 1) {
				return ArrowDirection.Up;
			} else {
				return ArrowDirection.Down;
			}
		}
		return ArrowDirection.NotSet;
	}

	private void ReplaceTextWithDirection(FloatingTextObject textObject) {
		switch(textObject.arrowDirection) {
		case ArrowDirection.Up:
			textObject.textToDisplay = "Up";
			break;
		case ArrowDirection.Down:
			textObject.textToDisplay = "Down";
			break;
		case ArrowDirection.Left:
			textObject.textToDisplay = "Left";
			break;
		case ArrowDirection.Right:
			textObject.textToDisplay = "Right";
			break;
		default:
			Debug.Log(
				name + 
				": SpawnTime " + 
				textObject.spawnTime + 
				"replaceTextWithDirection was checked but no ArrowDirection was assigned."
			);
			break;
		}
	}


	private void SpawnArrow(FloatingTextObject textObject) {
		GameObject arrow;

		switch (textObject.arrowDirection) {
		case ArrowDirection.Up:
			arrow = Instantiate (arrows [0]);
			break;
		case ArrowDirection.Down:
			arrow = Instantiate (arrows [1]);
			break;
		case ArrowDirection.Left:
			arrow = Instantiate (arrows [2]);
			break;
		case ArrowDirection.Right:
			arrow = Instantiate (arrows [3]);
			break;
		default:
			arrow = Instantiate (arrows [Random.Range (0, 4)]);
			Debug.Log (name + ": Arrow direction has not been set. Please check SpawnArrow(FloatingTextObject) function.");
			break;
		}

		arrow.GetComponent<ArrowObject>().textObject = textObject;
	}

	private void HandleFloatingTextSpawnNotification(FloatingTextObject textObject) {
		SpawnArrow(textObject);
	}

	void OnEnable() {
		FloatingTextSpawnManager.floatingTextNotification += HandleFloatingTextSpawnNotification;
	}

	void OnDisable() {
		FloatingTextSpawnManager.floatingTextNotification -= HandleFloatingTextSpawnNotification;
	}
}
