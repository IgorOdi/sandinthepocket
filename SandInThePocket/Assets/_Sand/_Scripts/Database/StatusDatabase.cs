using System.Collections.Generic;
using System.Linq;
using Sand.Combat;
using UnityEngine;

namespace Sand.Database {

	[CreateAssetMenu (menuName = "Status/Database", fileName = "StatusDatabase")]
	public class StatusDatabase : ScriptableObject {

		public List<StatusLevelDatabase> StatusLevelSettings;

		public StatusLevelDatabase GetFromType(ECombatStatus type) {

			return StatusLevelSettings.Where (x => x.Type.Equals (type)).FirstOrDefault ();
		}

		public StatusLevelData GetFromTypeAndLevel(ECombatStatus type, EStatusLevel level) {

			var sameType = GetFromType (type);
			return sameType.StatusSettings.Where (x => x.Level == level).FirstOrDefault ();
		}
	}
}