using Sand.Combat.Attacks;
using Sand.Pooling;
using Sand.Utils;
using UnityEngine;

namespace Sand.Combat {

	public partial class Damager : MonoBehaviour {

		protected const string BOX_DAMAGER = "BoxDamager";
		protected const string SPHERE_DAMAGER = "SphereDamager";

		public static void GenerateDamagerPool() {

			GameObject boxDamager = new GameObject (BOX_DAMAGER);
			boxDamager.gameObject.SetActive (false);
			boxDamager.AddComponent<BoxCollider> ();
			boxDamager.AddComponent<Damager> ();

			GameObject sphereDamager = new GameObject (SPHERE_DAMAGER);
			sphereDamager.SetActive (false);
			sphereDamager.AddComponent<SphereCollider> ();
			sphereDamager.AddComponent<Damager> ();

			PoolManager.CreatePool (BOX_DAMAGER, boxDamager, 10);
			PoolManager.CreatePool (SPHERE_DAMAGER, sphereDamager, 10);

			Destroy (boxDamager);
			Destroy (sphereDamager);
		}

		public static Damager Spawn(AttackData data, Transform parent = null, bool activeState = true) {

			string colliderShape = GetColliderTypeString (data.ColliderBuildData.ColliderBuildType);
			Damager damager = PoolManager.GetPool (colliderShape).Get<Damager> ();
			damager.AttackData = data;
			damager.transform.Reset (parent, activeState);
			return damager;
		}

		protected static string GetColliderTypeString(EColliderBuildType eColliderBuildType) {

			return eColliderBuildType.Equals (EColliderBuildType.Box) ? BOX_DAMAGER : SPHERE_DAMAGER;
		}
	}
}