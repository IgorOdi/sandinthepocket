using System;
using System.Collections.Generic;
using Sand.Combat.Attacks;
using Sand.Combat.Damaging;
using Sand.Utils;
using UnityEngine;

namespace Sand.Combat {

	[Serializable]
	public class ActorStats {

		public float Health;
	}

	[RequireComponent (typeof (Collider), typeof (Rigidbody))]
	public class CombatActor : MonoBehaviour, IDamageable {

		public ActorStats ActorStats;
		public List<Status> Statuses;

		public static event Action<CombatActor> OnActorGlobalSpawn;
		public static event Action<CombatActor, EAttackResult> OnActorGlobalHit;
		public static event Action<CombatActor> OnActorGlobalDeath;
		public static event Action<Status> OnGlobalAddStatus;
		public static event Action<Status, EAttackResult> OnGlobalStatusTick;
		public static event Action<Status> OnGlobalRemoveStatus;

		public Action<CombatActor> OnActorSpawn;
		public Action<CombatActor, EAttackResult> OnActorHit;
		public Action<CombatActor> OnActorDeath;
		public Action<Status> OnAddStatus;
		public Action<Status, EAttackResult> OnStatusTick;
		public Action<Status> OnRemoveStatus;

		void OnEnable() => OnSpawn ();
		protected virtual void OnSpawn() {

			OnActorGlobalSpawn?.Invoke (this);
			OnActorSpawn?.Invoke (this);
		}

		protected virtual void OnHit(DamagerData attackData, EAttackResult result) {

			PlayHitFX ();
			OnActorGlobalHit?.Invoke (this, result);
			OnActorHit?.Invoke (this, result);
			Debug.Log ($"Hit on {gameObject.name} from {attackData.User?.name}'s {attackData.Context.WeaponData.Name}");
		}

		protected virtual void OnTick(Status status, EAttackResult result) {

			PlayHitFX ();
			OnGlobalStatusTick?.Invoke (status, result);
			OnStatusTick?.Invoke (status, result);
			Debug.Log ($"{gameObject.name} took {status.DamagePerSecond} damage from {status.Type.ToString ()}");
		}

		protected virtual void OnDeath(DamagerData attackData) {

			PlayDeathFX ();
			OnActorGlobalDeath?.Invoke (this);
			OnActorDeath?.Invoke (this);
			Debug.Log ($"{gameObject.name} was killed by {attackData.User?.name}'s {attackData.Context.WeaponData.Name}");

			ReturnToPool ("");
		}

		protected virtual void OnTickDeath(Status status) {

			PlayDeathFX ();
			OnActorGlobalDeath?.Invoke (this);
			OnActorDeath?.Invoke (this);
			Debug.Log ($"{gameObject.name} was killed by {status.Type.ToString ()}");

			ReturnToPool ("");
		}

		protected virtual void ReturnToPool(string poolName) {

			//TODO: Convert to pool;
			Destroy (gameObject);
		}

		protected virtual void PlayHitFX() {

			//TODO: Personal Hit Visual and Sound FX and Animation goes here;
		}
		protected virtual void PlayDeathFX() {

			//TODO: Personal Hit Visual and Sound FX and Animation goes here;
		}

		protected void AddStatus(List<StatusData> statuses) {

			float randomResult = 0f;
			for (int i = 0; i < statuses.Count; i++) {

				randomResult = UnityEngine.Random.Range (0f, 1f);
				if (randomResult > statuses[i].Chance)
					continue;

				AddStatus (new Status (statuses[i]));
			}
		}

		protected void AddStatus(Status status) {

			for (int i = 0; i < Statuses.Count; i++) {

				if (Statuses[i].Type == status.Type) {

					if (Statuses[i].CurrentDuration <= status.Duration) {

						Statuses[i] = status;
					}

					return;
				}
			}

			//TODO: Loading the FX based on status type goes here;
			Statuses.Add (status);
			TakeDamageFromStatus (status);

			OnAddStatus?.Invoke (status);
			OnGlobalAddStatus?.Invoke (status);
		}

		protected void RemoveStatus(Status status) {

			Statuses.Remove (status);
			OnRemoveStatus?.Invoke (status);
			OnGlobalRemoveStatus?.Invoke (status);
		}

		protected void TakeDamageFromStatus(Status status) {

			if (!Statuses.Contains (status)) return;

			TickDamage (status, status.OnTickResult, status.OnKill);
			this.RunDelayed (1f, () => TakeDamageFromStatus (status));
		}

		protected void RemoveAllStatus() {

			for (int i = 0; i < Statuses.Count; i++) {

				RemoveStatus (Statuses[i]);
			}
		}

		protected virtual void Update() {

			for (int i = 0; i < Statuses.Count; i++) {

				Statuses[i].CurrentDuration -= Time.deltaTime;
				if (Statuses[i].CurrentDuration <= 0) RemoveStatus (Statuses[i]);
			}
		}

		public void TickDamage(Status status, Action<EAttackResult, CombatActor> tickResult, Action<bool, CombatActor> killResult) {

			ActorStats.Health -= status.DamagePerSecond;
			tickResult?.Invoke (EAttackResult.Success, this);
			OnTick (status, EAttackResult.Success);

			bool killSucess = ActorStats.Health <= 0;

			killResult?.Invoke (killSucess, this);
			if (killSucess) OnTickDeath (status);
		}

		public void CauseDamage(DamagerData attackData, Action<EAttackResult, CombatActor> atkResult, Action<bool, CombatActor> killed) {

			ActorStats.Health -= attackData.FullDamage;
			atkResult?.Invoke (EAttackResult.Success, this);
			OnHit (attackData, EAttackResult.Success);

			AddStatus (attackData.Statuses);

			bool killSucess = ActorStats.Health <= 0;

			killed?.Invoke (killSucess, this);
			if (killSucess) OnDeath (attackData);
		}
	}
}