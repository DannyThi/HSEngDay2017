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


	private void HandleFloatingTextSpawnNotification(FloatingTextObject textObject) {
		// check the objects arrow direction.
		// spawn and arrow.
		// the arrow has delegate to tell AI to move when it despawns.

	}

	void OnEnable() {
		FloatingTextSpawnManager.FloatingTextNotification += HandleFloatingTextSpawnNotification;
	}

	void OnDisable() {
		FloatingTextSpawnManager.FloatingTextNotification -= HandleFloatingTextSpawnNotification;
	}
}
