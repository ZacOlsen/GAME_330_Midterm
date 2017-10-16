using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreCalculator : MonoBehaviour {

	[SerializeField] private int currentFrame = 0;
	[SerializeField] private float timeBeforeScoreCount = 1.5f;

	public const int NUM_OF_FRAMES = 5;
	private GameObject scoreBoard;

	private static BowlingFrame first;
	private bool firstShot = true;
	private LaneReset laneReset;

	[SerializeField] private Transform camLoc = null;
	private BowlerController bc;

	void Start () {
		
		scoreBoard = GameObject.Find ("Score Board");
		bc = GameObject.Find ("Player").GetComponent<BowlerController> ();

		if (first == null) {
			first = new BowlingFrame (1);
			BowlingFrame current = first;

			for (int i = 2; i <= NUM_OF_FRAMES; i++) {
				current.next = new BowlingFrame (i);
				current = current.next;
			}
		}

		UpdateScore ();

		laneReset = GameObject.Find ("Lane Cleaner").GetComponent<LaneReset> ();
	}

	void UpdateScore () {

		for (int frameNum = 1; frameNum <= NUM_OF_FRAMES; frameNum++) {
			BowlingFrame frame = GetFrame (frameNum);
			Transform trans = scoreBoard.transform.GetChild (frameNum - 1);

			trans.GetChild (0).GetComponent<Text> ().text = frame.GetShot1 ();
			trans.GetChild (1).GetComponent<Text> ().text = frame.GetShot2 ();
			trans.GetChild (2).GetComponent<Text> ().text = "" + (frame.CalculateScore () >= 0 ? frame.CalculateScore()  + "" : "");
		}
	}

	public void ReceivePinsDown (int pinsDown) {

		BowlingFrame frame = GetFrame (currentFrame);

		if (firstShot) {
			frame.shot1Pins = pinsDown;
			firstShot = false;

			if (pinsDown == 10) {
				currentFrame++;
				SceneManager.LoadScene (currentFrame);
				return;
			}

		} else {
			frame.shot2Pins = pinsDown - frame.shot1Pins;
			currentFrame++;
			SceneManager.LoadScene (currentFrame);
			return;
		}

		Camera.main.transform.parent = camLoc;
		Camera.main.transform.localPosition = Vector3.zero;
		Camera.main.transform.localRotation = Quaternion.identity;

		UpdateScore ();
		bc.ResetShot ();
	}

	public static int GetTotalScore () {

		int score = 0;
		BowlingFrame frame = first;

		while (frame != null) {
			score += frame.CalculateScore ();
			frame = frame.next;
		}

		return score;
	}

	private BowlingFrame GetFrame (int frame) {

		BowlingFrame current = first;

		for (int i = 1; i < frame; i++) {
			current = current.next;
		}

		return current;
	}

	public void StartLaneReset () {
		laneReset.ClearLane ();
	}

	void OnTriggerEnter (Collider col) {

		if (col.CompareTag ("Ball")) {
			Invoke ("StartLaneReset", timeBeforeScoreCount);
			Destroy (col.GetComponent<BowlingBall> ());
		}
	}
}
