using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSpawnerSinglePoint : MonoBehaviour {

	public GameObject[] hazards;
	public float spawnDelay = 0.5f;

	private List<string> locationData;
	private Vector3 asteroidSpawnPoint;

	void Start() {
		asteroidSpawnPoint = new Vector3 (0, 0, this.transform.position.z);
	}
	private void HandleTextSpawnCall(bool _, string word) {
		StartCoroutine (SpawnSingleHazard ());
	}

	IEnumerator SpawnSingleHazard() {
		//Debug.Log("Spawned 1 hazard");
		yield return new WaitForSeconds (spawnDelay);
		int random = Random.Range (0, hazards.Length);

		Instantiate (hazards [random], new Vector3 (
			asteroidSpawnPoint.x,
			asteroidSpawnPoint.y,
			transform.position.z
		), Quaternion.identity);

		GameObject obj = GameObject.Find (locationData [0]);

		//Debug.Log (locationData [0]);
		asteroidSpawnPoint = new Vector3 (
			obj.transform.position.x,
			obj.transform.position.y, 
			transform.position.z
		);
		if (locationData.Count > 0) {
			locationData.RemoveAt (0);
		}
			
/*		Instantiate (hazards [random], new Vector3 (
			0,
			0,
			transform.position.z
		), Quaternion.identity);
*/
	}

	private void CollectLocationData(List<string> locationData) {
		this.locationData = new List<string>(locationData);
		//this.locationData = locationData;
		Debug.Log ("Hazards: Location Data recieved");
	}

	void OnEnable() {
		TextSpawnController.OnTextSpawned += HandleTextSpawnCall;
		LocationManager.LocationDataReady += CollectLocationData;
	}

	void OnDisable() {
		TextSpawnController.OnTextSpawned -= HandleTextSpawnCall;
		LocationManager.LocationDataReady += CollectLocationData;
	}
}
