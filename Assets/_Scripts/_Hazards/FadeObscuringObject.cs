using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObscuringObject : MonoBehaviour {

	public float distanceToCamera = 100.0f;
	public float updateInterval = 2.0f;

	private Camera cam;

	// 15 = 3dArrows Layer
	// 16 = WordContainer Layer
	//private int layerMask = (1 << 15) | (1 << 16);
	private float nextUpdate = 0;

	void Awake() {
		cam = GameObject.FindGameObjectWithTag ("FollowCam").GetComponent<Camera> ();
	}

	void Update() {
		Raycasting ();
	}


	private void Raycasting() {
		// update every two seconds
		if (Time.time > nextUpdate) 
		{
			nextUpdate = Time.time + updateInterval;

			RaycastHit[] raycastHits = Physics.RaycastAll (
				transform.position, 
				cam.transform.position,
				distanceToCamera
			);

			foreach (RaycastHit hit in raycastHits) {
				AutoTransparency transparency = hit.transform.gameObject.GetComponent<AutoTransparency> ();
				if (transparency != null) {
					transparency.FadeToMinAlpha ();
				}
			}
		}
	}
}
