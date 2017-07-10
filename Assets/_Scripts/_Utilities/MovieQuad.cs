using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieQuad : MonoBehaviour {

	public MovieTexture movTexture;
	void Start() {
		GetComponent<Renderer> ().material.mainTexture = movTexture;
		movTexture.Play();
	}
}
