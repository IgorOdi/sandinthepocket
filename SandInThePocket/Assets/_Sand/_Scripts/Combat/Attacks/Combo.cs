using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sand.Combat.Attacks {

	[CreateAssetMenu (menuName = "Combat/Melee Combo", fileName = "New Melee Combo")]
	public class Combo : ScriptableObject {

		public EComboWeaponType ComboType;

		[ShowIf ("ComboType", EComboWeaponType.Melee)]
		public List<MeleeAttackData> MeleeAttacks;
		[ShowIf ("ComboType", EComboWeaponType.Ranged)]
		public List<RangedAttackData> RangedAttacks;

		public BaseAttackData GetAttack(int index) {

			if (ComboType == EComboWeaponType.Melee) {

				return MeleeAttacks[index];
			} else {

				return RangedAttacks[index];
			}
		}
	}
}