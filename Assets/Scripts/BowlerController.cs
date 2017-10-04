using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowlerController : MonoBehaviour {

	[SerializeField] private float rotateSpeed = 120f;
	[SerializeField] private float moveSpeed = 5f;

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
		Rotating,
		Moving,
		Charging,
		Idle
	}
	
	void Update () {

		if (state == BowlerState.Rotating) {

			if (FigmentInput.GetButton (FigmentInput.FigmentButton.LeftButton)) {
				transform.Rotate (Vector3.up, -rotateSpeed * Time.deltaTime);
			} if (FigmentInput.GetButton (FigmentInput.FigmentButton.RightButton)) {
				transform.Rotate (Vector3.up, rotateSpeed * Time.deltaTime);
			}

			if (FigmentInput.GetButtonDown(FigmentInput.FigmentButton.ActionButton)) {
				state = BowlerState.Moving;
				return;
			}
		}

		if (state == BowlerState.Moving) {

			if (FigmentInput.GetButton (FigmentInput.FigmentButton.LeftButton)) {
				transform.position += -Vector3.right * moveSpeed * Time.deltaTime;
			} if (FigmentInput.GetButton (FigmentInput.FigmentButton.RightButton)) {
				transform.position += Vector3.right * moveSpeed * Time.deltaTime;
			}

			if (FigmentInput.GetButtonDown(FigmentInput.FigmentButton.ActionButton)) {
				state = BowlerState.Charging;
				return;
			}
		}

		if (state == BowlerState.Charging) {

			if (FigmentInput.GetButtonDown (FigmentInput.FigmentButton.ActionButton)) {
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
			//	Camera.main.transform.localPosition = Vector3.zero;
			//	Camera.main.transform.localRotation = Quaternion.identity;

				state = BowlerState.Idle;
				charging = false;
				return;
			}
		}
	}
}
