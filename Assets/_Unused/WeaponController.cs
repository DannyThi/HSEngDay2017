using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

	public GameObject bolt;
	public Transform shotSpawn;
	public float startDelay;
	public float fireRate;

	void Start() {
		InvokeRepeating ("Fire", startDelay, fireRate);
	}

	private void Fire () {
		Instantiate (bolt, shotSpawn.position, shotSpawn.rotation);
	}
}
