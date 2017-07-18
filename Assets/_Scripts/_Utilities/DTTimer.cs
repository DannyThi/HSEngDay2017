using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DTTimer : MonoBehaviour {

	private float currentTime = 0f;
	private string timeString;
	private TextMeshProUGUI timerDisplay;

	void Awake() {
		timerDisplay = GetComponent<TextMeshProUGUI> ();
	}

	// Update is called once per frame
	void Update () {
		currentTime = Time.time;
		timerDisplay.text = FormatTimer ();
	}

	private string FormatTimer() {
		int seconds = (int)(currentTime % 60);
		int minutes = (int)(currentTime / 60);
		int hours = (int)(minutes / 60);

		int milliseconds = (int)((currentTime * 100) % 100);

		return (hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds);
	}
}
