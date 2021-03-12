using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightButton : MonoBehaviour {

	[SerializeField]
	MonoBehaviour action;

	[SerializeField]
	Material activatedMaterial, deactivatedMaterial;

	[SerializeField]
	GameObject buttonChild;

	bool active;

	void OnTriggerEnter(Collider other) {
		active = true;
		((ITriggerableAction) action).SetActive (active);
        buttonChild.GetComponent<MeshRenderer> ().material = activatedMaterial;
        buttonChild.transform.localPosition = new Vector3 (0, -0.1f, 0);
	}

	void OnTriggerExit(Collider other) {
		active = false;
		((ITriggerableAction) action).SetActive (active);
        buttonChild.GetComponent<MeshRenderer> ().material = deactivatedMaterial;
        buttonChild.transform.localPosition = new Vector3 (0, 0, 0);
	}
}
