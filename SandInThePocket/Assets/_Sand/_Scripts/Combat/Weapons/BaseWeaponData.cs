using System;
using Sand.Combat.Attacks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sand.Combat.Weapons {

	[Serializable]
	public class WeaponElementSet {

		public EWeaponElement WeaponElement;
		[HideIf ("WeaponElement", EWeaponElement.None)]
		public EStatusLevel StatusLevel;
		[HideIf ("WeaponElement", EWeaponElement.None), Range (0f, 1f)]
		public float Chance;
	}

	public abstract class BaseWeaponData : ScriptableObject {

		public string Name;
		public int BaseDamage;
		[Tooltip ("Time between the end of the previous attack and the next attack")]
		public float Cooldown;
		public WeaponElementSet weaponElementSet;
		[Tooltip ("If you take more than x to make the next attack, your combo will reset to the first attack")]
		public float ComboResetTime = 2f;
		public Combo Combo;
	}
}