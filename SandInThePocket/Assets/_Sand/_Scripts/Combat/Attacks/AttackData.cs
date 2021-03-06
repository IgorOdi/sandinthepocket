using System.Collections.Generic;
using Sand.Combat.Weapons;
using UnityEngine;

namespace Sand.Combat.Attacks {

	#region Data Classes

	[System.Serializable]
	public class DamageData {

		public float WeaponPercentage = 1;
		public int RawDamage;
		public EDamageType DamageType;
		public float ChanceToEffect;
		public List<StatusData> StatusData;

		public int GetFullDamage(int weaponDamage) {

			return Mathf.RoundToInt ((float) weaponDamage * WeaponPercentage + RawDamage);
		}
	}

	[System.Serializable]
	public class StatusData {

		public ECombatStatus CombatStatus;
		public float Chance;
	}

	[System.Serializable]
	public class ColliderBuildData {

		public EColliderBuildType ColliderBuildType;

		public float Radius;
		public Vector3 Size;
		public Vector3 Offset;
	}

	[System.Serializable]
	public class TimingData {

		public float Delay;
		public float Duration;
	}

	[System.Serializable]
	public class ImpactData {

		public Vector3 PushForce;
		public Vector3 ImpactForce;
	}

	[System.Serializable]
	public class ScreenShakeData {

		public float Intensity;
		public float Duration;
		public AnimationCurve Curve;
	}

	#endregion

	[System.Serializable]
	public class AttackData {

		public DamageData DamageData;
		public TimingData TimingData;
		public ColliderBuildData ColliderBuildData;
		[Tooltip ("No fully tmplemented")]
		public ImpactData ImpactData;
		[Tooltip ("No fully tmplemented")]
		public ScreenShakeData ScreenShakeData;

		public WeaponData Context { get; set; }

		public int GetFullDamage(int weaponDamage) => DamageData.GetFullDamage (weaponDamage);
	}
}