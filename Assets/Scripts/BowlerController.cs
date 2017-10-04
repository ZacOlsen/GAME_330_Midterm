using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowlerController : MonoBehaviour {

	[SerializeField] private float rotateSpeed = 120f;
	[SerializeField] private float maxRotationAngle = 45f;
	private bool right = true;

	[SerializeField] private float moveSpeed = 5f;
	[SerializeField] private float maxMoveDist = 3.5f;
	private float startX;

	[SerializeField] private float minBallLaunchSpeed = 5f;
	[SerializeField] private float maxBallLaunchSpeed = 15f;
	[SerializeField] private float chargeCycleTime = 1f;
	[SerializeField] private Image chargeBar = null;
	private float timeOfChargeStart;
	private bool charging;
	private bool chargeIncreasing = true;

	[SerializeField] private GameObject bowlingBall = null;
	[SerializeField] private Transform spawnLoc = null;

	[SerializeField] private BowlerState state = BowlerState.Rotating;

	private enum BowlerState {
		Moving,
		Rotating,
		Charging,
		Idle
	}

	void Start () {
		startX = transform.position.x;
	}

	void Update () {

		if (state == BowlerState.Moving) {
			
			if (right) {
				transform.position += Vector3.right * moveSpeed * Time.deltaTime;
				if (transform.position.x > startX + maxMoveDist) {
					right = false;
				}
				
			} else {
				transform.position -= Vector3.right * moveSpeed * Time.deltaTime;
				if (transform.position.x < startX - maxMoveDist) {
					right = true;
				}
			}
			
			if (FigmentInput.GetButtonDown(FigmentInput.FigmentButton.ActionButton)) {
				state = BowlerState.Rotating;
				right = true;
				return;
			}
		}

		if (state == BowlerState.Rotating) {

			if (right) {
				transform.Rotate (Vector3.up, rotateSpeed * Time.deltaTime);
				if (transform.eulerAngles.y > maxRotationAngle && transform.eulerAngles.y < 180f) {
					right = false;
				}

			} else {
				transform.Rotate (Vector3.up, -rotateSpeed * Time.deltaTime);
				if (transform.eulerAngles.y < 360f - maxRotationAngle && transform.eulerAngles.y > 180f) {
					right = true;
				}
			}
				

			if (FigmentInput.GetButtonDown(FigmentInput.FigmentButton.ActionButton)) {
				state = BowlerState.Charging;
				right = true;
				return;
			}
		}
			
		if (state == BowlerState.Charging) {

			if (!charging && FigmentInput.GetButtonDown (FigmentInput.FigmentButton.ActionButton)) {
				charging = true;
				timeOfChargeStart = Time.time;
			}

			if (charging) {

				float percent = (Time.time - timeOfChargeStart) / chargeCycleTime;
				if (percent > chargeCycleTime) {
					timeOfChargeStart = Time.time;
					percent -= 1f;
					chargeIncreasing = !chargeIncreasing;
				}

				if (chargeIncreasing) {
					chargeBar.fillAmount = percent;
				} else {
					chargeBar.fillAmount = 1f - percent;
				}
			}

			if (charging && FigmentInput.GetButtonUp(FigmentInput.FigmentButton.ActionButton)) {

				float launchSpeed = chargeBar.fillAmount * (maxBallLaunchSpeed - minBallLaunchSpeed) + minBallLaunchSpeed;

				GameObject ball = Instantiate (bowlingBall, spawnLoc.position, Quaternion.identity);
				ball.GetComponent<Rigidbody> ().velocity = transform.TransformDirection (Vector3.forward) * launchSpeed;
				Camera.main.transform.parent = ball.transform.GetChild (0);

				state = BowlerState.Idle;
				charging = false;
				chargeBar.fillAmount = 0;
				return;
			}
		}
	}

	public void ResetShot () {

		transform.position = new Vector3 (startX, transform.position.y, transform.position.z);
		transform.rotation = Quaternion.identity;
		state = BowlerState.Moving;

		chargeIncreasing = true;
	}
}
