using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButton : MonoBehaviour {

	[SerializeField]
	GameObject actionGameObject;

	Animator animator;
	bool active;

	void Awake() {
		animator = GetComponentInChildren<Animator> ();
	}

	void OnTriggerEnter(Collider other) {
		active = !active;
		actionGameObject.GetComponent<ITriggerableAction> ().SetActive (active);
		animator.SetBool ("Active", active);
	}
}
