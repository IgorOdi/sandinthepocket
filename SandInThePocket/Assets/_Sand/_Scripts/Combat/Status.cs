using System;
using Sand.Combat.Attacks;
using Sand.Database;
using UnityEngine;

namespace Sand.Combat {

	[Serializable]
	public class Status {

		public ECombatStatus Type;

		[HideInInspector]
		public int DamagePerSecond;
		[HideInInspector]
		public float CurrentDuration;
		[HideInInspector]
		public float Duration;

		public Status(StatusData status) {

			var db = Resources.Load<StatusDatabase> ("StatusDatabase");
			var data = db.GetFromTypeAndLevel (status.CombatStatus, status.Level);

			Type = status.CombatStatus;
			DamagePerSecond = data.DamagePerSecond;
			CurrentDuration = data.Duration;
		}
	}
}