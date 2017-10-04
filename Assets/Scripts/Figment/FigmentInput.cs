using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigmentInput : MonoBehaviour {

    public enum FigmentButton
    {
        LeftButton,
        RightButton,
        ActionButton
    }

    public delegate void ButtonEvent(FigmentButton buttonType);

    public static event ButtonEvent OnButtonDown;
    public static event ButtonEvent OnButtonHold;
    public static event ButtonEvent OnButtonUp;

    static bool[] FigmentButtonPressed;
    static bool[] FigmentButtonPressedLastFrame;

    // Use this for initialization
    void Start () {
        FigmentButtonPressed = new bool[System.Enum.GetValues(typeof(FigmentButton)).Length];
        FigmentButtonPressedLastFrame = new bool[System.Enum.GetValues(typeof(FigmentButton)).Length];
    }
	
	// Update is called once per frame
	void Update () {
        LaunchButtonEventsFromKeyboard(FigmentButton.LeftButton, KeyCode.LeftArrow);
        LaunchButtonEventsFromKeyboard(FigmentButton.RightButton, KeyCode.RightArrow);
        LaunchButtonEventsFromKeyboard(FigmentButton.ActionButton, KeyCode.Space);

        UpdateButtonStateFromKeyboard(FigmentButton.LeftButton, KeyCode.LeftArrow);
        UpdateButtonStateFromKeyboard(FigmentButton.RightButton, KeyCode.RightArrow);
        UpdateButtonStateFromKeyboard(FigmentButton.ActionButton, KeyCode.Space);
    }

    void UpdateButtonStateFromKeyboard(FigmentButton buttonType, KeyCode keyboardKey)
    {
        FigmentButtonPressedLastFrame[(int)buttonType] = FigmentButtonPressed[(int)buttonType];
        FigmentButtonPressed[(int)buttonType] = Input.GetKey(keyboardKey);
    }

    void LaunchButtonEventsFromKeyboard(FigmentButton buttonType, KeyCode keyboardKey)
    {
        if (Input.GetKeyDown(keyboardKey))
        {
            if(OnButtonDown != null)
            {
                OnButtonDown(buttonType);
            }
        }

        if(Input.GetKey(keyboardKey))
        {
            if (OnButtonHold != null)
            {
                OnButtonHold(buttonType);
            }
        }

        if (Input.GetKeyUp(keyboardKey))
        {
            if (OnButtonUp != null)
            {
                OnButtonUp(buttonType);
            }
        }
    }

    public static bool GetButton(FigmentButton buttonType)
    {
        return FigmentButtonPressed[(int)buttonType];
    }

    public static bool GetButtonDown(FigmentButton buttonType)
    {
        return FigmentButtonPressed[(int)buttonType] && !FigmentButtonPressedLastFrame[(int)buttonType];
    }

    public static bool GetButtonUp(FigmentButton buttonType)
    {
        return !FigmentButtonPressed[(int)buttonType] && FigmentButtonPressedLastFrame[(int)buttonType];
    }
}
