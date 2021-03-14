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

		protected override void OnWeaponPress() {

			SetAttacking (true);
			cooldownRunningTime = MeleeWeaponData.Cooldown;
			comboResetRunningTime = MeleeWeaponData.ComboResetTime;

			this.RunDelayed (NextAttack.TimingData.TotalDuration, () => SetAttacking (false));

			string poolOrigin = Damager.GetColliderTypeString (NextAttack.ColliderBuildData.ColliderBuildType);
			MeleeDamagerData damagerData = new MeleeDamagerData (NextAttack, poolOrigin, this);

			Damager damager = Damager.Spawn (NextAttack.ColliderBuildData, transform);
			damager.Initialize (damagerData, NextAttack.TimingData.Delay, NextAttack.TimingData.Duration);

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

		protected override void Update() {

			base.Update ();
			if (Input.GetKeyDown (KeyCode.Z) && cooldownRunningTime <= 0) OnWeaponPress ();
		}
	}
}