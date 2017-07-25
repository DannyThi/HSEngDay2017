using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
	private TextMeshProUGUI fpsDisplay;
	private float deltaTime = 0.0f;

	void Awake() {
		fpsDisplay = GetComponent<TextMeshProUGUI> ();
	}

	void Update()
	{
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
		fpsDisplay.SetText (FormatFPSString ());
	}

	private string FormatFPSString() {
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		return string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
	}
}