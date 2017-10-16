using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

	[SerializeField] private float[] positions = null;
	private Image selection;
	private MenuChoice choice;

	[SerializeField] private GameObject main = null;
	[SerializeField] private GameObject options = null;
	private bool mainMenu = true;

	private static float volumeLevel = 1;
	[SerializeField] private float maxVolPos = 0;
	[SerializeField] private float minVolPos = 0;
	[SerializeField] private GameObject slider = null;
	[SerializeField] private GameObject oSelect = null;
	private OptionsChoice oChoice;
	private bool volSelected;
	[SerializeField] private float[] oPos = null;

	private enum MenuChoice {
		PLAY,
		OPTIONS,
		QUIT
	}

	private enum OptionsChoice {
		VOLUME,
		BACK
	}

	void Start () {

		selection = GameObject.Find ("Selection Tool").GetComponent<Image> ();
	}

	void Update () {

		if (mainMenu) {
			
			if (FigmentInput.GetButtonDown (FigmentInput.FigmentButton.RightButton)) {
				choice++;
				if ((int)choice > 2) {
					choice = MenuChoice.PLAY;
				}

				selection.transform.localPosition = new Vector3 (positions [(int)choice], 
					selection.transform.localPosition.y, selection.transform.localPosition.z);
			}

			if (FigmentInput.GetButtonDown (FigmentInput.FigmentButton.LeftButton)) {
				choice--;
				if ((int)choice < 0) {
					choice = MenuChoice.QUIT;
				}

				selection.transform.localPosition = new Vector3 (positions [(int)choice], 
					selection.transform.localPosition.y, selection.transform.localPosition.z);
			}

			if (FigmentInput.GetButtonDown (FigmentInput.FigmentButton.ActionButton)) {

				switch (choice) {

				case MenuChoice.PLAY:
					SceneManager.LoadScene (1);
					break;
	
				case MenuChoice.OPTIONS:
					main.SetActive (false);
					options.SetActive (true);
					mainMenu = false;
					break;
			
				default:
					Application.Quit ();
					break;
				}
			}
		
		} else {

			if (FigmentInput.GetButtonDown (FigmentInput.FigmentButton.RightButton)) {

				if (volSelected) {
					slider.transform.localPosition = new Vector3 (Mathf.Clamp (slider.transform.localPosition.x + 15,
						minVolPos, maxVolPos), slider.transform.localPosition.y, slider.transform.localPosition.z);

					volumeLevel = (slider.transform.localPosition.x - minVolPos) / (maxVolPos - minVolPos);

				} else {
					oChoice++;
					if ((int)oChoice > 1) {
						oChoice = OptionsChoice.VOLUME;
					}

					oSelect.transform.localPosition = new Vector3 (0, oPos [(int)oChoice], 0);
				}
			}

			if (FigmentInput.GetButtonDown (FigmentInput.FigmentButton.LeftButton)) {

				if (volSelected) {
					slider.transform.localPosition = new Vector3 (Mathf.Clamp (slider.transform.localPosition.x - 15,
						minVolPos, maxVolPos), slider.transform.localPosition.y, slider.transform.localPosition.z);
					
					volumeLevel = (slider.transform.localPosition.x - minVolPos) / (maxVolPos - minVolPos);

				} else {
					oChoice--;
					if ((int)oChoice < 0) {
						oChoice = OptionsChoice.BACK;
					}

					oSelect.transform.localPosition = new Vector3 (0, oPos [(int)oChoice], 0);
				}
			}

			if (FigmentInput.GetButtonDown (FigmentInput.FigmentButton.ActionButton)) {

				switch (oChoice) {
				case OptionsChoice.VOLUME:
					volSelected = !volSelected;
					break;
				
				default:
					main.SetActive (true);
					options.SetActive (false);
					mainMenu = true;
					break;
				}
			}
		}
	}

	public static float GetVolumeLevel () {
		return volumeLevel;
	}
}
