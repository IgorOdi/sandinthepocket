﻿using Sand.Combat.Attacks;
using Sand.Utils;
using UnityEngine;

namespace Sand.Combat.Damaging {

	[RequireComponent (typeof (Rigidbody), typeof (Collider))]
	public partial class RangedDamager : Damager {

		protected new Rigidbody rigidbody;

		protected void Awake() {

			rigidbody = GetComponent<Rigidbody> ();
		}

		public void Initialize(DamagerData data, float duration, MovingData movingData) {

			Data = data;
			OnStart ();
			this.RunDelayed (duration, OnEnd);

			if (movingData.MoveMode == MoveMode.Speed) {

				rigidbody.useGravity = false;
				rigidbody.velocity = transform.forward * movingData.Speed;
			} else {

				rigidbody.useGravity = true;
				rigidbody.AddForce (movingData.Force);
			}
		}

		protected override void OnTriggerEnter(Collider other) {

			base.OnTriggerEnter (other);
			OnEnd ();
		}
	}
}