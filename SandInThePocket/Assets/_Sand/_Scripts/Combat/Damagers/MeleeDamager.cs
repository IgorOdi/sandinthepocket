using Sand.Utils;
using UnityEngine;

namespace Sand.Combat.Damaging {

	public partial class MeleeDamager : Damager {

		protected void Awake() {

			collider = GetComponent<Collider> ();
		}

		public void Initialize(DamagerData data, string poolOrigin, float delay, float duration) {

			base.Initialize (data, poolOrigin);

			collider.enabled = false;
			this.RunDelayed (delay, OnStart);
			this.RunDelayed (duration, OnEnd);
		}

		protected override void OnStart() {

			collider.enabled = true;
			base.OnStart ();
		}

		protected override void OnEnd() {

			collider.enabled = false;
			base.OnEnd ();
		}
	}
}