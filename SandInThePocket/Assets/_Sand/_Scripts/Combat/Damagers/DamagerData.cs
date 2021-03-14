using System.Collections.Generic;
using Sand.Combat.Attacks;
using Sand.Combat.Weapons;
using UnityEngine;

namespace Sand.Combat.Damaging {

	public abstract class DamagerData {

		public int FullDamage { get; protected set; }
		public readonly Vector3 ImpactForce;
		public readonly List<StatusData> Statuses;
		private readonly ScreenShakeData screenShakeData;

		public string PoolOrigin;
		public virtual BaseWeaponController Context { get; set; }
		public virtual CombatActor User => Context.Context;

		public ScreenShakeData ScreenShakeData => screenShakeData;

		public DamagerData(BaseAttackData attackData, string poolOrigin, BaseWeaponController context) {

			Context = context;

			FullDamage = attackData.DamageData.GetFullDamage (Context.WeaponData.BaseDamage);
			ImpactForce = attackData.ImpactData.ImpactForce;
			Statuses = attackData.DamageData.StatusData;
			screenShakeData = attackData.ScreenShakeData;
			PoolOrigin = poolOrigin;
		}
	}

	public class MeleeDamagerData : DamagerData {

		public MeleeDamagerData(MeleeAttackData attackData, string poolOrigin, MeleeWeaponController context) :
			base (attackData, poolOrigin, context) {

			FullDamage = attackData.DamageData.GetFullDamage (Context.WeaponData.BaseDamage);
		}
	}

	public class ProjectileDamagerData : DamagerData {

		public ProjectileDamagerData(RangedAttackData attackData, float chargeTime, string poolOrigin, RangedWeaponController context) :
			base (attackData, poolOrigin, context) {

			if (attackData.ChargeData.ChargeMathMode == ChargeMathMode.Multiplicative) {

				FullDamage = Mathf.RoundToInt (FullDamage * attackData.ChargeData.GetEvaluted (chargeTime));
			} else {

				FullDamage = Mathf.RoundToInt (FullDamage + attackData.ChargeData.GetEvaluted (chargeTime));
			}
		}
	}
}