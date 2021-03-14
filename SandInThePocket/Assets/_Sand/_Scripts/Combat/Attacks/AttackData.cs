using System.Collections.Generic;
using Sand.Combat.Weapons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sand.Combat.Attacks {

	#region Data Classes

	[System.Serializable]
	public class DamageData {

		public float WeaponPercentage = 1;
		public int RawDamage;
		public List<StatusData> StatusData;

		public int GetFullDamage(int weaponDamage) {

			return Mathf.RoundToInt ((float) weaponDamage * WeaponPercentage + RawDamage);
		}
	}

	[System.Serializable]
	public class StatusData {

		public ECombatStatus CombatStatus;
		[Tooltip ("Chance percentage to apply the status"), Range (0f, 1f)]
		public float Chance;
		public float Duration;
	}

	[System.Serializable]
	public class ColliderBuildData {

		public EColliderBuildType ColliderBuildType;

		[ShowIf ("ColliderBuildType", EColliderBuildType.Sphere)]
		public float Radius;
		[ShowIf ("ColliderBuildType", EColliderBuildType.Box)]
		public Vector3 Size;
		public Vector3 Offset;
	}

	[System.Serializable]
	public class TimingData {

		[Tooltip ("Time before enabling the collider")]
		public float Delay;
		[Tooltip ("Time the collider will be active")]
		public float Duration;
		public float TotalDuration => Delay + Duration;
	}

	[System.Serializable]
	public class ImpactData {

		[Tooltip ("Force added to the user")]
		public Vector3 PushForce;
		[Tooltip ("Force added to the hit receiver")]
		public Vector3 ImpactForce;
	}

	[System.Serializable]
	public class ScreenShakeData {

		public float Intensity;
		public float Duration;
		[Tooltip ("Intensity multiplier during Lifetime")]
		public AnimationCurve Curve;
	}

	#endregion

	[System.Serializable]
	public class BaseAttackData {

		public DamageData DamageData;
		public TimingData TimingData;
		[Tooltip ("Not implemented")]
		public ImpactData ImpactData;
		[Tooltip ("Not implemented")]
		public ScreenShakeData ScreenShakeData;

		public BaseWeaponController Context { get; set; }
		public CombatActor User => Context.Context;

#if UNITY_EDITOR
		[Button ("Copy")]
		public void Copy() {
			Debug.Log ("Not implemented");
		}
#endif

		public int GetFullDamage() => DamageData.GetFullDamage (Context == null ? 0 : Context.WeaponData.BaseDamage);
	}

	[System.Serializable]
	public class MeleeAttackData : BaseAttackData {

		public ColliderBuildData ColliderBuildData;
	}

	[System.Serializable]
	public class RangedAttackData : BaseAttackData {

		public string ProjectilePoolOverride;
	}
}