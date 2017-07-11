using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchTransition : MonoBehaviour {

	[Tooltip("While active, press <SPACE> to trigger glitch.")]
	public bool testMode = false;

	[Tooltip("The total time it takes to lerp from A to B.")]
	public float glitchLerpTime = 1;
	[Tooltip("The wait time before lerping from B back to A")]
	public float waitTime = 2;

	private const float minGlitchValue = 0.01f;
	private const float maxGlitchValue = 1.0f;

	private float glitchTarget = 0.01f;
	private float currentGlitchValue = 0.01f;
	private float glitchVelocity = 0.0f;

	private Kino.AnalogGlitch analogue;
	private Kino.DigitalGlitch digital;

	void Awake() {
		analogue = GetComponent<Kino.AnalogGlitch> ();
		digital = GetComponent<Kino.DigitalGlitch> ();
	}

	IEnumerator Glitch() {
		glitchTarget = maxGlitchValue;
		yield return new WaitForSeconds(glitchLerpTime);
		yield return new WaitForSeconds (waitTime);
		glitchTarget = minGlitchValue;
	}

	void Update() {
		currentGlitchValue = Mathf.SmoothDamp (
			currentGlitchValue, 
			glitchTarget, 
			ref glitchVelocity, 
			glitchLerpTime
		);

		InitiateDigitalGlitch (currentGlitchValue);
		InitiateAnalogueGlitch (currentGlitchValue);

		if (testMode == true) {
			if (Input.GetKeyUp(KeyCode.Space)) {
				HandleTransitionNotification ();
			}
		}
	}

	private void InitiateAnalogueGlitch(float value) {
		analogue.scanLineJitter = value;
		analogue.verticalJump = value;
		analogue.horizontalShake = value;
		analogue.colorDrift = value;
	}

	private void InitiateDigitalGlitch(float value) {
		digital.intensity = value;
	}

	private void HandleTransitionNotification() {
		StartCoroutine (Glitch ());
	}

	void OnEnable() {
		FinalLevel.OnTransitionTrigger += HandleTransitionNotification;
	}

	void OnDisable() {
		FinalLevel.OnTransitionTrigger -= HandleTransitionNotification;
	}
}
