using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCalculator : MonoBehaviour {

	[SerializeField] private float timeBeforeScoreCount = 1.5f;
	private float timeOfOut = float.MaxValue;

	public const int NUM_OF_FRAMES = 10;
	private GameObject scoreBoard;

	private BowlingFrame first;
	private LaneReset laneReset;

	void Start () {
		
		scoreBoard = GameObject.Find ("Score Board");

		first = new BowlingFrame (1);
		BowlingFrame current = first;

		for (int i = 2; i <= NUM_OF_FRAMES; i++) {
			current.next = new BowlingFrame (i);
			current = current.next;
		}

		laneReset = GameObject.Find ("Lane Cleaner").GetComponent<LaneReset> ();
	}

	void FixedUpdate () {

	//	if (Time.time - timeOfOut > timeBeforeScoreCount) {
	//		GetScore (0);
	//	}
	}

	void GetScore (int frameNum) {

		BowlingFrame frame = GetFrame (frameNum);
		Transform trans = scoreBoard.transform.GetChild (frameNum);

		trans.GetChild (0).GetComponent<Text> ().text = "" + frame.shot1Pins;
		trans.GetChild (1).GetComponent<Text> ().text = "" + frame.shot2Pins;
		trans.GetChild (2).GetComponent<Text> ().text = "" + frame.shot1Pins + frame.shot2Pins;
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
			timeOfOut = Time.time;
			Invoke ("StartLaneReset", timeBeforeScoreCount);
		}
	}
}
