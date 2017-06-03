using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController_ : MonoBehaviour {

	public GameObject[] hazards;

	public Vector3 hazardSpawnPoint;
	public int hazardCountPerWave;
	public float waitBeforeStart;
	public float spawnWait;
	public float waitBeforeNextWave;
	public float restartWaitTime;

	public GUIText scoreText;
	public GUIText gameOverText;
	public GUIText restartText;

	private bool gameOver;
	private bool restartGame;
	private int score;

	void Start () {
		gameOver = false;
		restartGame = false;
		score = 0;

		// Update GUI
		scoreText.text = "Score: 0";
		gameOverText.text = "";
		restartText.text = "";
		//

		StartCoroutine (SpawnWaves());
	}

	IEnumerator SpawnWaves () {
		// Wait for some time before initial wave.
		yield return new WaitForSeconds (waitBeforeStart);
		while (true) {
			for (int i = 0; i < hazardCountPerWave; i++) {
				
				// Choose a hazard from the array.
				GameObject hazard = hazards[Random.Range(0, hazards.Length)];

				// Create a spawn position for the hazard.
				Vector3 spawnPosition = new Vector3 (
					Random.Range(-hazardSpawnPoint.x, hazardSpawnPoint.x),
					hazardSpawnPoint.y,
					hazardSpawnPoint.z
				);

				// A quarternion of 'no rotation'.
				Quaternion spawnRotation = Quaternion.identity;

				// Instantiate gameObject
				Instantiate (hazard, spawnPosition, spawnRotation);

				// Wait before spawning next hazard
				yield return new WaitForSeconds (spawnWait);
			}

			// Wait before starting the next wave.
			yield return new WaitForSeconds (waitBeforeNextWave);

			if (gameOver) {
				// update GUI
				restartGame = true;
				restartText.text = "Press 'R' to Restart";
				break;
			}
		}
	}
		
	void Update() {
		if (restartGame) {
			if (Input.GetKeyDown(KeyCode.R)) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}
	}

	public void IncreaseScore(int scoreValue) {
		score += scoreValue;
		scoreText.text = "Score: " + score;
	}

	public void GameOver() {
		gameOver = true;
		// Update GUI
		gameOverText.text = "Game Over";
	}
}
