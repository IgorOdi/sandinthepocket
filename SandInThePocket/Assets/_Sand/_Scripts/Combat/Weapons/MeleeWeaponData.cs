using Sand.Combat.Attacks;
using UnityEngine;

namespace Sand.Combat.Weapons {

	[CreateAssetMenu (menuName = "Combat/Melee Weapon Data", fileName = "New Melee Weapon Data")]
	public class MeleeWeaponData : BaseWeaponData {

		void OnValidate() {

			if (Combo != null && !Combo.ComboType.Equals (EComboWeaponType.Melee)) {

				Combo = null;
				Debug.LogError("You must set the combo as Melee");
			}
		}
	}
}