using Sand.Combat.Weapons;
using Sand.Player;

namespace Sand.Items {

	public class PickupWeapon : PickupItem {

		public override void OnGetItem(PlayerCombatActor combatActor) {

			var weapon = Instantiate (((PickupWeaponData) PickupData).WeaponPrefab);

			BaseWeaponController controller = weapon.GetComponent<BaseWeaponController> ();
			controller.DropObject = PickupData.PickupPrefab;
			combatActor.GetWeapon (controller);
		}
	}
}