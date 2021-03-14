using Sand.Combat.Attacks;
using Sand.Combat.Projectiles;
using Sand.Pooling;
using Sand.Utils;
using UnityEngine;

namespace Sand.Combat.Weapons {

	public class RangedWeaponController : BaseWeaponController {

		public RangedWeaponData RangedWeaponData => (RangedWeaponData) WeaponData;
		public new RangedAttackData NextAttack => (RangedAttackData) RangedWeaponData.Combo.GetAttack (comboIndex);

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

			Debug.Log ($"Shooting with {RangedWeaponData.Name} | Held Time: {holdTime}\nAttack Index: {comboIndex} | AttackDamage: {GetNextAttackDamage ()}");

			BaseProjectile baseProjectile = PoolManager.GetFromPool<BaseProjectile> (NextAttack.ProjectilePoolOverride);
			baseProjectile.AttackData = NextAttack;

			//Improviso por enquanto;
			Vector3 shootPos = transform.position + transform.forward * 1.5f + transform.up * 0.5f;

			baseProjectile.transform.Reset (null, shootPos, transform.eulerAngles);
			baseProjectile.gameObject.MoveToCurrentScene ();
			SetAttacking (false);
		}

		protected override void Update() {

			base.Update ();
			//if (Input.GetKeyDown (KeyCode.Z) && cooldownRunningTime <= 0) OnWeaponPress ();
			if (Input.GetKey (KeyCode.Z) && cooldownRunningTime <= 0 && !IsAttacking) OnWeaponPress ();
			if (Input.GetKey (KeyCode.Z) && IsAttacking) OnWeaponHold ();
			if (Input.GetKeyUp (KeyCode.Z) && IsAttacking) OnWeaponRelease ();
		}
	}
}