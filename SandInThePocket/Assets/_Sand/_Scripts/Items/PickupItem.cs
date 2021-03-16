using Sand.Player;
using Sand.Utils;
using UnityEngine;

namespace Sand.Items {

	[RequireComponent (typeof (Collider))]
	public class PickupItem : MonoBehaviour {

		public PickupItemData PickupData;
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
		}

		public void OnTriggerExit(Collider other) {

			onTriggerPlayer = null;
		}
	}
}