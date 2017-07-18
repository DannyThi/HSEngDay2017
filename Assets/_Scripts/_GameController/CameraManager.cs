using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraManager : MonoBehaviour {

	public bool testMode = false;

	public Camera playerFollowCam;
	public Camera[] glamourCams;

	public float testModeStartTimer;
	public float cameraSwitchTime = 3;

	private int nextCameraNumber;

	void Start() {
		if (testMode == true) {
			StartCoroutine (StartTimer ());
		}
	}

	private IEnumerator StartTimer() {
		while (true) {
			if (Time.time >= testModeStartTimer) {
				StartCoroutine ("ActivateGlamourCams");
				break;
			}
			yield return null;
		}
	}

	private IEnumerator ActivateGlamourCams() {
		playerFollowCam.enabled = false;
		while (true) {
			SwitchGlamourCam ();
			yield return new WaitForSeconds (cameraSwitchTime);
		}
	}

	private void SwitchGlamourCam() {
		for(int i = 0; i < glamourCams.Length; i++) {
			if (i == nextCameraNumber) {
				glamourCams [i].enabled = true;
			} else {
				glamourCams [i].enabled = false;
			}
		}

		nextCameraNumber++;
		if (nextCameraNumber >= glamourCams.Length) {
			nextCameraNumber = 0;
		}
	}

	private void EnablePlayerFollowCam() {
		playerFollowCam.enabled = true;
		for(int i = 0; i < glamourCams.Length; i++) {
			glamourCams [i].enabled = false;
		}
	}

	private void HandleSceneStartNotification(string notificationName) {
		if (notificationName == "StartGlamCam") {
			Debug.Log ("Starting Glam Cam.");
			playerFollowCam.enabled = false;
			StartCoroutine ("ActivateGlamourCams");
		}
	}

	private void HandleSceneEndNotification(string notificationName) {
		if (notificationName == "StopGlamCam") {
			Debug.Log ("Stopping Glam Cam.");
			StopCoroutine ("ActivateGlamourCams");
			EnablePlayerFollowCam ();
		}
	}

	void OnEnable() {
		NotificationManager.eventNotification += HandleSceneStartNotification;
		NotificationManager.eventNotification += HandleSceneEndNotification;

	}

	void OnDisable() {
		NotificationManager.eventNotification -= HandleSceneStartNotification;
		NotificationManager.eventNotification -= HandleSceneEndNotification;
	}
}
