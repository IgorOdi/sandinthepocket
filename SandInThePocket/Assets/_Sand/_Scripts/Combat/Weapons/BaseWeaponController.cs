using UnityEngine;

namespace Sand.Combat.Weapons {

	public abstract class BaseWeaponController : MonoBehaviour, IWeapon {

		public WeaponData WeaponData;
		public bool IsAttacking { get; protected set; }
		//public Controller Context {get; set;}

		protected float cooldownRunningTime;
		protected float comboResetRunningTime;

		//TODO: Abstrair para IEquipment?
		//Será que isso vai ser útil mesmo?
		public virtual void OnEquip() { }
		public virtual void OnUnequip() { }

		public virtual void OnWeaponHold() { }
		public virtual void OnWeaponPress() { }
		public virtual void OnWeaponRelease() { }

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