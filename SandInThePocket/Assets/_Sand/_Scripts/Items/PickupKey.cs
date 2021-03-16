using Sand.Player;

namespace Sand.Items {

	public class PickupKey : PickupItem<PickupKeyData> {

		public override void OnGetItem(PlayerCombatActor actor) {

			actor.AddKey (PickupData.KeyString);
			Destroy (gameObject);
		}
	}
}