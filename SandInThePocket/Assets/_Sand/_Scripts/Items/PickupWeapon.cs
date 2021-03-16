using Sand.Combat.Weapons;
using Sand.Player;

namespace Sand.Items {

	public class PickupWeapon : PickupItem<PickupWeaponData> {

		protected override bool CollectOnCollide => false;

		public override void OnGetItem(PlayerCombatActor combatActor) {

			var weapon = Instantiate (PickupData.ControllerPrefab);

			BaseWeaponController controller = weapon.GetComponent<BaseWeaponController> ();
			controller.DropObject = PickupData.PickupPrefab;
			combatActor.GetWeapon (controller);
		}
	}
}