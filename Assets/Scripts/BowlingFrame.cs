using System.Collections;
using System.Collections.Generic;

public class BowlingFrame {

	public int shot1Pins = 0;
	public int shot2Pins = 0;

	public readonly int frameNum = 0;

	public BowlingFrame next;
	private int score = 0;

	public BowlingFrame (int frame) {
		frameNum = frame;
	}

	public string GetShot1 () {

		if (shot1Pins == 10) {
			return "X";
		}

		return "" + shot1Pins;
	}

	public string GetShot2 () {

		if (shot2Pins + shot1Pins == 10) {
			return "/";
		}

		return "" + shot2Pins;
	}

	public int CalculateScore () {
		return shot1Pins + shot2Pins;
	}

	public int GetScore () {
		return score;
	}
}
