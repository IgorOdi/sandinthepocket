using UnityEngine;

namespace Sand.Combat.Weapons {

	public class RangedWeaponController : BaseWeaponController {

		public RangedWeaponData RangedWeaponData => (RangedWeaponData) WeaponData;
		protected float holdTime;

		void OnValidate() {

			if (WeaponData != null && !WeaponData.GetType ().Equals (typeof (RangedWeaponData))) {

				WeaponData = null;
				Debug.LogError ("You must select a Ranged Weapon");
			}
		}

		protected override void OnWeaponPress() {

			SetAttacking (true);
			cooldownRunningTime = RangedWeaponData.Cooldown;

			NextAttack.Context = this;
			Debug.Log ($"Starting Charging with {RangedWeaponData.Name}");

			holdTime = 0;
		}

		protected override void OnWeaponHold() {

			holdTime += Time.deltaTime;
			Debug.Log ($"Charging with {RangedWeaponData.Name}");
		}

		protected override void OnWeaponRelease() {

			Debug.Log (holdTime);
			Debug.Log ($"Shooting with {RangedWeaponData.Name}\nAttack Index: {comboIndex} | AttackDamage: {GetNextAttackDamage ()}");
			SetAttacking (false);
		}

		protected override void Update() {

			base.Update ();
			if (Input.GetKeyDown (KeyCode.Z) && cooldownRunningTime <= 0) OnWeaponPress ();
			if (Input.GetKey (KeyCode.Z) && IsAttacking) OnWeaponHold ();
			if (Input.GetKeyUp (KeyCode.Z) && IsAttacking) OnWeaponRelease ();
		}
	}
}