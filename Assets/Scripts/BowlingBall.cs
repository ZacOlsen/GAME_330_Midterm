using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBall : MonoBehaviour {

	[SerializeField] private AudioClip hitGround = null;
	[SerializeField] private AudioClip hitPins = null;

	private Rigidbody rb;
	private AudioSource source;
	private bool pinSoundPlayed;

	private bool camStop;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		source = GetComponent<AudioSource> ();
	}

	void FixedUpdate () {

		if (rb.velocity.magnitude < .2f) {
			GameObject.Find ("End Zone").GetComponent<ScoreCalculator> ().StartLaneReset ();
			Destroy (this);
			return;
		}

		float angle = Mathf.Atan (Mathf.Abs(rb.velocity.x / rb.velocity.z)) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3 (0, angle, 0)), .1f);

		if (!camStop) {
			Camera.main.transform.localPosition = Vector3.Lerp (Camera.main.transform.localPosition, 
				Vector3.zero, .1f);
			Camera.main.transform.localRotation = Quaternion.Lerp (Camera.main.transform.localRotation, 
				Quaternion.identity, .1f);

		} else {
			Quaternion camRot = Camera.main.transform.rotation;
			Camera.main.transform.LookAt (transform);
			Camera.main.transform.rotation = Quaternion.Lerp(camRot, Camera.main.transform.rotation, .1f);
		}
	}

	void OnCollisionEnter (Collision col) {

		if (!pinSoundPlayed && col.collider.CompareTag ("Pin")) {
			source.volume = StartMenu.GetVolumeLevel ();
			source.PlayOneShot (hitPins);
			pinSoundPlayed = true;
		}

		if (col.collider.CompareTag ("Ground")) {
			source.volume = StartMenu.GetVolumeLevel ();
			source.PlayOneShot (hitGround);
		}
	}

	void OnTriggerEnter (Collider col) {

		Camera.main.transform.parent = null;
		camStop = true;
	}
}
