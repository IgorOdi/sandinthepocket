using Sand.Combat.Attacks;
using UnityEngine;

namespace Sand.Combat.Weapons {

	[CreateAssetMenu (menuName = "Combat/Weapon Data", fileName = "New Weapon Data")]
	public class WeaponData : ScriptableObject {

		public string Name;
		public int BaseDamage;
		[Tooltip ("Time between the end of the previous attack and the next attack")]
		public float Cooldown;
		[Tooltip ("If you take more than x to make the next attack, your combo will reset to the first attack")]
		public float ComboResetTime = 2f;
		public Combo Combo;
	}
}