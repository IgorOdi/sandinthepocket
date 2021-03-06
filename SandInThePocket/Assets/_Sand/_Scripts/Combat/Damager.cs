using System;
using Sand.Combat.Attacks;
using Sand.Utils;
using UnityEngine;

namespace Sand.Combat {

	public class Damager : MonoBehaviour {

		public AttackData AttackData { get; protected set; }

		public Action OnHitEnable { get; set; }
		public Action OnHitDisable { get; set; }
		public Action<bool> OnHitSuccess { get; set; }
		public Action<bool> OnHitKill { get; set; }

		private new Collider collider;
		private IDamageable lastHitEnemy;

		public void Initialize(AttackData context) {

			this.AttackData = context;
			collider = GetComponent<Collider> ();
			collider.enabled = false;

			this.RunDelayed (context.TimingData.Delay, EnableDamager);
			this.RunDelayed (context.TimingData.Delay + context.TimingData.Duration, DisableDamager);
		}

		private void EnableDamager() {

			lastHitEnemy = null;
			collider.enabled = true;
			OnHitEnable?.Invoke ();
		}

		private void DisableDamager() {

			collider.enabled = false;
			OnHitDisable?.Invoke ();

			if (lastHitEnemy == null) OnHitSuccess?.Invoke (false);

			Destroy (gameObject);
			//TODO: Convert Destroy to Pool;
		}

		private void OnTriggerEnter(Collider other) {

			if (other.gameObject.TryGetComponent<IDamageable> (out lastHitEnemy)) {

				lastHitEnemy.CauseDamage (AttackData,
					(sucess) => OnHitSuccess?.Invoke (sucess),
					(killed) => OnHitKill?.Invoke (killed)
				);
			}
		}
	}
}