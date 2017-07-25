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
		//matRenderer.material.color = new Color (color.r, color.b, color.g, 2); // wtf?
	}
		
	private void FadeUsingITween() {
		iTween.FadeTo (
			gameObject, iTween.Hash (
				"amount", targetAlpha, 
				"time", fadeLength, 
				"easetype", iTween.EaseType.easeInSine
			)
		);
	}

	public void FadeToMinAlpha() {
		if (isFading == false) {
			isFading = true;
			SetMaterialTransparent ();
			startAlpha = color.a;
			targetAlpha = minAlpha;
			FadeUsingITween ();

			//StartCoroutine (Fade ());
		}
	}

	public void FadeToMaxAlpha() {
		startAlpha = color.a;
		targetAlpha = maxAlpha;
		StartCoroutine (Fade ());
	}


	private void SetMaterialTransparent() {
		foreach (Material m in GetComponentInChildren<Renderer>().materials) {
			m.SetFloat ("_Mode", 2);
			m.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			m.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			m.SetInt ("_ZWrite", 0);
			m.DisableKeyword ("_ALPHATEST_ON");
			m.EnableKeyword ("_ALPHABLEND_ON");
			m.DisableKeyword ("_ALPHAPREMULTIPLY_ON");
			m.renderQueue = 3000;
		}
	}



	private IEnumerator Fade() {
		Debug.Log ("Starting Fade");
		while (true) {
			currentTime += Time.deltaTime;
			float t = currentTime / fadeLength;
			t = 1f - Mathf.Cos (t * Mathf.PI * 0.5f);
			newAlpha = Mathf.Lerp (startAlpha, targetAlpha, t);
			Color newColor = new Color (color.r, color.b, color.g, newAlpha);
			matRenderer.material.color = newColor;

			if (currentTime > fadeLength) {
				break;
			}
			yield return new WaitForFixedUpdate();
		}
	}
}
