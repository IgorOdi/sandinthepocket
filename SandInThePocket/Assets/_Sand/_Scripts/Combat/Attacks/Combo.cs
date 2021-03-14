using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sand.Combat.Attacks {

	public enum ComboWeaponType {

		Melee,
		Ranged
	}

	[CreateAssetMenu (menuName = "Combat/Melee Combo", fileName = "New Melee Combo")]
	public class Combo : ScriptableObject {

		public ComboWeaponType ComboType;

		[ShowIf ("ComboType", ComboWeaponType.Melee)]
		public List<MeleeAttackData> Attacks;
		[ShowIf ("ComboType", ComboWeaponType.Ranged)]
		public List<RangedAttackData> RangedAttacks;

		public MeleeAttackData GetAttack(int index) { return Attacks[index]; }
	}
}