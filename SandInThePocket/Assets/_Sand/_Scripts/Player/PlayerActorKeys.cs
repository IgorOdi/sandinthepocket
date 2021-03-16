using System.Collections.Generic;
using Sand.Combat;

namespace Sand.Player {

	public partial class PlayerCombatActor : CombatActor {

		private static List<string> keys = new List<string> ();

		public void AddKey(string key) {

			keys.Add (key);
		}

		public void RemoveKey(string key) {

			keys.Remove (key);
		}

		public bool HasKey(string key) {

			return keys.Contains (key);
		}

		public void ClearKeys() {

			keys.Clear ();
		}

	}
}
