using System.Collections.Generic;
using Sand.Combat;

namespace Sand.Database.Status {

	public class StatusElementRelation {

		public static Dictionary<EWeaponElement, ECombatStatus> Relation = new Dictionary<EWeaponElement, ECombatStatus> () {

			{ EWeaponElement.Fire, ECombatStatus.Burn },
			{ EWeaponElement.Ice, ECombatStatus.Frozen },
			{ EWeaponElement.Lightning, ECombatStatus.Paralyzed },
			{ EWeaponElement.Water, ECombatStatus.Wet },
		};
	}
}