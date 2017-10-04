using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneReset : MonoBehaviour {

	[SerializeField] private float moveUpHeight = 2f;
	private float startY;

	private List<GameObject> pins;
	private BoxCollider triggerBox;

	private bool topReached;

	void Start () {

		startY = transform.position.y;

		pins = new List<GameObject> ();
		triggerBox = GetComponent<BoxCollider> ();
		triggerBox.enabled = false;
	}
	
	void FixedUpdate () {

		if (triggerBox.enabled) {

			if (!topReached) {
				transform.position = new Vector3 (transform.position.x, Mathf.Lerp (transform.position.y, startY + moveUpHeight, .1f), transform.position.z);

				if (transform.position.y + .1f >= startY + moveUpHeight) {
					topReached = true;
					RemoveDownedPins ();
				}

			} else {
				transform.position = new Vector3 (transform.position.x, Mathf.Lerp (transform.position.y, startY, .05f), transform.position.z);

				if (transform.position.y + .1f <= startY) {
					topReached = false;
					triggerBox.enabled = false;

					for (int i = 0; i < pins.Count; i++) {
						pins [i].GetComponent<Rigidbody> ().isKinematic = false;
					}
				}
			}
		}
	}

	public void ClearLane () {

		pins.Clear ();
		triggerBox.enabled = true;
	}

	private void RemoveDownedPins () {

		GameObject[] downedPins = GameObject.FindGameObjectsWithTag ("Pin");

		Debug.Log (pins.Count);

		for (int i = 0; i < downedPins.Length; i++) {
			if (!pins.Contains (downedPins [i])) {
				downedPins [i].GetComponent<Collider> ().enabled = false;
			}
		}
	}

	void OnTriggerEnter (Collider col) {

		if (col.CompareTag ("Pin")) {
			pins.Add (col.gameObject);
		}

		col.transform.parent = transform;
		col.GetComponent<Rigidbody> ().isKinematic = true;
	}
}
