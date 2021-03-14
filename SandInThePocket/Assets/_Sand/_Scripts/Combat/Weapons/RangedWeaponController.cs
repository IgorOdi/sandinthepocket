using Sand.Combat.Attacks;
using Sand.Combat.Damaging;
using Sand.Pooling;
using Sand.Utils;
using UnityEngine;

namespace Sand.Combat.Weapons {

	public class RangedWeaponController : BaseWeaponController {

		public RangedWeaponData RangedWeaponData => (RangedWeaponData) WeaponData;
		private new RangedAttackData NextAttack => (RangedAttackData) WeaponData.Combo.GetAttack (comboIndex);

		protected float chargeTime;

		void OnValidate() {

			if (WeaponData != null && !WeaponData.GetType ().Equals (typeof (RangedWeaponData))) {

				WeaponData = null;
				Debug.LogError ("You must select a Ranged Weapon");
			}
		}

		protected override void OnWeaponPress() {

			SetAttacking (true);
			cooldownRunningTime = RangedWeaponData.Cooldown;
			chargeTime = 0;

			Debug.Log ($"Starting Charging with {RangedWeaponData.Name}");
		}

		protected override void OnWeaponHold() {

			chargeTime += Time.deltaTime;
			Debug.Log ($"Charging with {RangedWeaponData.Name}");
		}

		protected override void OnWeaponRelease() {

			string poolOrigin = string.IsNullOrEmpty (NextAttack.PoolOverride) ? RangedWeaponData.ProjectilePool : NextAttack.PoolOverride;

			RangedDamager baseProjectile = PoolManager.GetFromPool<RangedDamager> (poolOrigin);
			ProjectileDamagerData damagerData = new ProjectileDamagerData (NextAttack, chargeTime, poolOrigin, this);

			//Improviso por enquanto;
			Vector3 shootPos = transform.position + transform.forward * 1.5f + transform.up * 0.5f;

			baseProjectile.transform.Reset (null, shootPos, transform.eulerAngles);
			baseProjectile.gameObject.MoveToCurrentScene ();

			baseProjectile.Initialize (damagerData, NextAttack.TimingData.Duration, NextAttack.MovingData);
			SetAttacking (false);

			Debug.Log ($"Shooting with {RangedWeaponData.Name} | Held Time: {chargeTime}\nAttack Index: {comboIndex} | AttackDamage: {baseProjectile.Data.FullDamage}");
		}

		protected override void Update() {

			base.Update ();
			if (Input.GetKey (KeyCode.Z) && cooldownRunningTime <= 0 && !IsAttacking) OnWeaponPress ();
			if (Input.GetKey (KeyCode.Z) && IsAttacking) OnWeaponHold ();
			if (Input.GetKeyUp (KeyCode.Z) && IsAttacking) OnWeaponRelease ();
		}
	}
}