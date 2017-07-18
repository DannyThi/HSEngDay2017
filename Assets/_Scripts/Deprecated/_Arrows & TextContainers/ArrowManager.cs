using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour {

	[Tooltip("Arrows that will spawn will be called from this pool.")]
	public GameObject[] arrows;
	[Tooltip("Randomize the spawn of the arrows. If unchecked, " +
		"the arrows will spawn in the order listed in the array. " +
		"This can be good for practice mode, for example.")]
	public bool randomizeArrowSpawn = true;

	// movement data from level controller
	private List<string> moveData;

	// Counter for looping through the arrows.
	private int arrowsIndex = 0;

	private void HandleOnTextSpawned(bool isLastArrow, string word) {
		GameObject gmObj;
		if (randomizeArrowSpawn == true) {
			gmObj = SpawnArrowRandom ();
		} else {
			gmObj = SpawnArrowFixed ();
		}

		if (isLastArrow) {
			Arrow arrow = gmObj.GetComponent<Arrow> ();
			if (arrow != null) {
				arrow.setLastArrow ();
			}
		}
	}

	private GameObject SpawnArrowRandom() {
		arrowsIndex = Random.Range (0, arrows.Length);
		return Instantiate (arrows [arrowsIndex]);
	}

	//public delegate void ArrowSpawnNotification(Vector3 moveData);
	//public static event ArrowSpawnNotification OnArrowSpawn;

	private GameObject SpawnArrowFixed() {
		// use move data to spawn arrows
		if (moveData != null) {
			GameObject arrow = Instantiate(FindArrow (moveData[0]));
			moveData.RemoveAt (0);
			return arrow;
		} else {
			Debug.Log (name + " : no move data");
			return null;
		}


/*		GameObject arrow = Instantiate (arrows [arrowsIndex]);
		arrowsIndex++;
		if (arrowsIndex >= arrows.Length) {
			arrowsIndex = 0;
		}*/
	}

	private GameObject FindArrow(string arrowDirection) {
		for (int i = 0; i < arrows.Length; i++) {
			if (arrows[i].name.Contains(arrowDirection)) {
				return arrows[i];
			}
		}
		Debug.Log (name + " : No arrows contain word -> " + arrowDirection);
		return null;
	}

	void HandleMoveDataReady(List<string> data) {
		moveData = data;
		Debug.Log ("Collected MoveData");
	}

	void OnEnable() {
		TextSpawnController.OnTextSpawned += HandleOnTextSpawned;
		LocationManager.MoveDataReady += HandleMoveDataReady;
	}

	void OnDisable() {
		TextSpawnController.OnTextSpawned -= HandleOnTextSpawned;
		LocationManager.MoveDataReady -= HandleMoveDataReady;
	}
}
