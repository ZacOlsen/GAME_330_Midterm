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

	public void CalculateScore () {

	}

	public int GetScore () {
		return score;
	}
}
