using System;
using Sand.Combat.Attacks;
using Sand.Pooling;
using Sand.Utils;
using UnityEngine;

namespace Sand.Combat {

	public partial class Damager : MonoBehaviour {

		public AttackData AttackData { get; protected set; }

		public Action OnHitEnable { get; set; }
		public Action OnHitDisable { get; set; }
		public Action<EAttackResult, CombatActor> OnHitSuccess { get; set; }
		public Action<bool, CombatActor> OnHitKill { get; set; }

		protected new Collider collider;
		private IDamageable lastHitEnemy;

		private void OnEnable() {

			ConfigureFromData (AttackData.ColliderBuildData);
			this.RunDelayed (AttackData.TimingData.Delay, EnableDamager);
			this.RunDelayed (AttackData.TimingData.Delay + AttackData.TimingData.Duration, DisableDamager);
		}

		private void EnableDamager() {

			lastHitEnemy = null;
			collider.enabled = true;
			OnHitEnable?.Invoke ();
		}

		private void DisableDamager() {

			collider.enabled = false;
			OnHitDisable?.Invoke ();

			if (lastHitEnemy == null)
				OnHitSuccess?.Invoke (EAttackResult.Miss, null);

			string colliderShape = GetColliderTypeString (AttackData.ColliderBuildData.ColliderBuildType);
			PoolManager.AddToPool (colliderShape, gameObject);
		}

		private void ConfigureFromData(ColliderBuildData buildData) {

			collider = GetComponent<Collider> ();

			if (buildData.ColliderBuildType == EColliderBuildType.Box) {
				var boxCollider = (BoxCollider) collider;
				boxCollider.size = buildData.Size;
				boxCollider.center = buildData.Offset;
				boxCollider.isTrigger = true;
			} else {
				var sphereCollider = (SphereCollider) collider;
				sphereCollider.radius = buildData.Radius;
				sphereCollider.center = buildData.Offset;
				sphereCollider.isTrigger = true;
			}

			collider.enabled = false;
		}

		private void OnTriggerEnter(Collider other) {

			if (other.gameObject.TryGetComponent<IDamageable> (out lastHitEnemy)) {

				lastHitEnemy.CauseDamage (AttackData,
					(atkResult, actor) => OnHitSuccess?.Invoke (atkResult, actor),
					(killed, actor) => OnHitKill?.Invoke (killed, actor)
				);
			}
		}
	}
}