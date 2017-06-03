//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine; 
//
//// Abstract Superclass
//
//public abstract class SpawnByTime : CoroutineObject {
//
//	[Tooltip("Wait time before starting the spawn waves")]
//	public float delayStart = 0; 
//
//	[Tooltip("Time between the start of each spawn")]
//	public float spawnTimeInterval = 1;
//
//
//	void Start () {
//		ReferenceObjects ();
//		ClampVariables ();
//	}
//
//
//	// Reference outside game objects to be used in this class.
//	protected ArrowController arrowController;
//	private void ReferenceObjects() {
//		GameObject HUD = GameObject.FindGameObjectWithTag ("HUD");
//		arrowController = HUD.GetComponent<ArrowController> ();
//	}
//
//
//	// Clamp the any variables so  never a negative numthey never fall below zero.
//	// This is mainly to keep onScreenTime from being a negative number.
//	private float minimumSpawnTime = 0.5f;
//	private float timeBetweenEachLine = 0.2f;
//
//	private void ClampVariables() {
//		float onScreenTime = Mathf.Max (minimumSpawnTime, spawnTimeInterval - timeBetweenEachLine);
//		arrowController.onScreenTime = onScreenTime;
//
//		spawnTimeInterval = Mathf.Max (minimumSpawnTime + timeBetweenEachLine, spawnTimeInterval);
//		_totalOnScreenTime = SetTotalOnScreenTime (spawnTimeInterval);
//	}
//
//	// Override this with a concrete class.
//	public float totalOnScreenTime {
//		get {
//			return _totalOnScreenTime;
//		}
//	}
//	protected float _totalOnScreenTime = 0;
//}
