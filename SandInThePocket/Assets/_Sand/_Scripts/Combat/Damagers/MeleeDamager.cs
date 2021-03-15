using Sand.Combat.Attacks;
using Sand.Utils;
using UnityEngine;

namespace Sand.Combat.Damaging {

	public partial class MeleeDamager : Damager {

		protected void Awake() {

			collider = GetComponent<Collider> ();
		}

		public override void Initialize(DamagerData data, float delay, float duration) {

			collider.enabled = false;
			base.Initialize (data, delay, duration);
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