using System;
using Sand.Combat;
using UnityEngine;

namespace Sand.Database.Status {

	[Serializable]
	public class StatusLevelData {

		public EStatusLevel Level = EStatusLevel.Level_01;
		public float Duration;
		public int DamagePerSecond;
		[Range (0f, 1f)]
		public float SlowPercent;
		public bool Stun;
	}
}