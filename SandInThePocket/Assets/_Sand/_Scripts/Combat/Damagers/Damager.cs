using System;
using Sand.Pooling;
using UnityEngine;

namespace Sand.Combat.Damaging {

	public abstract partial class Damager : MonoBehaviour {

		public DamagerData Data { get; protected set; }
		public string PoolOrigin { get; protected set; }

		public Action OnHitEnable { get; set; }
		public Action OnHitDisable { get; set; }
		public Action<EAttackResult, CombatActor> OnHitSuccess { get; set; }
		public Action<bool, CombatActor> OnHitKill { get; set; }

		protected new Collider collider;
		protected IDamageable lastHitEnemy;

		public virtual void Initialize(DamagerData data, string poolOrigin) {

			Data = data;
			PoolOrigin = poolOrigin;
		}

		protected virtual void OnStart() {

			lastHitEnemy = null;
			OnHitEnable?.Invoke ();
		}

		protected virtual void OnEnd() {

			OnHitDisable?.Invoke ();

			if (lastHitEnemy == null)
				OnHitSuccess?.Invoke (EAttackResult.Miss, null);

			PoolManager.AddToPool (PoolOrigin, gameObject);
		}

		protected virtual void OnTriggerEnter(Collider other) {

			if (other.gameObject.TryGetComponent<IDamageable> (out lastHitEnemy)) {

				lastHitEnemy.CauseDamage (Data,
					(atkResult, actor) => OnHitSuccess?.Invoke (atkResult, actor),
					(killed, actor) => OnHitKill?.Invoke (killed, actor)
				);
			}
		}
	}
}