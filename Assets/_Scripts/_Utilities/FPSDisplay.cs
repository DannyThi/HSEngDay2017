using UnityEngine;
using System.Collections;

public class FPSDisplay : MonoBehaviour
{
	public Color32 color = new Color32 (0, 0, 0, 1);
	public TextAnchor anchor;

	private float deltaTime = 0.0f;
	private Rect rect;
	private GUIStyle style = new GUIStyle();

	void Start() {
		int w = Screen.width, h = Screen.height; 
		rect = new Rect (0, 0, w, h);
		style.alignment = anchor;
		style.fontSize = h * 2 / 100;
		style.normal.textColor = color;
	}

	void Update()
	{
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
	}

	void OnGUI()
	{
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		GUI.Label(rect, text, style);
	}
}