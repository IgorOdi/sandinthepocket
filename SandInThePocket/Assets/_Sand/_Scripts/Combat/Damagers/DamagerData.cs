using System.Collections.Generic;
using Sand.Combat.Attacks;
using Sand.Combat.Weapons;
using UnityEngine;

namespace Sand.Combat.Damaging {

	public abstract class DamagerData {

		public int FullDamage { get; protected set; }
		public readonly Vector3 ImpactForce;
		public readonly List<StatusData> Statuses;
		public readonly ScreenShakeData ScreenShakeData;

		public string PoolOrigin;
		public virtual BaseWeaponController Context { get; set; }
		public virtual CombatActor User => Context.Context;

		public DamagerData(BaseAttackData attackData, string poolOrigin, BaseWeaponController context) {

			Context = context;

			FullDamage = attackData.DamageData.GetFullDamage (Context.WeaponData.BaseDamage);
			ImpactForce = attackData.ImpactData.ImpactForce;
			Statuses = ConfigureStatuses (attackData.DamageData.StatusData, context.WeaponData);
			ScreenShakeData = attackData.ScreenShakeData;
			PoolOrigin = poolOrigin;
		}

		public List<StatusData> ConfigureStatuses(List<StatusData> statuses, BaseWeaponData weaponData) {

			var statusList = new List<StatusData> ();
			for (int i = 0; i < statuses.Count; i++) {

				statusList.Add (statuses[i]);
			}

			if (weaponData.weaponElementSet.WeaponElement != EWeaponElement.None)
				statusList.Add (new StatusData (weaponData));

			return statusList;
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

			if (attackData.ChargeData.ChargeMathMode == EChargeMathMode.Multiplicative) {

				FullDamage = Mathf.RoundToInt (FullDamage * attackData.ChargeData.GetEvaluted (chargeTime));
			} else {

				FullDamage = Mathf.RoundToInt (FullDamage + attackData.ChargeData.GetEvaluted (chargeTime));
			}
		}
	}
}