using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour {

	public List<SceneObject> scenes;

	private float currentTime = 0;

	void Update() {
		LoadScene ();
	}

	private void LoadScene() {
		if (scenes.Count > 0) {
			currentTime = Time.time;
			if (currentTime >= scenes [0].spawnTime) {
				SceneManager.LoadScene (scenes [0].sceneName, LoadSceneMode.Additive);
				scenes.RemoveAt(0);
			}
		}
	}
}
