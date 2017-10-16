using System.Collections;
using System.Collections.Generic;

public class BowlingFrame {

	public int shot1Pins = -1;
	public int shot2Pins = -1;

	public readonly int frameNum = 0;

	public BowlingFrame next;

	public BowlingFrame (int frame) {
		frameNum = frame;
	}

	public string GetShot1 () {

		if (shot1Pins == 10) {
			return "X";
		}

		if (shot1Pins == -1) {
			return "";
		}

		return "" + shot1Pins;
	}

	public string GetShot2 () {

		if (shot2Pins + shot1Pins == 10) {
			return "/";
		}

		if (shot2Pins == -1) {
			return "";
		}

		return "" + shot2Pins;
	}

	public int CalculateScore () {

		if (shot1Pins == 10) {
			if(next == null){
				return 20;
			}

			if (next.shot1Pins != -1) {
				if (next.shot2Pins == -1) {
					
					if (next.next != null && next.next.shot1Pins != -1) {
						return 10 + next.shot1Pins + next.next.shot1Pins;
					}

				} else {

					return 10 + next.shot1Pins + next.shot2Pins;
				}

				return -1;
			}
		}

		if (shot1Pins + shot2Pins == 10) {
			if(next == null){
				return 15;
			}

			if (next.shot1Pins != -1) {
				return 10 + next.shot1Pins;
			}

			return -1;
		}

		if (shot2Pins == -1) {
			return shot1Pins;
		}

		return shot1Pins + shot2Pins;
	}
}
