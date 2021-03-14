using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightButton : MonoBehaviour {

	[SerializeField]
	GameObject actionGameObject;

	Animator animator;

	void Awake() {
		animator = GetComponentInChildren<Animator> ();
	}

	void OnTriggerEnter(Collider other) {
		SetActive(true);
	}

	void OnTriggerExit(Collider other) {
		SetActive(false);
	}

	void SetActive(bool active) {
		actionGameObject.GetComponent<ITriggerableAction> ().SetActive (active);
		animator.SetBool ("Active", active);
	}
}
