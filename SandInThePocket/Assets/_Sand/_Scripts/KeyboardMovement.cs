using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMovement : MonoBehaviour {

	[SerializeField, Range (0, 100)]
	float maxAcceleration = 100;

	[SerializeField, Range (0, 100)]
	float maxSpeed = 10;

	Rigidbody rigidbody;
	Vector2 inputDirection;
	Vector3 velocity;

	void Awake() {
		rigidbody = GetComponent<Rigidbody> ();
	}

	void Update() {
		inputDirection.x = Input.GetAxis ("Horizontal");
		inputDirection.y = Input.GetAxis ("Vertical");
		inputDirection = Vector2.ClampMagnitude (inputDirection, 1);
	}

	void FixedUpdate() {
		Vector3 desiredVelocity = new Vector3 (inputDirection.x, 0, inputDirection.y) * maxSpeed;

		velocity = rigidbody.velocity;

		float maxSpeedChange = maxAcceleration * Time.deltaTime;
		velocity.x = Mathf.MoveTowards (velocity.x, desiredVelocity.x, maxSpeedChange);
		velocity.z = Mathf.MoveTowards (velocity.z, desiredVelocity.z, maxSpeedChange);

		rigidbody.velocity = velocity;

		if (inputDirection.magnitude > 0.1) {
			float inputAngle = Mathf.Atan2 (inputDirection.x, inputDirection.y) * Mathf.Rad2Deg;
			transform.eulerAngles = Vector3.up * inputAngle;
		}
	}
}
