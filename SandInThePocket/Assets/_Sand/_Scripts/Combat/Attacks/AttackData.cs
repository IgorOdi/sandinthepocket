using System.Collections.Generic;
using Sand.Combat.Weapons;
using UnityEngine;

namespace Sand.Combat.Attacks {

	[System.Serializable]
	public class ColliderBuildData {

		public EColliderBuildType ColliderBuildType;

		public float Radius;
		public Vector3 Size;
		public Vector3 Offset;
	}

	[System.Serializable]
	public class DamageTypeSet {

		public EDamageType DamageType;
		public float ChanceToEffect;
	}

	[System.Serializable]
	public class AttackStatusSet {

		public ECombatStatus CombatStatus;
		public float Chance;
	}

	[System.Serializable]
	public class AttackData {

		public float WeaponDamageMultiplier = 1;
		public int ExtraDamage;
		public float Delay;
		public float Duration;
		public DamageTypeSet DamageTypeSet;
		public List<AttackStatusSet> AttackStatusSet;
		public ColliderBuildData ColliderBuildData;
		[Tooltip("No fully tmplemented")]
		public Vector3 PushForce;
		[Tooltip("No fully tmplemented")]
		public Vector3 ImpactForce;
		//TODO: Screen Shake;
	}
}