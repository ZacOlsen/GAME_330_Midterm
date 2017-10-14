using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCreator : MonoBehaviour {

	void OnTriggerEnter(Collider col) {

		if (col.CompareTag ("Ball")) {
			GameObject[] pins = GameObject.FindGameObjectsWithTag ("Pin");
			for (int i = 0; i < pins.Length; i++) {
				pins [i].GetComponent<Rigidbody> ().AddExplosionForce (100, transform.position - new Vector3 (0, -.5f, 0), 5f);
			}
		}
	}
}
