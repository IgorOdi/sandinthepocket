using Sand.Combat.Attacks;
using Sand.Utils;
using UnityEngine;

namespace Sand.Combat.Weapons {

	public class MeleeWeaponController : BaseWeaponController {

		protected AttackData NextAttack { get { return WeaponData.Combo.GetAttack (comboIndex); } }
		protected int comboIndex;

		protected override void OnWeaponPress() {

			SetAttacking (true);
			cooldownRunningTime = WeaponData.Cooldown;
			comboResetRunningTime = WeaponData.ComboResetTime;

			NextAttack.Context = this;
			Debug.Log ($"Attacking with {WeaponData.Name}\nAttack Index: {comboIndex} | AttackDamage: {GetNextAttackDamage ()}");
			this.RunDelayed (NextAttack.TimingData.TotalDuration, () => SetAttacking (false));
			Damager.Spawn (NextAttack, transform);

			IncreaseComboIndex ();
		}

		protected void IncreaseComboIndex() {

			if (comboIndex < WeaponData.Combo.Attacks.Count - 1) {
				comboIndex++;
			} else {
				comboIndex = 0;
			}
		}

		protected void SetAttacking(bool attacking) => IsAttacking = attacking;
		protected DamageData GetDamageSet(int index) => WeaponData.Combo.GetAttack (index).DamageData;
		protected DamageData GetNextDamageSet() => GetDamageSet (comboIndex);

		protected int GetAttackDamage(int index) => GetDamageSet (index).GetFullDamage (WeaponData.BaseDamage);
		protected int GetNextAttackDamage() => GetAttackDamage (comboIndex);

		protected override void Update() {

			base.Update ();
			if (Input.GetKeyDown (KeyCode.Z) && cooldownRunningTime <= 0) OnWeaponPress ();
			if (comboResetRunningTime <= 0) comboIndex = 0;
		}
	}
}