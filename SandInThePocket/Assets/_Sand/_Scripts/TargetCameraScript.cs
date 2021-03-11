using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

[RequireComponent (typeof (Camera))]
public class TargetCameraScript : MonoBehaviour {
	[SerializeField]
	Transform currentFocus;

	Transform followFocus;

	[SerializeField]
	Transform superNewFocus;

	bool isTweening;

	[SerializeField, Range (1f, 20f)]
	float offset = 15f;

	[SerializeField, Range (0f, 90f)]
	float startAngle = 45f;

	[SerializeField, Min (0f)]
	float focusRadius = 1f;

	[SerializeField, Range (0f, 1f)]
	float focusCentering = 0.5f;

	Vector3 focusPoint;

	Transform player;

	void Awake() {
		player = GameObject.Find ("Player").transform;
		followFocus = new GameObject ("followFocus").transform;
		focusPoint = currentFocus.position;
		ChangeAngle (startAngle, 0);
	}

	public void ChangeAngle(float newAngle, float tweenTime = 0, Ease easing = Ease.Linear) {
		transform.DORotate (new Vector3 (newAngle, 0, 0), tweenTime)
			.SetEase (easing);
	}

	public void ChangeOffset(float newOffset, float tweenTime = 0, Ease easing = Ease.Linear) {
		DOTween.To (() => offset, x => offset = x, newOffset, tweenTime)
			.SetEase (easing);
	}
	public Tween ChangePosition(Transform newFocus, float tweenTime = 0, Ease easing = Ease.Linear) {
		isTweening = true;
		return followFocus.DOMove (newFocus.position, tweenTime)
			.SetEase (easing)
			.OnComplete (() => {
				currentFocus = newFocus;
				isTweening = false;
			});
	}

	public Tween FocusOnPlayer(float tweenTime = 0, Ease easing = Ease.Linear) {
		return ChangePosition (player, tweenTime, easing);
	}

	[Button ("do move")]
	void changeTest() {
		ChangePosition (superNewFocus, 1, Ease.Linear);
	}

	void UpdateFocusPoint() {
		if (!isTweening) {
			followFocus.position = currentFocus.position;
		}
		Vector3 targetPoint = followFocus.position;
		if (focusRadius > 0f) {
			float distance = Vector3.Distance (targetPoint, focusPoint);
			float t = 1f;
			if (distance > 0.01f && focusCentering > 0f) {
				t = Mathf.Pow (1f - focusCentering, Time.deltaTime);
			}
			if (distance > focusRadius) {
				t = Mathf.Min (t, focusRadius / distance);
			}
			focusPoint = Vector3.Lerp (targetPoint, focusPoint, t);

		} else {

			focusPoint = targetPoint;
		}
	}

	void LateUpdate() {
		UpdateFocusPoint ();
		Vector3 lookDirection = transform.forward;
		transform.localPosition = focusPoint - lookDirection * offset;
	}
}
