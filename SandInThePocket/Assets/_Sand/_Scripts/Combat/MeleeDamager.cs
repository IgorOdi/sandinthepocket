using Sand.Combat.Attacks;
using Sand.Utils;
using UnityEngine;

namespace Sand.Combat {

	public partial class MeleeDamager : Damager<MeleeAttackData> {

		protected override string PoolName => GetColliderTypeString (AttackData.ColliderBuildData.ColliderBuildType);

		protected override void OnEnable() {

			ConfigureFromData (AttackData.ColliderBuildData);
			base.OnEnable();
		}

		private void ConfigureFromData(ColliderBuildData buildData) {

			collider = GetComponent<Collider> ();

			if (buildData.ColliderBuildType == EColliderBuildType.Box) {
				var boxCollider = (BoxCollider) collider;
				boxCollider.size = buildData.Size;
				boxCollider.center = buildData.Offset;
				boxCollider.isTrigger = true;
			} else {
				var sphereCollider = (SphereCollider) collider;
				sphereCollider.radius = buildData.Radius;
				sphereCollider.center = buildData.Offset;
				sphereCollider.isTrigger = true;
			}

			collider.enabled = false;
		}
	}
}