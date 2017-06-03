//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class FixedSpawnCoroutine : SpawnByTime {
//
//	public string[] commands;
//
//	public override IEnumerator StartRoutine () {
//		Debug.Log ("FixedSpawnCoroutine Started.");
//
//		if (commands == null) {
//			Debug.Log ("No commands present.");
//		} else {
//			
//			yield return new WaitForSeconds (delayStart);
//			foreach (string command in commands) {
//				arrowController.TriggerArrowEvent (command);
//				yield return new WaitForSeconds (spawnTimeInterval);
//			}
//		}
//
//		Debug.Log ("FixedSpawnCoroutine Finished.");
//	}
//
//
//	protected override float SetTotalOnScreenTime(float spawnTimeInterval) {
//		float time = delayStart + (spawnTimeInterval * commands.Length);
//		return time;
//	}
//}
