using Sand.Combat.Attacks;
using UnityEngine;

namespace Sand.Combat.Weapons {

	[CreateAssetMenu (menuName = "Combat/Weapon Data", fileName = "New Weapon Data")]
	public class WeaponData : ScriptableObject {

		public string Name;
		public int BaseDamage;
		public float Cooldown;
		public EDamageType DamageType;
		public Combo Combo;
	}
}