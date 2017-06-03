using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour {

	public static string[] ConvertText(TextAsset textFile) {
		if (textFile != null) {
			return textFile.text.Split ('\n');
		} else {
			return null;
		}
	}

	public static GameController GetGameController() {
		GameController gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
		if (gameController == null) {
			Debug.Log ("Utilities: No GameController");
			return null;
		}
		return gameController;
	}

}
