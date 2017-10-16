using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour {

	private Text text;

	void Start () {
		text = GameObject.Find ("Text").GetComponent<Text> ();
		text.text = ScoreCalculator.GetTotalScore () + "";
	}
	
	void Update () {

		if (FigmentInput.GetButtonDown (FigmentInput.FigmentButton.ActionButton) ||
			FigmentInput.GetButtonDown (FigmentInput.FigmentButton.LeftButton) ||
			FigmentInput.GetButtonDown (FigmentInput.FigmentButton.RightButton)) {

			SceneManager.LoadScene (0);
		}
	}
}
