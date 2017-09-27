using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamplePlayerMovement : MonoBehaviour {

    public float TurnSpeed = 120.0f;
    public float MoveSpeed = 8.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (FigmentInput.GetButton(FigmentInput.FigmentButton.LeftButton))
        {
            transform.Rotate(Vector3.up, -TurnSpeed * Time.deltaTime);
        }
        else if (FigmentInput.GetButton(FigmentInput.FigmentButton.RightButton))
        {
            transform.Rotate(Vector3.up, TurnSpeed * Time.deltaTime);
        }

        if (FigmentInput.GetButton(FigmentInput.FigmentButton.ActionButton))
        {
            transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
        }
    }
}
