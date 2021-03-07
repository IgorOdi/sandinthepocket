using System;
using Sand.Combat.Attacks;

namespace Sand.Combat {

	[Serializable]
	public class Status {

		public ECombatStatus Type;
		public float TotalDuration;
        public float CurrentDuration;
		public int DamagePerSecond;

		public Status(ECombatStatus type, float duration) {

			Type = type;
			TotalDuration = duration;
            CurrentDuration = TotalDuration;
		}

		public Status(StatusData data) {

			Type = data.CombatStatus;
			TotalDuration = data.Duration;
            CurrentDuration = TotalDuration;
		}
	}
}