using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSpawnRandom : MonoBehaviour {

	public bool playOnAwake = true;
	public GameObject[] hazards;
	public float delayStart = 0;
	public float minSpawnInterval = 0;
	public float maxSpawnInterval = 1;

	private Bounds bounds;

	void Start() {
		minSpawnInterval = Mathf.Max (0, minSpawnInterval);
		maxSpawnInterval = Mathf.Max (0.1f, maxSpawnInterval);
		bounds = GetComponent<Renderer> ().bounds;

		if (playOnAwake) {
			StartCoroutine (SpawnHazards ());
		}
	}

	public IEnumerator SpawnHazards ()
	{
		yield return new WaitForSeconds (delayStart);
		while (true) {
			SpawnHazard ();
			float randomSpawnTime = Random.Range (maxSpawnInterval, maxSpawnInterval);
			yield return new WaitForSeconds (randomSpawnTime);
		}
	}

	// this spawns the hazard even though it says spawn arrow???
	private void SpawnHazard() {
		int randomHazard = Random.Range (0, hazards.Length);
		float randomX = Random.Range (bounds.min.x, bounds.max.x);
		float randomY = Random.Range (bounds.min.y, bounds.max.y);
		Vector3 spawnPosition = new Vector3 (randomX, randomY, bounds.center.z);

		Instantiate (hazards [randomHazard], spawnPosition, Quaternion.identity);
	}
}
