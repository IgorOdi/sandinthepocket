using Sand.Combat.Attacks;
using Sand.Pooling;
using Sand.Utils;
using UnityEngine;

namespace Sand.Combat.Damaging {

	public partial class Damager {

		protected const string BOX_DAMAGER = "BoxDamager";
		protected const string SPHERE_DAMAGER = "SphereDamager";

		public static Damager Spawn(ColliderBuildData buildData, Transform parent = null, bool activeState = true) {

			string colliderShapePoolName = GetColliderTypeString (buildData.ColliderBuildType);
			Damager damager = PoolManager.GetFromPool<Damager> (colliderShapePoolName);
			damager.transform.Reset (parent, activeState);

			if (buildData.ColliderBuildType == EColliderBuildType.Box) {

				var boxCollider = (BoxCollider) damager.collider;
				boxCollider.size = buildData.Size;
				boxCollider.center = buildData.Offset;
				boxCollider.isTrigger = true;
			} else {
				
				var sphereCollider = (SphereCollider) damager.collider;
				sphereCollider.radius = buildData.Radius;
				sphereCollider.center = buildData.Offset;
				sphereCollider.isTrigger = true;
			}

			return damager;
		}

		public static string GetColliderTypeString(EColliderBuildType eColliderBuildType) {

			return eColliderBuildType.Equals (EColliderBuildType.Box) ? BOX_DAMAGER : SPHERE_DAMAGER;
		}
	}
}