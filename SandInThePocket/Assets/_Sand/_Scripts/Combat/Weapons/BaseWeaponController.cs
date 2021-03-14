using Sand.Combat.Attacks;
using UnityEngine;

namespace Sand.Combat.Weapons {

	[RequireComponent (typeof (CombatActor))]
	public abstract class BaseWeaponController : MonoBehaviour {

		public BaseWeaponData WeaponData;
		public CombatActor Context { get; set; }
		public BaseAttackData NextAttack { get { return WeaponData.Combo.GetAttack (comboIndex); } }

		public bool IsAttacking { get; protected set; }

		protected float cooldownRunningTime;
		protected float comboResetRunningTime;
		protected int comboIndex;

		protected virtual void OnWeaponPress() { }
		protected virtual void OnWeaponHold() { }
		protected virtual void OnWeaponRelease() { }

		void OnEnable() => Initialize ();
		protected virtual void Initialize() {

			Context = GetComponent<CombatActor> ();
		}

		protected void SetAttacking(bool attacking) => IsAttacking = attacking;

		protected DamageData GetDamageSet(int index) => WeaponData.Combo.GetAttack (index).DamageData;
		protected DamageData GetNextDamageSet() => GetDamageSet (comboIndex);

		protected int GetAttackDamage(int index) => GetDamageSet (index).GetFullDamage (WeaponData.BaseDamage);
		protected int GetNextAttackDamage() => GetAttackDamage (comboIndex);

		protected virtual void Update() {

			if (cooldownRunningTime > 0 && !IsAttacking) {
				cooldownRunningTime -= Time.deltaTime;
			}

			if (comboResetRunningTime > 0 && !IsAttacking) {
				comboResetRunningTime -= Time.deltaTime;
			}

			if (comboResetRunningTime <= 0) comboIndex = 0;
		}
	}
}