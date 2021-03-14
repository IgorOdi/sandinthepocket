using Sand.Combat.Attacks;
using Sand.Utils;
using UnityEngine;

namespace Sand.Combat.Weapons {

	public class MeleeWeaponController : BaseWeaponController {

		public MeleeWeaponData MeleeWeaponData => (MeleeWeaponData) WeaponData;

		void OnValidate() {

			if (WeaponData != null && !WeaponData.GetType ().Equals (typeof (MeleeWeaponData))) {

				WeaponData = null;
				Debug.LogError("You must select a Melee Weapon");
			}
		}

		protected override void OnWeaponPress() {

			SetAttacking (true);
			cooldownRunningTime = MeleeWeaponData.Cooldown;
			comboResetRunningTime = MeleeWeaponData.ComboResetTime;

			NextAttack.Context = this;
			Debug.Log ($"Attacking with {WeaponData.Name}\nAttack Index: {comboIndex} | AttackDamage: {GetNextAttackDamage ()}");
			this.RunDelayed (NextAttack.TimingData.TotalDuration, () => SetAttacking (false));
			Damager.Spawn ((MeleeAttackData) NextAttack, transform);

			IncreaseComboIndex ();
		}

		protected void IncreaseComboIndex() {

			if (comboIndex < MeleeWeaponData.Combo.Attacks.Count - 1) {
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