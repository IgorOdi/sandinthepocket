using System;
using System.Collections.Generic;
using Sand.Combat;
using UnityEngine;

namespace Sand.Database.Status {

	[CreateAssetMenu (menuName = "Status/Level Settings", fileName = "New Status level Settings")]
	public class StatusLevelSettings : ScriptableObject {

		public ECombatStatus Type;
		public List<StatusLevelData> StatusSettings;
	}
}