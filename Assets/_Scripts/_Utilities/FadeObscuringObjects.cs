﻿//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class FadeObscuringObjects : MonoBehaviour {
//	
//	private RaycastHit oldHit;
//
//	void FixedUpdate() {
//		XRay ();
//	}
//
//	private void XRay() {
//
//		float characterDistance = Vector3.Distance (transform.position, GameObject.Find ("Character").transform.position);
//		Vector3 fwd = transform.TransformDirection (Vector3.forward);
//
//		RaycastHit hit;
//		if (Physics.Raycast (transform.position, fwd, out hit, characterDistance)) {
//			if (oldHit.transform) {
//
//				Color colorA = oldHit.transform.gameObject.GetComponent<Renderer>().material.color;
//				colorA.a = 1f;
//				oldHit.transform.gameObject.GetComponent<Renderer> ().material.SetColor ("_Color", colorA);
//			}
//
//			Color colorB = hit.transform.gameObject.GetComponent<Renderer> ().material.color;
//			colorB.a = 0.5f;
//			hit.transform.gameObject.GetComponent<Renderer> ().material.SetColor ("_Color", colorB);
//
//			oldHit = hit;
//		}
//	}
//}
 