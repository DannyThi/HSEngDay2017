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

	private GameObject SpawnArrowFixed() {
		GameObject arrow = Instantiate (arrows [arrowsIndex]);
		arrowsIndex++;
		if (arrowsIndex >= arrows.Length) {
			arrowsIndex = 0;
		}
		return arrow;
	}

	void OnEnable() {
		TextSpawnController.OnTextSpawned += HandleOnTextSpawned;
	}

	void OnDisable() {
		TextSpawnController.OnTextSpawned -= HandleOnTextSpawned;
	}

	void Awake() {
		//arrows = new GameObject[0];
	}
}
