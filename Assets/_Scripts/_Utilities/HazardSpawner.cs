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
			Random.Range(boundary.yMin,boundary.yMax),
			spawnDistance
		);
		Instantiate (hazards [random], spawnPosition, Quaternion.identity);
	}
}
