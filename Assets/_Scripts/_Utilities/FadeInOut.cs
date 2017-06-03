using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour {

	public float delayFade = 4; 
	public float fadeTime = 3;

	private new MeshRenderer renderer;
	private float alpha = 0;
	private Color color;

	void Awake() {
		renderer = gameObject.GetComponentInChildren<MeshRenderer> ();
		if (renderer == null) {
			Debug.Log("ChangeColorOnContact: Cannot find renderer.");
		}
	}

	void Start() {
		color = renderer.material.color;
		renderer.material.color = new Color (color.r, color.b, color.g, 0);
		StartCoroutine (StartFadeIn ());
	}

	private IEnumerator StartFadeIn() {
		yield return new WaitForSeconds (delayFade);

		while (true) {
			alpha = Mathf.Lerp (alpha, 1, Time.deltaTime / fadeTime);
			renderer.material.color = new Color (color.r, color.b, color.g, alpha);
			yield return new WaitForEndOfFrame ();
		}

	}
}
