using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

	[SerializeField] private float[] positions;
	private Image selection;
	private MenuChoice choice;

	private enum MenuChoice {
		PLAY,
		OPTIONS,
		QUIT
	}

	void Start () {

		selection = GameObject.Find ("Selection Tool").GetComponent<Image> ();
	}

	void Update () {

		if (FigmentInput.GetButtonDown (FigmentInput.FigmentButton.RightButton)) {
			choice++;
			if ((int)choice > 2) {
				choice = MenuChoice.PLAY;
			}

			selection.transform.localPosition = new Vector3 (selection.transform.localPosition.x, positions[(int)choice], selection.transform.localPosition.z);
		}

		if (FigmentInput.GetButtonDown (FigmentInput.FigmentButton.LeftButton)) {
			choice--;
			if ((int)choice < 0) {
				choice = MenuChoice.QUIT;
			}

			selection.transform.localPosition = new Vector3 (selection.transform.localPosition.x, positions[(int)choice], selection.transform.localPosition.z);
		}

		if (FigmentInput.GetButtonDown (FigmentInput.FigmentButton.ActionButton)) {

			switch (choice) {

			case MenuChoice.PLAY:
				SceneManager.LoadScene (1);
				break;
	
			case MenuChoice.OPTIONS:
				break;
			
			default:
				Application.Quit ();
				break;
			}
		}
	}
}
