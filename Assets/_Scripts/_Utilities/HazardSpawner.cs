using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSpawner : MonoBehaviour {

	public Boundary boundary;
	public float spawnDistance = 50;
	public bool playOnAwake = false;
	public GameObject[] hazards;
	public float delayStart;
	public int spawnCount = 1;
	public float spawnWaitTime = 1;
	public int waveCount = 1;
	public float waveWaitTime = 1;

	public float singleHazrdSpawnWait = 0.5f;

	void Start() {
		Mathf.Max (1, spawnCount);
		Mathf.Max (1, waveCount);

		if (playOnAwake) {
			StartCoroutine (SpawnHazards ());
		}
	}

	public IEnumerator SpawnHazards ()
	{
		yield return new WaitForSeconds (delayStart);
		for (int wave = 0; wave <= waveCount; wave++) {

			for (int spawn = 0; spawn <= spawnCount; spawn++ ) {
				SpawnHazard ();
				yield return new WaitForSeconds (spawnWaitTime);
			}
			yield return new WaitForSeconds (waveWaitTime);
		}
	}

	// this spawns the hazard even though it says spawn arrow???
	private void SpawnHazard() {
		int random = Random.Range (0, hazards.Length);

		Vector3 spawnPosition = new Vector3 (
			Random.Range(boundary.xMin,boundary.xMax),
			Random.Range(boundary.xMin,boundary.xMax),
			spawnDistance
		);
		Instantiate (hazards [random], spawnPosition, Quaternion.identity);
	}

	private void HandleTextSpawnCall(bool _, string word) {
		StartCoroutine (SpawnSingleHazard ());
	}

	IEnumerator SpawnSingleHazard() {
		Debug.Log("Spawned 1 hazard");
		yield return new WaitForSeconds (singleHazrdSpawnWait);
		int random = Random.Range (0, hazards.Length);
		Instantiate (hazards [random], new Vector3 (0, 0, spawnDistance), Quaternion.identity);
	}

	void OnEnable() {
		TextSpawnController.OnTextSpawned += HandleTextSpawnCall;
	}

	void OnDisable() {
		TextSpawnController.OnTextSpawned -= HandleTextSpawnCall;
	}
}
