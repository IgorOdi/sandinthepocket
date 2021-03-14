using System;
using System.Collections.Generic;
using Sand.Combat.Weapons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sand.Combat.Attacks {

	public class DamagerData {

		public int FullDamage { get; protected set; }
		public readonly Vector3 ImpactForce;
		public readonly List<StatusData> Statuses;
		public readonly ScreenShakeData ScreenShakeData;

		public string PoolOrigin;
		public readonly BaseWeaponController Context;
		public CombatActor User => Context.Context;

		public DamagerData(BaseAttackData attackData, string poolOrigin, BaseWeaponController context) {

			FullDamage = attackData.DamageData.GetFullDamage (context.WeaponData.BaseDamage);
			ImpactForce = attackData.ImpactData.ImpactForce;
			Statuses = attackData.DamageData.StatusData;
			ScreenShakeData = attackData.ScreenShakeData;

			PoolOrigin = poolOrigin;
			Context = context;
		}
	}

	public class ProjectileDamagerData : DamagerData {

		public ProjectileDamagerData(RangedAttackData attackData, float chargeTime, string poolOrigin, BaseWeaponController context) :
			base (attackData, poolOrigin, context) {

			if (attackData.ChargeData.ChargeMathMode == ChargeMathMode.Multiplicative) {

				FullDamage = Mathf.RoundToInt (FullDamage * attackData.ChargeData.GetEvaluted (chargeTime));
			} else {

				FullDamage = Mathf.RoundToInt (FullDamage + attackData.ChargeData.GetEvaluted (chargeTime));
			}
		}
	}

	#region Data Classes

	[Serializable]
	public class DamageData {

		public float WeaponPercentage = 1;
		public int RawDamage;
		public List<StatusData> StatusData;

		public int GetFullDamage(int weaponDamage) {

			return Mathf.RoundToInt ((float) weaponDamage * WeaponPercentage + RawDamage);
		}
	}

	[Serializable]
	public class StatusData {

		public ECombatStatus CombatStatus;
		[Tooltip ("Chance percentage to apply the status"), Range (0f, 1f)]
		public float Chance;
		public float Duration;
	}

	[Serializable]
	public class ColliderBuildData {

		public EColliderBuildType ColliderBuildType;

		[ShowIf ("ColliderBuildType", EColliderBuildType.Sphere)]
		public float Radius;
		[ShowIf ("ColliderBuildType", EColliderBuildType.Box)]
		public Vector3 Size;
		public Vector3 Offset;
	}

	[Serializable]
	public class TimingData {

		[Tooltip ("Time before enabling the collider")]
		public float Delay;
		[Tooltip ("Time the collider will be active")]
		public float Duration;
		public float TotalDuration => Delay + Duration;
	}

	[Serializable]
	public class ImpactData {

		[Tooltip ("Force added to the user")]
		public Vector3 PushForce;
		[Tooltip ("Force added to the hit receiver")]
		public Vector3 ImpactForce;
	}

	[Serializable]
	public class ScreenShakeData {

		public float Intensity;
		public float Duration;
		[Tooltip ("Intensity multiplier during Lifetime")]
		public AnimationCurve Curve;
	}

	public enum MoveMode {

		Speed,
		Force
	}

	[Serializable]
	public class MovingData {

		public MoveMode MoveMode;

		[ShowIf ("MoveMode", MoveMode.Speed)]
		public float Speed;
		[ShowIf ("MoveMode", MoveMode.Force)]
		public Vector3 Force;
	}

	public enum ChargeMathMode {

		Additive,
		Multiplicative
	}

	[Serializable]
	public class ChargeData {

		public float MaxChargeTime;
		public AnimationCurve ChargeEvaluation;
		public ChargeMathMode ChargeMathMode;

		public float GetEvaluted(float chargedTime) {

			float timeConverted = Mathf.Clamp (chargedTime, 0, MaxChargeTime) / MaxChargeTime;
			return ChargeEvaluation.Evaluate (timeConverted);
		}
	}

	#endregion

	[Serializable]
	public class BaseAttackData {

		public DamageData DamageData;
		public TimingData TimingData;
		[Tooltip ("Not implemented")]
		public ImpactData ImpactData;
		[Tooltip ("Not implemented")]
		public ScreenShakeData ScreenShakeData;
		public ColliderBuildData ColliderBuildData;

#if UNITY_EDITOR
		[Button ("Copy")]
		public void Copy() {
			Debug.Log ("Not implemented");
		}
#endif
	}

	[Serializable]
	public class MeleeAttackData : BaseAttackData { }

	[Serializable]
	public class RangedAttackData : BaseAttackData {

		public MovingData MovingData;
		public ChargeData ChargeData;
		public GameObject Visual;
		public string PoolOverride;
	}
}