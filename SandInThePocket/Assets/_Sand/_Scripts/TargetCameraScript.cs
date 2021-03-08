using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Camera))]
public class TargetCameraScript : MonoBehaviour {
	[SerializeField]
	Transform focus = default;

	[SerializeField, Range (1f, 20f)]
	float distance = 15f;

	[SerializeField, Min (0f)]
	float focusRadius = 1f;

	Vector3 focusPoint;

	void Awake() {
		focusPoint = focus.position;

	}

	void UpdateFocusPoint() {
		Vector3 targetPoint = focus.position;
		if (focusRadius > 0f) {
			float distance = Vector3.Distance (targetPoint, focusPoint);
			if (distance > focusRadius) {
				focusPoint = Vector3.Lerp (
					targetPoint, focusPoint, focusRadius / distance
				);
			}
		} else {

			focusPoint = targetPoint;
		}
	}

	void LateUpdate() {
		UpdateFocusPoint ();
		Vector3 lookDirection = transform.forward;
		transform.localPosition = focusPoint - lookDirection * distance;
	}

	void Start() {

	}

	void Update() {
		LateUpdate ();
	}
}
