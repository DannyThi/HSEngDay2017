using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMovementCoordinator : MovementCooridinator {

	public override void Start() {
		target = GameObject.FindGameObjectWithTag ("CullPlane");
		if (target == null) {
			Debug.Log (name + ": No CullPlane");
		}
	}


}
