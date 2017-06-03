using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Class for moving an object towards a target. Used by text.


public class MovementCooridinator : MonoBehaviour {

	public float speed = 6;
	protected GameObject target;
	protected Rigidbody rb;

	private bool useRelativePositioning = true;

	public virtual void Awake() {
		rb = GetComponent<Rigidbody> ();
	}

	public virtual void Start() {
		target = GameObject.FindGameObjectWithTag("Player");
	}

	void FixedUpdate() {
		MoveToTarget ();
	}


	private void MoveToTarget() {
		if (target != null) {
			if (useRelativePositioning) {
				RelativePositioning ();
			} else {
				AbsolutePositioning ();
			}

			rb.rotation = target.transform.rotation;

		} else {
			Debug.Log ("MovementCoordinator: No target.");
		}
	}

	protected virtual void RelativePositioning() {
		float zPosition = Mathf.MoveTowards (
			rb.position.z, 
			target.transform.position.z, 
			Time.deltaTime * speed);

		rb.position = new Vector3 (
			target.transform.position.x,
			target.transform.position.y,
			zPosition
		);
	}

	// Deprecated
	protected virtual void AbsolutePositioning() {
		rb.position = Vector3.MoveTowards (
			transform.position, 
			target.transform.position, 
			Time.deltaTime * speed
		);
	}
}
