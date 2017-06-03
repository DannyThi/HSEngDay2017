using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenOverlay : MonoBehaviour {

	[Header("Crossfade")]
	[Tooltip("The duration of the crossfade in seconds.")]
	public float fadeDuration = 1;
	public bool startSceneFaded = false;
	private GUITexture texture;
	private float alpha = 1;

	void Awake() {
		texture = GetComponent<GUITexture> ();
		if (texture == null) {
			Debug.Log (name + ": No texture.");
		} else {
			alpha = texture.color.a;
		}
	}

	void Start() {
		if (startSceneFaded == true) {
			texture.color = new Color (0, 0, 0, 1);
		}
	}



	// THIS NEEDS FIXING!!

	// Fade in ** FROM ** black. 1 > 0
	public IEnumerator FadeIn() {
		alpha = texture.color.a;
		while (true) {
			DoCrossFadeMath (alpha, 0, 1);
			yield return new WaitForEndOfFrame ();
			Debug.Log (alpha);
			if (alpha < 0) {
				break;
			}
		}
	}

	// Fade out ** TO ** black 0 > 1
	public IEnumerator FadeOut() {
		while (alpha != 1) {
			DoCrossFadeMath (alpha, 1, -1);
			yield return new WaitForEndOfFrame ();
		}
	}

	// We are fading the texture. 1 = Opaque, 0 = Transparent
	private void DoCrossFadeMath (float from, float to, int fadeDirection) {
		alpha = Mathf.Lerp (from, to, fadeDirection * fadeDuration * Time.deltaTime);
		Mathf.Clamp01 (alpha);
		texture.color = new Color (0, 0, 0, alpha);
	}
		
	public void CrossFade (bool fadeIn) {
		alpha = texture.color.a;
		if (fadeIn == true) {
			StartCoroutine (FadeIn ());
		} else {
			StartCoroutine (FadeOut ());
		}
	}



	void OnEnable() {
		PauseFunction.OnGamePauseNotification += HandleDimScreen;
	}

	void OnDisable() {
		PauseFunction.OnGamePauseNotification -= HandleDimScreen;
	}

	// This is riggered when the pause screen appears.
	private void HandleDimScreen(float timeScale) {
		if (timeScale > 0) {
			texture.color = new Color (0, 0, 0, 0);
		} else {
			texture.color = new Color (0, 0, 0, 0.3f);
		}
	}

}
