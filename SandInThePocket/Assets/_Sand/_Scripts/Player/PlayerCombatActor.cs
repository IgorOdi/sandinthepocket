using Sand.Combat;
using Sand.Combat.Weapons;
using Sand.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sand.Player {

	public class PlayerCombatActor : CombatActor {

		[SerializeField, ReadOnly]
		private BaseWeaponController[] weaponControllers = new BaseWeaponController[2];
		private int currentWeaponIndex;

		public void GetWeapon(BaseWeaponController weaponController) {

			DropCurrentWeapon ();

			weaponController.transform.Reset (transform);
			weaponControllers[currentWeaponIndex] = weaponController;
			weaponControllers[currentWeaponIndex].OnCollect(this);
			weaponControllers[currentWeaponIndex].OnEquip ();
		}

		public void DropWeapon(int index) {

			if (weaponControllers[index] != null)
				weaponControllers[index].OnDrop ();
		}

		public void DropCurrentWeapon() {

			DropWeapon (currentWeaponIndex);
		}

		public void ChangeWeapon() {

			currentWeaponIndex = currentWeaponIndex == 0 ? 1 : 0;
			int notCurrentWeaponIndex = currentWeaponIndex == 0 ? 1 : 0;
			weaponControllers[currentWeaponIndex]?.OnEquip ();
			weaponControllers[notCurrentWeaponIndex]?.OnUnequip ();
		}

		protected override void Update() {

			base.Update ();

			if (Input.GetKeyDown (KeyCode.X)) ChangeWeapon ();

			if (weaponControllers[currentWeaponIndex] != null) {

				if (Input.GetKeyDown (KeyCode.Z)) weaponControllers[currentWeaponIndex].OnWeaponPress ();
				if (Input.GetKey (KeyCode.Z)) weaponControllers[currentWeaponIndex].OnWeaponHold ();
				if (Input.GetKeyUp (KeyCode.Z)) weaponControllers[currentWeaponIndex].OnWeaponRelease ();
			}
		}
	}
}