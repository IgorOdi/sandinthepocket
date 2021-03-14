using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTriggerAction : MonoBehaviour, ITriggerableAction {
	bool rotating;
	float angle;

	void Update() {
		if (rotating) {
			transform.eulerAngles = Vector3.up * angle;
			angle += 50 * Time.deltaTime;
		}
	}

	public void SetActive(bool active) {
		rotating = active;
	}
}
