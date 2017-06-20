using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Utilities : MonoBehaviour {

	public static string[] ConvertText(TextAsset textFile) {
		if (textFile != null) {
			return textFile.text.Split ('\n');
		} else {
			return null;
		}
	}

	public static SpawnTiming[] ConvertCVS(TextAsset textFile) {
		if (textFile != null) {
//			string[] lines = Utilities.ConvertText (textFile);
//			List<SpawnTiming> list = new List<SpawnTiming> ();
//			foreach (string line in lines) {
//				string time;
//				bool isPaused;
//				if (line.Contains (",") == true) {
//					time = line.Substring (0, 5);
//					isPaused = true;
//				} else {
//					time = line;
//					isPaused = false;
//				}
//
//
//				// convert time
//
//
//
//			}
//			return list.ToArray ();
		} else {
			Debug.Log ("No textfile: ");
			return null;
		}
	}

	private static List<SpawnTiming> ConvertTime(string time) {
//		time = "00:" + time;
//		float seconds = (float)TimeSpan.Parse (time).TotalSeconds;
//		Debug.Log ("Seconds: " + seconds);
//		SpawnTiming timing = new SpawnTiming (seconds, isPaused);
//
//		list.Add (timing);
//
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
