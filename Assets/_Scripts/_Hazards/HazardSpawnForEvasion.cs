using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSpawnForEvasion : MonoBehaviour {

	public GameObject[] hazards;
	public float spawnDelay = 1.0f;

	private Vector3 newSpawnLocation;

	void Start() {
		newSpawnLocation = new Vector3 (0, 0, this.transform.position.z);
	}

	private IEnumerator SpawnHazardForEvasion(Vector3 location) {
		yield return new WaitForSeconds (spawnDelay);
		int randomHazard = Random.Range (0, hazards.Length);
		Instantiate (hazards [randomHazard], newSpawnLocation, Quaternion.identity);

		newSpawnLocation = new Vector3 (location.x, location.y, transform.position.z);
	}

	private void HandleFloatingTextSpawnNotification(FloatingTextObject textObject) {
		GameObject moveLocation = GameObject.Find ("Loc (" + textObject.coordinates.x + ", " 
														   + textObject.coordinates.y + ")");
		StartCoroutine (SpawnHazardForEvasion (moveLocation.transform.position));
	}

	void OnEnable() {
		FloatingTextSpawnManager.floatingTextNotification += 
			HandleFloatingTextSpawnNotification;
	}

	void OnDisable() {
		FloatingTextSpawnManager.floatingTextNotification -= 
			HandleFloatingTextSpawnNotification;
	}
}
