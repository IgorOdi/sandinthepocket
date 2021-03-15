using System;
using System.Collections.Generic;
using Sand.Combat;
using UnityEngine;

namespace Sand.Database {

	[Serializable]
	public class StatusLevelData {

		public EStatusLevel Level = EStatusLevel.Level_01;
		public float Duration;
		public int DamagePerSecond;
		[Range (0f, 1f)]
		public float SlowPercent;
		public bool Stun;
	}

	[CreateAssetMenu (menuName = "Status/Level Settings", fileName = "New Status level Settings")]
	public class StatusLevelDatabase : ScriptableObject {

		public ECombatStatus Type;
		public List<StatusLevelData> StatusSettings;
	}
}