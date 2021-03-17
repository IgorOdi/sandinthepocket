using System;
using System.Collections.Generic;
using Sand.Database.Status;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sand.Combat.Attacks {

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
		public EStatusLevel Level;
		[Tooltip ("Chance percentage to apply the status"), Range (0f, 1f)]
		public float Chance;

		public StatusData(ECombatStatus status, EStatusLevel level, float chance) {

			CombatStatus = status;
			Level = level;
			Chance = chance;
		}

		public StatusData(Weapons.BaseWeaponData weaponData) {

			ECombatStatus statusType = StatusElementRelation.Relation[weaponData.weaponElementSet.WeaponElement];
			CombatStatus = statusType;
			Level = weaponData.weaponElementSet.StatusLevel;
			Chance = weaponData.weaponElementSet.Chance;
		}
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

	public enum EMoveMode {

		Speed,
		Force
	}

	[Serializable]
	public class MovingData {

		public EMoveMode MoveMode;

		[ShowIf ("MoveMode", EMoveMode.Speed)]
		public float Speed;
		[ShowIf ("MoveMode", EMoveMode.Force)]
		public Vector3 Force;
	}

	public enum EChargeMathMode {

		Additive,
		Multiplicative
	}

	[Serializable]
	public class ChargeData {

		public float MaxChargeTime;
		public AnimationCurve ChargeEvaluation;
		public EChargeMathMode ChargeMathMode;

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
		public GameObject PoolOverride;
	}
}