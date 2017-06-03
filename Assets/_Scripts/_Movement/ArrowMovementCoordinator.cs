using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is attached to a 3D arrow. It moves the arrow towards the target (corresponding 2D arrow).
// The GameController tracks the number of arrows on screen. Other classes may use that to determine
// when to display following coroutines.

public class ArrowMovementCoordinator : MovementCooridinator {

	public override void Start ()
	{
		// Find the target
		FindTarget ();
	}

	protected void FindTarget ()
	{
		switch (tag.ToString ()) {
		case "Left_3d":
			ReferenceFlatArrow ("Left_flat");
			break;
		case "Right_3d":
			ReferenceFlatArrow ("Right_flat");
			break;
		case "Up_3d":
			ReferenceFlatArrow ("Up_flat");
			break;
		case "Down_3d":
			ReferenceFlatArrow ("Down_flat");
			break;
		default:
			Debug.Log ("Model not an arrow?");
			break;
		}
	}

	private void ReferenceFlatArrow (string arrow) {
		target = GameObject.FindGameObjectWithTag(arrow);
	}
}