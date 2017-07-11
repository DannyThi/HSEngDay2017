using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract Superclass

public class TimedObject : IComparable<TimedObject> {

	public float spawnTime;

	public TimedObject(float time) {
		spawnTime = time;
	}
		

	public int CompareTo(TimedObject other) {
		if (other == null) return 1;

		float difference = spawnTime - other.spawnTime;
		if (difference == 0.0f) {
			return 0;
		} else {
			return (int)Mathf.Sign (difference);
		}
	}
}
