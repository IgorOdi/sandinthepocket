using Sand.Combat;
using Sand.Combat.Weapons;
using UnityEngine;

namespace Sand.Player {

	public class PlayerCombatActor : CombatActor {

		[SerializeField]
		private BaseWeaponController weaponController;

		protected override void OnSpawn() {

			base.OnSpawn ();
			weaponController = GetComponentInChildren<BaseWeaponController> ();
		}

		protected override void Update() {

			base.Update ();

			if (weaponController != null) {

				if (Input.GetKeyDown (KeyCode.Z)) weaponController.OnWeaponPress ();
				if (Input.GetKey (KeyCode.Z)) weaponController.OnWeaponHold ();
				if (Input.GetKeyUp (KeyCode.Z)) weaponController.OnWeaponRelease ();
			}
		}
	}
}