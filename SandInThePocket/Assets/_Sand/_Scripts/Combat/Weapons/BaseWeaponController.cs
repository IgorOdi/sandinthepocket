using Sand.Combat.Attacks;
using UnityEngine;

namespace Sand.Combat.Weapons {

	public abstract class BaseWeaponController : MonoBehaviour {

		public BaseWeaponData WeaponData;
		public CombatActor Context { get; set; }
		public virtual BaseAttackData NextAttack => WeaponData.Combo.GetAttack (comboIndex);

		public bool IsAttacking { get; protected set; }

		protected float cooldownRunningTime;
		protected float comboResetRunningTime;
		protected int comboIndex;

		public virtual void OnWeaponPress() { }
		public virtual void OnWeaponHold() { }
		public virtual void OnWeaponRelease() { }

		void OnEnable() => Initialize ();
		protected virtual void Initialize() => Context = GetComponent<CombatActor> ();
		protected void SetAttacking(bool attacking) => IsAttacking = attacking;

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