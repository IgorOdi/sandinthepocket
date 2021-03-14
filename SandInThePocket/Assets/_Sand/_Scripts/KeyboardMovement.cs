using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMovement : MonoBehaviour {

	[SerializeField, Range (0f, 100f)]
	float maxAcceleration = 100;

	[SerializeField, Range (0f, 100f)]
	float maxSpeed = 10;

	[SerializeField, Range (0.1f, 2000f)]
	float rollingForce = 1000f;

	[SerializeField, Range (0.1f, 2f)]
	float rollingDuration = .5f;

	[SerializeField, Range (0.1f, 2f)]
	float rollingCooldown = .25f;

	// Movement
	Rigidbody body;
	Vector2 inputDirection;
	Vector3 velocity;

	// Rolling
	bool rolling;
	bool canRoll = true;

	void Awake() {
		body = GetComponent<Rigidbody> ();
	}

	void Update() {
		inputDirection.x = Input.GetAxis ("Horizontal");
		inputDirection.y = Input.GetAxis ("Vertical");
		inputDirection = Vector2.ClampMagnitude (inputDirection, 1);

		if (Input.GetAxisRaw ("Roll") != 0 && canRoll) {
			rolling = true;
			canRoll = false;
			StartCoroutine ("WaitStopRoll");
		}
	}

	void FixedUpdate() {

		velocity = body.velocity;

		if (rolling) {
			velocity = Roll ();
		} else {
			velocity = Walk();
		}

		body.velocity = velocity;

		RotatePlayer();
	}

	Vector3 Roll() {
		return transform.forward * rollingForce * Time.deltaTime;
	}

	Vector3 Walk() {
		Vector3 direction = new Vector3 (inputDirection.x, 0, inputDirection.y);
		Vector3 vel = velocity;
		Vector3 desiredVelocity = direction * maxSpeed;

		float maxSpeedChange = maxAcceleration * Time.deltaTime;
		vel.x = Mathf.MoveTowards (vel.x, desiredVelocity.x, maxSpeedChange);
		vel.z = Mathf.MoveTowards (vel.z, desiredVelocity.z, maxSpeedChange);
		return vel;
	}

	void RotatePlayer() {
		if (inputDirection.magnitude > 0.1) {
			float inputAngle = Mathf.Atan2 (inputDirection.x, inputDirection.y) * Mathf.Rad2Deg;
			transform.eulerAngles = Vector3.up * inputAngle;
		}
	}

	IEnumerator WaitStopRoll() {
		yield return new WaitForSeconds (rollingDuration);
		rolling = false;
		yield return new WaitForSeconds (rollingCooldown);
		canRoll = true;
	}
}
