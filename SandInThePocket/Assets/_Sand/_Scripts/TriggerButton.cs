using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerableAction {
	void SetActive(bool active);
}

public class TriggerButton : MonoBehaviour {

	[SerializeField]
	MonoBehaviour action;

	[SerializeField]
	Material activatedMaterial, deactivatedMaterial;

	[SerializeField]
	GameObject buttonChild;

	bool active;

	void OnTriggerEnter(Collider other) {
		active = !active;

		((ITriggerableAction) action).SetActive (active);

		if (active) {
			buttonChild.GetComponent<MeshRenderer> ().material = activatedMaterial;
			buttonChild.transform.localPosition = new Vector3 (0, -0.1f, 0);
		} else {
			buttonChild.GetComponent<MeshRenderer> ().material = deactivatedMaterial;
			buttonChild.transform.localPosition = new Vector3 (0, 0, 0);
		}
	}
}
