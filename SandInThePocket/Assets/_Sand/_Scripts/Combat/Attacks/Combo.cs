using System.Collections.Generic;
using UnityEngine;

namespace Sand.Combat.Attacks {

	[CreateAssetMenu (menuName = "Combat/Combo", fileName = "New Combo")]
	public class Combo : ScriptableObject {

		public List<AttackData> Attacks;

		public AttackData GetAttack(int index) { return Attacks[index]; }
	}
}