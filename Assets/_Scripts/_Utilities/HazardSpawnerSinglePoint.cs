using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSpawnerSinglePoint : MonoBehaviour {

	public GameObject[] hazards;
	public float spawnDelay = 0.5f;

	private void HandleTextSpawnCall(bool _, string word) {
		StartCoroutine (SpawnSingleHazard ());
	}

	IEnumerator SpawnSingleHazard() {
		Debug.Log("Spawned 1 hazard");
		yield return new WaitForSeconds (spawnDelay);
		int random = Random.Range (0, hazards.Length);
		Instantiate (hazards [random], new Vector3 (0, 0, transform.position.z), Quaternion.identity);
	}

	void OnEnable() {
		TextSpawnController.OnTextSpawned += HandleTextSpawnCall;
	}

	void OnDisable() {
		TextSpawnController.OnTextSpawned -= HandleTextSpawnCall;
	}
}
