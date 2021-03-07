using System;
using Sand.Combat.Attacks;
using Sand.Utils;
using UnityEngine;

namespace Sand.Combat {

	public class Damager : MonoBehaviour {

		public AttackData AttackData { get; protected set; }

		public Action OnHitEnable { get; set; }
		public Action OnHitDisable { get; set; }
		public Action<EAttackResult, CombatActor> OnHitSuccess { get; set; }
		public Action<bool, CombatActor> OnHitKill { get; set; }

		private new Collider collider;
		private IDamageable lastHitEnemy;

		public void Configure(AttackData context) {

			this.AttackData = context;
			ConfigureFromData (context.ColliderBuildData);

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

			if (lastHitEnemy == null) OnHitSuccess?.Invoke (EAttackResult.Miss, null);

			//TODO: Convert Destroy to Pool;
			Destroy (gameObject);
		}

		private void ConfigureFromData(ColliderBuildData buildData) {

			if (buildData.ColliderBuildType == EColliderBuildType.Box) {
				var boxCollider = gameObject.AddComponent<BoxCollider> ();
				boxCollider.size = buildData.Size;
				boxCollider.center = buildData.Offset;
				boxCollider.isTrigger = true;
			} else {
				var sphereCollider = gameObject.AddComponent<SphereCollider> ();
				sphereCollider.radius = buildData.Radius;
				sphereCollider.center = buildData.Offset;
				sphereCollider.isTrigger = true;
			}

			collider = GetComponent<Collider> ();
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