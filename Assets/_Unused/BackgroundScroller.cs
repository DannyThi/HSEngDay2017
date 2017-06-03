using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

	public float scrollSpeed;
	public float tileSizeZ;

	private Vector3 startPosition;

	// Use this for initialization
	void Start () {
		// We need to also get the x and z (mostly the z) position of the gameObject.
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
		// newPosition is only a float(someNewValue). If we set this and the Vector3.forward 
		// directly to the transform.position (without the startPosition), the background will
		// 'just' up, obscuring the starfield.
		transform.position = startPosition + Vector3.forward * newPosition;
	}
}
