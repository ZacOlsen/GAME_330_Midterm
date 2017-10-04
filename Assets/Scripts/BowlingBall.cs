using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBall : MonoBehaviour {

	[SerializeField] private AudioClip hitGround = null;
	[SerializeField] private AudioClip hitPins = null;

	private AudioSource source;

	private bool camStop;

	void Start () {
		source = GetComponent<AudioSource> ();
	}

	void FixedUpdate () {

		if (!camStop) {
			Camera.main.transform.localPosition = Vector3.Lerp (Camera.main.transform.localPosition, Vector3.zero, .1f);
			Camera.main.transform.localRotation = Quaternion.Lerp (Camera.main.transform.localRotation, Quaternion.identity, .1f);
		}
	}

	void OnCollisionEnter(Collision col){

		if (col.collider.CompareTag ("Pin")) {
			source.PlayOneShot (hitPins);
		}

		if (col.collider.CompareTag ("Ground")) {
			source.PlayOneShot (hitGround);
		}
	}

	void OnTriggerEnter(Collider col){

		Camera.main.transform.parent = null;
		camStop = true;
	}
}
