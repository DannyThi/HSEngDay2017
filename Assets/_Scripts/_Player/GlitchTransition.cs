using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchTransition : MonoBehaviour {

	public float glitchValue = 1;
	private Kino.AnalogGlitch analogue;
	private Kino.DigitalGlitch digital;

	void Awake() {
		analogue = GetComponent<Kino.AnalogGlitch> ();
		digital = GetComponent<Kino.DigitalGlitch> ();
	}

	void Start() {
		glitchValue = Mathf.Clamp (glitchValue, 0.1f, glitchValue);
	}

	IEnumerator Glitch() {
		// start glitch in a loop until value is = to value
		// wait
		// unglitch back to 0.1
		yield return null;

	}

	private void IntiateAnalogueGlitch(float value) {
		analogue.scanLineJitter = value;
		analogue.verticalJump = value;
		analogue.horizontalShake = value;
		analogue.colorDrift = value;
	}

	private void InitiateDigitalGlitch(float value) {
		digital.intensity = value;
	}

	private void HandleTransitionNotification() {
		//StartCoroutine (Glitch ());
	}

	void OnEnable() {
		FinalLevel.OnTransitionTrigger += HandleTransitionNotification;
	}

	void OnDisable() {
		FinalLevel.OnTransitionTrigger -= HandleTransitionNotification;
	}
}
