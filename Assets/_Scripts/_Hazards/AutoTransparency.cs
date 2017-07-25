using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTransparency : MonoBehaviour {

	public const float minAlpha = 0.2f;
	public const float maxAlpha = 1.0f;
	public float fadeLength = 0.2f;

	private float startAlpha; 
	private float targetAlpha;
	private float newAlpha;
	private bool isFading = false;
	private float currentTime;

	private Renderer matRenderer;
	private Color color;

	void Awake() {
		matRenderer = gameObject.GetComponentInChildren<MeshRenderer> ();
		color = GetComponentInChildren<MeshRenderer> ().material.color;
	}

	void Start() {
		matRenderer.material.color = new Color (color.r, color.b, color.g, 255); // wtf?
	}

	private IEnumerator Fade() {
		Debug.Log ("Starting Fade");
		while (true) {
			currentTime += Time.deltaTime;

			float t = currentTime / fadeLength;
			t = Mathf.Sin(t * Mathf.PI * 0.5f);

			newAlpha = Mathf.Lerp (startAlpha, targetAlpha, t);
			Color newColor = new Color (color.r, color.b, color.g, newAlpha);
			matRenderer.material.color = newColor;

			if (currentTime > fadeLength) {
				break;
			}

			yield return new WaitForFixedUpdate();
		}
	}

	public void FadeToMinAlpha() {
		if (isFading == false) {
			isFading = true;
			startAlpha = color.a;
			targetAlpha = minAlpha;
			StartCoroutine (Fade ());
		}
	}

	public void FadeToMaxAlpha() {
		startAlpha = color.a;
		targetAlpha = maxAlpha;
		StartCoroutine (Fade ());
	}

}
