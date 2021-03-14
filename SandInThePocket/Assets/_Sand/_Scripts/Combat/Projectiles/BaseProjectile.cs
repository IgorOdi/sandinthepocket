using Sand.Combat.Attacks;
using Sand.Pooling;
using Sand.Utils;
using UnityEngine;

namespace Sand.Combat.Projectiles {

	[RequireComponent (typeof (Rigidbody), typeof (Collider))]
	public class BaseProjectile : Damager<RangedAttackData> {

		protected Rigidbody rb;

		protected override void OnEnable() {

			rb = GetComponent<Rigidbody> ();
			this.RunDelayed (AttackData.TimingData.Duration, ReturnToPool);

			if (AttackData.MovingData.MoveMode == MoveMode.Speed) {

				rb.useGravity = false;
				rb.velocity = transform.forward * AttackData.MovingData.Speed;
			} else {

				rb.useGravity = true;
				rb.AddForce (AttackData.MovingData.Force);
			}
		}

		public void ReturnToPool() {

			PoolManager.AddToPool (AttackData.ProjectilePoolOverride, gameObject);
		}

		protected override void OnTriggerEnter(Collider other) {

			base.OnTriggerEnter (other);
			ReturnToPool ();
		}
	}
}