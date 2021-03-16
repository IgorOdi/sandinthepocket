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

		public override void OnWeaponPress() {

			if (cooldownRunningTime > 0 || IsAttacking) return;

			SetAttacking (true);
			cooldownRunningTime = RangedWeaponData.Cooldown;
			chargeTime = 0;

			Debug.Log ($"Starting Charging with {RangedWeaponData.Name}");
		}

		public override void OnWeaponHold() {

			if (!IsAttacking) {

				if (cooldownRunningTime <= 0) OnWeaponPress ();
				return;
			}

			chargeTime += Time.deltaTime;
			Debug.Log ($"Charging with {RangedWeaponData.Name}");
		}

		public override void OnWeaponRelease() {

			if (!IsAttacking) return;

			GameObject poolProjectileReference = NextAttack.PoolOverride == null ? RangedWeaponData.ProjectilePrefab : NextAttack.PoolOverride;
			ProjectileDamagerData damagerData = new ProjectileDamagerData (NextAttack, chargeTime, this);

			//Improviso por enquanto;
			Vector3 shootPos = transform.position + transform.forward * 1.5f + transform.up * 0.5f;

			RangedDamager projectile;
			string poolName;

			bool useDefaultProjectile = poolProjectileReference == null;
			if (useDefaultProjectile) {
				projectile = Damager.Spawn (shootPos, transform.eulerAngles, out poolName);
			} else {

				poolName = poolProjectileReference.name;
				projectile = Damager.SpawnSpecific (poolProjectileReference, shootPos, transform.eulerAngles);
			}

			projectile.Initialize (damagerData, poolName, NextAttack.TimingData.Duration, NextAttack.MovingData);
			SetAttacking (false);

			Debug.Log ($"Shooting with {RangedWeaponData.Name} | Held Time: {chargeTime}\nAttack Index: {comboIndex} | AttackDamage: {projectile.Data.FullDamage}");
		}
	}
}