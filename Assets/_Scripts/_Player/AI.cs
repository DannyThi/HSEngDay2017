
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

	public Boundary boundary;

	public bool playerControl = false;
	public float movementTilt = 3;
	public float dodgeDistance = 6;
	public float smoothing = 10;

	[Range(0.0f, 1.0f)]
	public float barrelRollFrequency = 0.20f;

	private Rigidbody rb;
	private Vector3 targetManeuver;
	private float maneuverTime = 2; 
	private GameObject playerModel;

	private List<string> locationData = new List<string> {};

	private Vector3 rotationTarget;
	private Vector3 rotationVelocity;

	public delegate void BarrelRollNotification(bool rotation);
	public static event BarrelRollNotification onDodge;

	void Awake () {
		rb = GetComponent<Rigidbody> ();
		playerModel = GameObject.Find ("PlayerModel");
	}

	void Start() {
		barrelRollFrequency = Mathf.Clamp01(barrelRollFrequency);
		targetManeuver = Vector3.zero;
	}

	public void SetManeuverTime(float time) {
		maneuverTime = Mathf.Max (0.5f, time / 2);
		//Debug.Log ("ManeuverTime set to " + time);
	}

	void HandleDodgeEvent(Vector3 direction) {
		StartCoroutine (Evade (direction));
	}

	public IEnumerator Evade(Vector3 direction) {
		targetManeuver = GameObject.Find (locationData [0]).transform.position;
		rotationTarget = direction;

		if (targetManeuver.x != 0) {
			if (Random.Range (0.0f, 1.0f) <= barrelRollFrequency) {
				StartCoroutine (DoABarrelRoll (direction.x));
			}
		}

		if (locationData.Count > 0) {
			locationData.RemoveAt (0);
		}
		yield return new WaitForSeconds (maneuverTime * 0.5f);
		rotationTarget = Vector3.zero;

	}
		
	void FixedUpdate() {
		rb.position = Vector3.Lerp (rb.position, targetManeuver, smoothing * Time.deltaTime);
		rotationVelocity = Vector3.Lerp (rotationVelocity, rotationTarget, smoothing * Time.deltaTime);
		rb.rotation = Quaternion.Euler (
			rotationVelocity.y * -movementTilt, 
			0, 
			rotationVelocity.x * -movementTilt
		);

	}




	// ******* BARREL ROLL *******

	private float barrelRollDuration {
		get {
			return maneuverTime / 2;
		}
	}

	IEnumerator DoABarrelRoll(float direction) {
		onDodge (true);
		iTween.RotateBy (playerModel, iTween.Hash ("z", -direction, "time", maneuverTime));
		yield return new WaitForSeconds (maneuverTime);
		onDodge (false);
	}

	private void CollectLocationData(List<string> locationData) {
		this.locationData = new List<string> ();
		this.locationData = new List<string> (locationData);

		Debug.Log ("AI: Location Data recieved");
	}


	void OnEnable() {
		Arrow.DodgeEventTriggerer += HandleDodgeEvent;
		LocationManager.LocationDataReady += CollectLocationData;
	}

	void OnDisable() {
		Arrow.DodgeEventTriggerer -= HandleDodgeEvent;
		LocationManager.LocationDataReady += CollectLocationData;
	}

	//	private Vector3 ClampToBoundary(Vector3 position) {
	//		Vector3 newPosition = new Vector3 (
	//			Mathf.Clamp (position.x, boundary.xMin, boundary.xMax),
	//			Mathf.Clamp (position.y, boundary.yMin, boundary.yMax),
	//			position.z
	//		);
	//		return newPosition;
	//	}
}



