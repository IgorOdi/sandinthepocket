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

		public virtual BaseWeaponController Context { get; set; }
		public virtual CombatActor User => Context.Context;

		public DamagerData(BaseAttackData attackData, BaseWeaponController context) {

			Context = context;

			FullDamage = attackData.DamageData.GetFullDamage (Context.WeaponData.BaseDamage);
			ImpactForce = attackData.ImpactData.ImpactForce;
			Statuses = ConfigureStatuses (attackData.DamageData.StatusData, context.WeaponData);
			ScreenShakeData = attackData.ScreenShakeData;
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

		public MeleeDamagerData(MeleeAttackData attackData, MeleeWeaponController context) :
			base (attackData, context) {

			FullDamage = attackData.DamageData.GetFullDamage (Context.WeaponData.BaseDamage);
		}
	}

	public class ProjectileDamagerData : DamagerData {

		public ProjectileDamagerData(RangedAttackData attackData, float chargeTime, RangedWeaponController context) :
			base (attackData, context) {

			if (attackData.ChargeData.ChargeMathMode == EChargeMathMode.Multiplicative) {

				FullDamage = Mathf.RoundToInt (FullDamage * attackData.ChargeData.GetEvaluted (chargeTime));
			} else {

				FullDamage = Mathf.RoundToInt (FullDamage + attackData.ChargeData.GetEvaluted (chargeTime));
			}
		}
	}
}