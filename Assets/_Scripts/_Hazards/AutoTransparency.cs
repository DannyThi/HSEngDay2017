using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTransparency : MonoBehaviour {

	public const float minAlpha = 0.1f;
	public const float maxAlpha = 1.0f;
	public float fadeLength = 0.2f;

	private float targetAlpha;
	private float startAlpha;

	private Renderer materialRenderer;
	private Color color;

	void Awake() {
		color = GetComponentInChildren<Renderer> ().material.color;
	}

	private IEnumerator Fade() {
		Debug.Log ("Starting Fade");
		while (color.a >= minAlpha) {
			color.a = Mathf.Lerp (startAlpha, targetAlpha, Time.deltaTime * fadeLength);
			yield return new WaitForFixedUpdate();
		}
		color.a = targetAlpha;
	}

	public void FadeToMinAlpha() {
		startAlpha = color.a;
		targetAlpha = minAlpha;
		StartCoroutine (Fade ());
	}

	public void FadeToMaxAlpha() {
		Debug.Log ("Received fade request.");
		startAlpha = color.a;
		targetAlpha = maxAlpha;
		StartCoroutine (Fade ());
	}

}
