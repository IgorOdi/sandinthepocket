using System;
using Sand.Combat.Attacks;
using Sand.Pooling;
using Sand.Utils;
using UnityEngine;

namespace Sand.Combat {

	public abstract class Damager<T> : MonoBehaviour where T : BaseAttackData {

		public T AttackData { get; set; }

		public Action OnHitEnable { get; set; }
		public Action OnHitDisable { get; set; }
		public Action<EAttackResult, CombatActor> OnHitSuccess { get; set; }
		public Action<bool, CombatActor> OnHitKill { get; set; }

		protected virtual string PoolName { get; }

		protected new Collider collider;
		protected IDamageable lastHitEnemy;

		protected virtual void OnEnable() {

			this.RunDelayed (AttackData.TimingData.Delay, EnableDamager);
			this.RunDelayed (AttackData.TimingData.Delay + AttackData.TimingData.Duration, DisableDamager);
		}

		protected virtual void EnableDamager() {

			lastHitEnemy = null;
			collider.enabled = true;
			OnHitEnable?.Invoke ();
		}

		protected virtual void DisableDamager() {

			collider.enabled = false;
			OnHitDisable?.Invoke ();

			if (lastHitEnemy == null)
				OnHitSuccess?.Invoke (EAttackResult.Miss, null);

			PoolManager.AddToPool (PoolName, gameObject);
		}

		protected virtual void OnTriggerEnter(Collider other) {

			if (other.gameObject.TryGetComponent<IDamageable> (out lastHitEnemy)) {

				lastHitEnemy.CauseDamage (AttackData,
					(atkResult, actor) => OnHitSuccess?.Invoke (atkResult, actor),
					(killed, actor) => OnHitKill?.Invoke (killed, actor)
				);
			}
		}
	}
}