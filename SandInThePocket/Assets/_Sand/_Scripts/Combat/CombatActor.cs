using System;
using System.Collections.Generic;
using Sand.Combat.Attacks;
using UnityEngine;

namespace Sand.Combat {

	[Serializable]
	public class ActorStats {

		public float Health;
	}

	public class CombatActor : MonoBehaviour, IDamageable {

		public ActorStats ActorStats;
		public List<Status> Statuses;

		public static event Action OnActorGlobalSpawn;
		public static event Action OnActorGlobalHit;
		public static event Action OnActorGlobalDeath;
		public static event Action<Status> OnGlobalAddStatus;
		public static event Action<Status> OnGlobalRemoveStatus;

		public Action OnActorSpawn;
		public Action OnActorHit;
		public Action OnActorDeath;
		public Action<Status> OnAddStatus;
		public Action<Status> OnRemoveStatus;

		void OnEnable() => OnSpawn ();
		protected virtual void OnSpawn() {

			OnActorGlobalSpawn?.Invoke ();
			OnActorSpawn?.Invoke ();
		}

		protected virtual void OnHit() {

			//TODO: Personal Hit Visual and Sound FX and Animation goes here;
			Debug.Log ($"Hit on {gameObject.name}");
			OnActorGlobalHit?.Invoke ();
			OnActorHit?.Invoke ();
		}

		protected virtual void OnDeath() {

			//TODO: Personal Hit Visual and Sound FX and Animation goes here;
			Debug.Log ($"Killed {gameObject.name}");
			OnActorGlobalDeath?.Invoke ();
			OnActorDeath?.Invoke ();

			//TODO: Conver to pool;
			Destroy (gameObject);
		}

		protected void AddStatus(Status status) {

			for (int i = 0; i < Statuses.Count; i++) {

				if (Statuses[i].Type == status.Type) {

					if (Statuses[i].CurrentDuration <= status.TotalDuration) {

						Statuses[i] = status;
					}

					return;
				}
			}

			//TODO: Loading the FX based on status type goes here;
			Statuses.Add (status);
			OnAddStatus?.Invoke (status);
			OnGlobalAddStatus?.Invoke (status);
		}

		protected void RemoveStatus(Status status) {

			Statuses.Remove (status);
			OnRemoveStatus?.Invoke (status);
			OnGlobalRemoveStatus?.Invoke (status);
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

		public void CauseDamage(AttackData attackData, Action<bool> success, Action<bool> killed) {

			ActorStats.Health -= attackData.GetFullDamage ();

			for (int i = 0; i < attackData.DamageData.StatusData.Count; i++) {

				float randomResult = UnityEngine.Random.Range (0f, 1f);
				if (randomResult > attackData.DamageData.StatusData[i].Chance)
					continue;

				AddStatus (new Status (attackData.DamageData.StatusData[i]));
			}

			bool hitSuccess = true;
			bool killSucess = ActorStats.Health <= 0;

			success?.Invoke (hitSuccess);
			killed?.Invoke (killSucess);

			if (hitSuccess) OnHit ();
			if (killSucess) OnDeath ();
		}
	}
}