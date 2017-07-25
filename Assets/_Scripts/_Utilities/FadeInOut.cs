using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour {

	public float delayFade = 4; 
	public float fadeTime = 3;

	private new MeshRenderer renderer;
	private float alpha = 0;
	private Color color;
	private float currentTime;

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
			currentTime += Time.deltaTime;
	
			float t = currentTime / fadeTime;
			//t = Mathf.Sin(t * Mathf.PI * 0.5f);
			t = 1f - Mathf.Cos (t * Mathf.PI * 0.5f);

			alpha = Mathf.Lerp (0, 1, t);
			renderer.material.color = new Color (color.r, color.b, color.g, alpha);

			if (currentTime > fadeTime) {
				break;
			}
			yield return new WaitForFixedUpdate();
		}

	}
}
