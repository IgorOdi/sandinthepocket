using UnityEngine;

namespace Sand.Combat.Weapons {

	public abstract class BaseWeaponController : MonoBehaviour, IWeapon {

		public WeaponData WeaponData;
		public bool IsAttacking { get; protected set; }
		//public Controller Context {get; set;}

		protected float cooldownRunningTime;

		//TODO: Abstrair para IEquipment?
		public virtual void OnEquip() { }
		public virtual void OnUnequip() { }

		public virtual void OnWeaponHold() { }
		public virtual void OnWeaponPress() { }
		public virtual void OnWeaponRelease() { }

		protected virtual void Update() {

			if (cooldownRunningTime > 0 && !IsAttacking) {
				cooldownRunningTime -= Time.deltaTime;
			}
		}
	}
}