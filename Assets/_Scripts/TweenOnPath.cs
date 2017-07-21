using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenOnPath : MonoBehaviour {

	public bool testMode = true;
	public string pathName;
	public GameObject objectToMove;
	public float tweenTime = 10;

	void Start() {
		if (testMode == true) {
			iTween.MoveTo (
				gameObject, iTween.Hash (
				"path", iTweenPath.GetPath ("MovePath"),
				"time", 10, 
				"easetype", iTween.EaseType.easeInOutSine
			));
		}
	}

	private void Tween() {
		iTween.MoveTo (
			objectToMove, iTween.Hash (
				"path", iTweenPath.GetPath (pathName),
				"time", tweenTime, 
				"easetype", iTween.EaseType.easeInOutSine
			)
		);
	}


	// listen for a trigger
	// start coroutine to move
	// start co routine to rotate
}
