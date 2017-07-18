using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : MonoBehaviour {

	private string[][] moveLocations = new string[5][];
	private Vector2 currentLocation;

	// move data sent to arrowmanager
	private List<string> moveData = new List<string> {};
	private List<string> locationData = new List<string> { };
	private GameController gc;

	void Awake() {
		gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		moveLocations [0] = new string[] { "-2,2",  "-1,2",  "0,2",  "1,2",  "2,2"};
		moveLocations [1] = new string[] { "-2,1",  "-1,1",  "0,1",  "1,1",  "2,1"};
		moveLocations [2] = new string[] { "-2,0",  "-1,0",  "0,0",  "1,0",  "2,0"};
		moveLocations [3] = new string[] {"-2,-1", "-1,-1", "0,-1", "1,-1", "2,-1"};
		moveLocations [4] = new string[] {"-2,-2", "-1,-2", "0,-2", "1,-2", "2,-2"};
	}


	public delegate void LocationDataNotification(List<string> locationData);
	public static event LocationDataNotification LocationDataReady;
	public delegate void MoveDataNotification(List<string> moveData);
	public static event MoveDataNotification MoveDataReady;

	void Start() {
		moveData = new List<string> { };
		locationData = new List<string> { };

		currentLocation = gc.lastKnownLocation;
		Debug.Log ("Pulling last known location from GC: " + gc.lastKnownLocation);

		int spawnCount = GetComponent<TextSpawnController> ().GetSpawnLength ();
		CalculateMoveVectors (spawnCount);
	}

	// return an array of move data
	public void CalculateMoveVectors(int iteration) {
	//public List<string> CalculateMoveVectors(int iteration) {
		// get the number of text spawns
		for (int i = 0; i < iteration; i++) {
			CalculateMoveLocation ();
		}

		gc.lastKnownLocation = currentLocation;
		Debug.Log ("Giving Current location to GC: " + currentLocation);

		if (LocationDataReady != null) {
			LocationDataReady (locationData);
		}
		if (MoveDataReady != null) {
			MoveDataReady (moveData);
		}

	}

	private void CalculateMoveLocation() {
		Vector2 newMoveLocation;
		int randomVector = Random.Range (0, 2);
		//Debug.Log("Random = " + randomVector);

		if (randomVector == 0) {
			int random = GetMoveValue ((int)currentLocation.x);
			newMoveLocation = new Vector2 (random, currentLocation.y);
			//add Left_3d or Right_3d
			if (newMoveLocation.x - currentLocation.x == -1) {
				moveData.Add ("Left_3d");
			} else {
				moveData.Add ("Right_3d");
			}
		} else {
			int random = GetMoveValue ((int)currentLocation.y);
			newMoveLocation = new Vector2 (currentLocation.x, random);
			//add Down_3d or Up_3d
			if (newMoveLocation.y - currentLocation.y == -1) {
				moveData.Add ("Down_3d");
			} else {
				moveData.Add ("Up_3d");
			}
		}
		locationData.Add ("Loc (" + newMoveLocation.x + ", " + newMoveLocation.y + ")");
		//Debug.Log ("Loc (" + newMoveLocation.x + ", " + newMoveLocation.y + ")");
		//Debug.Log ("MoveData: " + moveData [moveData.Count - 1]);
		currentLocation = newMoveLocation;
	}

	private int GetMoveValue(int origin) {
		//Debug.Log ("origin: " + origin);
		int randomRange = (int)Mathf.Sign (Random.Range (-1, 1));
		//Debug.Log ("RandomRange: " + randomRange);
		int randomLocation = origin + randomRange;
		//Debug.Log ("RandomLocation: " + randomLocation);

		if (randomLocation < - 2) {
			//Debug.Log ("+2");
			randomLocation += 2;
		} else if (randomLocation > moveLocations.Length - 3) {
			//Debug.Log ("-2");
			randomLocation -= 2;
		}
		//Debug.Log ("NewRandomLocation: " + randomLocation);
		return randomLocation;
	}

	void OnDestroy() {

	}

}
