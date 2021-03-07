using UnityEngine;

namespace Sand.Combat.Weapons {

	[RequireComponent (typeof (CombatActor))]
	public abstract class BaseWeaponController : MonoBehaviour {

		public WeaponData WeaponData;
		public CombatActor Context { get; set; }
		public bool IsAttacking { get; protected set; }

		protected float cooldownRunningTime;
		protected float comboResetRunningTime;

		//TODO: Abstrair?
		//Será que isso vai ser útil mesmo?
		protected virtual void OnEquip() { }
		protected virtual void OnUnequip() { }
		protected virtual void OnWeaponPress() { }
		protected virtual void OnWeaponHold() { }
		protected virtual void OnWeaponRelease() { }

		void OnEnable() => Initialize ();
		protected virtual void Initialize() {

			Context = GetComponent<CombatActor> ();
		}

		protected virtual void Update() {

			if (cooldownRunningTime > 0 && !IsAttacking) {
				cooldownRunningTime -= Time.deltaTime;
			}

			if (comboResetRunningTime > 0 && !IsAttacking) {
				comboResetRunningTime -= Time.deltaTime;
			}
		}
	}
}