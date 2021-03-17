using Sand.Player;
using Sand.Utils;
using UnityEngine;

namespace Sand.Items {

	[RequireComponent (typeof (Collider))]
	public abstract class PickupItem<T> : MonoBehaviour where T : PickupItemData {

		public T PickupData;
		protected virtual bool CollectOnCollide { get; set; } = true;
		private PlayerCombatActor onTriggerPlayer;

		public virtual void OnGetItem(PlayerCombatActor combatActor) { }

		private void Update() {

			if (Input.GetKeyDown (KeyCode.C) && onTriggerPlayer != null) {

				OnGetItem (onTriggerPlayer);
				Destroy (gameObject);
			}
		}

		public void OnTriggerEnter(Collider other) {

			other.TryGetComponent<PlayerCombatActor> (out onTriggerPlayer);

			if (CollectOnCollide && onTriggerPlayer != null) {

				OnGetItem (onTriggerPlayer);
			}
		}

		public void OnTriggerExit(Collider other) {

			onTriggerPlayer = null;
		}
	}
}