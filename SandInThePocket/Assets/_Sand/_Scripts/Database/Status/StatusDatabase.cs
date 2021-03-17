using System.Linq;
using Sand.Combat;
using Sand.Database.Items;
using UnityEngine;

namespace Sand.Database.Status {

	[CreateAssetMenu (menuName = "Database/Status", fileName = "StatusDatabase")]
	public class StatusDatabase : Database<StatusLevelSettings> {

		protected override string findTypeName => "statuslevelsettings";

		public StatusLevelSettings GetFromType(ECombatStatus type) {

			return items.Where (x => x.Type.Equals (type)).FirstOrDefault ();
		}

		public StatusLevelData GetFromTypeAndLevel(ECombatStatus type, EStatusLevel level) {

			var sameType = GetFromType (type);
			return sameType.StatusSettings.Where (x => x.Level == level).FirstOrDefault ();
		}
	}
}