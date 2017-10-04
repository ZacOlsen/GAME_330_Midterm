using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneReset : MonoBehaviour {

	[SerializeField] private float moveUpHeight = 2f;
	private float startY;

	private List<GameObject> pins;
	private BoxCollider triggerBox;

	private bool topReached;

	private ScoreCalculator sc;
	private int pinsDowned;

	void Start () {

		startY = transform.position.y;

		pins = new List<GameObject> ();
		triggerBox = GetComponent<BoxCollider> ();
		triggerBox.enabled = false;

		sc = GameObject.Find ("End Zone").GetComponent<ScoreCalculator> ();
	}
	
	void FixedUpdate () {

		if (triggerBox.enabled) {

			if (!topReached) {

				float dist = startY + moveUpHeight - transform.position.y;

				transform.position = new Vector3 (transform.position.x, Mathf.Lerp (transform.position.y, 
					startY + moveUpHeight, .1f / dist), transform.position.z);

				if (transform.position.y + .1f >= startY + moveUpHeight) {
					topReached = true;
					RemoveDownedPins ();
				}

			} else {

				float dist = transform.position.y - startY;

				transform.position = new Vector3 (transform.position.x, Mathf.Lerp (transform.position.y, 
					startY, .05f / dist), transform.position.z);

				if (transform.position.y - .1f <= startY) {
					topReached = false;
					triggerBox.enabled = false;

					for (int i = 0; i < pins.Count; i++) {
						pins [i].GetComponent<Rigidbody> ().isKinematic = false;
					}

					sc.ReceivePinsDown (10 - pins.Count);
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

		for (int i = 0; i < downedPins.Length; i++) {
			if (!pins.Contains (downedPins [i])) {
				downedPins [i].GetComponent<Collider> ().isTrigger = true;
			}
		}
	}

	void OnTriggerEnter (Collider col) {

		if (col.CompareTag ("Pin")) {
			pins.Add (col.gameObject);
		}

		col.transform.rotation = Quaternion.identity;

		col.transform.parent = transform;
		col.GetComponent<Rigidbody> ().isKinematic = true;
	}
}
