using Sand.Combat.Attacks;
using Sand.Pooling;
using Sand.Utils;
using UnityEngine;

namespace Sand.Combat.Damaging {

	public partial class Damager {

		public const string BOX_DAMAGER = "BoxDamager";
		public const string SPHERE_DAMAGER = "SphereDamager";
		public const string BASE_PROJECTILE = "BaseProjectile";

		public static MeleeDamager Spawn(ColliderBuildData buildData, Transform parent, out string originName) {

			string colliderShapePoolName = GetColliderTypeString (buildData.ColliderBuildType);
			MeleeDamager damager = PoolManager.GetFromPool<MeleeDamager> (colliderShapePoolName);
			damager.transform.Reset (parent);

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

			originName = colliderShapePoolName;
			return damager;
		}

		public static RangedDamager Spawn(Vector3 position, Vector3 direction, out string originName) {

			RangedDamager damager = PoolManager.GetFromPool<RangedDamager> (BASE_PROJECTILE);
			damager.transform.Reset (null, position, direction);
			damager.gameObject.MoveToCurrentScene ();

			originName = BASE_PROJECTILE;

			return damager;
		}

		public static RangedDamager SpawnSpecific(GameObject reference, Vector3 position, Vector3 direction) {

			RangedDamager damager = PoolManager.GetFromOrCreatePool<RangedDamager> (reference);
			damager.transform.Reset (null, position, direction);
			damager.gameObject.MoveToCurrentScene ();

			return damager;
		}

		public static string GetColliderTypeString(EColliderBuildType eColliderBuildType) {

			return eColliderBuildType.Equals (EColliderBuildType.Box) ? BOX_DAMAGER : SPHERE_DAMAGER;
		}
	}
}