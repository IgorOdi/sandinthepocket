using Sand.Combat.Attacks;
using UnityEngine;

namespace Sand.Combat.Weapons {

	[CreateAssetMenu (menuName = "Combat/Ranged Weapon Data", fileName = "New Ranged Weapon Data")]
	public class RangedWeaponData : BaseWeaponData {

		public string ProjectilePool;

		void OnValidate() {

			if (Combo != null && !Combo.ComboType.Equals (ComboWeaponType.Ranged)) {

				Combo = null;
				Debug.LogError("You must set the combo as Ranged");
			}
		}
	}
}