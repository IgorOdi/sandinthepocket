using Sand.Combat.Attacks;
using Sand.Combat.Damaging;
using Sand.Utils;
using UnityEngine;

namespace Sand.Combat.Weapons {

	public class MeleeWeaponController : BaseWeaponController {

		public MeleeWeaponData MeleeWeaponData => (MeleeWeaponData) WeaponData;
		public new MeleeAttackData NextAttack => (MeleeAttackData) WeaponData.Combo.GetAttack (comboIndex);

		void OnValidate() {

			if (WeaponData != null && !WeaponData.GetType ().Equals (typeof (MeleeWeaponData))) {

				WeaponData = null;
				Debug.LogError ("You must select a Melee Weapon");
			}
		}

		public override void OnWeaponPress() {

			if (cooldownRunningTime > 0) return;

			SetAttacking (true);
			cooldownRunningTime = MeleeWeaponData.Cooldown;
			comboResetRunningTime = MeleeWeaponData.ComboResetTime;

			this.RunDelayed (NextAttack.TimingData.TotalDuration, () => SetAttacking (false));

			MeleeDamagerData damagerData = new MeleeDamagerData (NextAttack, this);

			MeleeDamager damager = Damager.Spawn (NextAttack.ColliderBuildData, transform, out string poolOrigin);
			damager.Initialize (damagerData, poolOrigin, NextAttack.TimingData.Delay, NextAttack.TimingData.Duration);

			IncreaseComboIndex ();

			Debug.Log ($"Attacking with {WeaponData.Name}\nAttack Index: {comboIndex} | AttackDamage: {damagerData.FullDamage}");
		}

		protected void IncreaseComboIndex() {

			if (comboIndex < MeleeWeaponData.Combo.MeleeAttacks.Count - 1) {
				comboIndex++;
			} else {
				comboIndex = 0;
			}
		}
	}
}