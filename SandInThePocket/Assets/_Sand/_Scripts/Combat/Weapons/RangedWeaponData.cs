using Sand.Combat.Attacks;
using UnityEngine;

namespace Sand.Combat.Weapons {

	[CreateAssetMenu (menuName = "Combat/Ranged Weapon Data", fileName = "New Ranged Weapon Data")]
	public class RangedWeaponData : BaseWeaponData {

		public GameObject ProjectilePrefab;

		void OnValidate() {

			if (Combo != null && !Combo.ComboType.Equals (EComboWeaponType.Ranged)) {

				Combo = null;
				Debug.LogError("You must set the combo as Ranged");
			}
		}
	}
}