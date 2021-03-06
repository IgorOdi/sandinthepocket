using System;
using Sand.Combat.Attacks;
using Sand.Utils;
using UnityEngine;

namespace Sand.Combat {

	public class Damager : MonoBehaviour {

		public AttackData AttackData { get; private set; }

		public Action OnHitUnleash { get; set; }
		public Action OnHitDisable { get; set; }
		public Action OnHitSuccess { get; set; }
		public Action OnHitFail { get; set; }
		public Action OnHitKill { get; set; }

		private new Collider collider;
		private IDamageable lastHitEnemy;

		public void Initialize(AttackData context) {

			this.AttackData = context;
			collider = GetComponent<Collider> ();
			collider.enabled = false;

			this.RunDelayed (context.Delay, UnleashDamager);
			this.RunDelayed (context.Delay + context.Duration, DisableDamager);
			//TODO: Convert Destroy to Pool;
		}

		private void UnleashDamager() {

			collider.enabled = true;
			OnHitUnleash?.Invoke ();
		}

		private void DisableDamager() {

			collider.enabled = false;
			DestroyImmediate (gameObject);
		}

		private void OnTriggerEnter(Collider other) {

			if (other.gameObject.TryGetComponent<IDamageable> (out lastHitEnemy)) {

				lastHitEnemy.CauseDamage (AttackData, (sucess) => {

					if (sucess) OnHitSuccess?.Invoke ();
					else OnHitFail?.Invoke ();
				}, (killed) => {

					if (killed) OnHitKill?.Invoke ();
				});
			}
		}
	}
}