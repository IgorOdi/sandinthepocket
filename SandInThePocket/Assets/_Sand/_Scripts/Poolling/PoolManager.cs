using System;
using System.Collections.Generic;
using System.Linq;
using Sand.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sand.Pooling {

	public class PoolManager : MonoBehaviour {

		private static List<Pool> pools = new List<Pool> ();
		private static Transform managerTransRef;

		[RuntimeInitializeOnLoadMethod (RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Initialize() {

			SceneManager.LoadSceneAsync ("Pooling", LoadSceneMode.Additive);
		}

		private void Awake() {

			managerTransRef = transform;
			Resources.Load<PoolDatabase> ("PoolDatabase").Initialize ();
		}

		public static bool HasPool(string poolName, out Pool pool) {

			pool = GetPool (poolName);
			return pool != null;
		}

		public static bool HasPool(string poolName) {

			return GetPool (poolName) != null;
		}

		public static Pool GetPool(string poolName) {

			return pools.Where (p => p.Name.Equals (poolName)).FirstOrDefault ();
		}

		public static GameObject GetFromPool(string poolName) {

			return GetPool (poolName).Get ();
		}

		public static T GetFromPool<T>(string poolName) {

			return GetPool (poolName).Get<T> ();
		}

		public static GameObject GetFromOrCreatePool(GameObject reference) {

			GameObject pooledObject;
			if (!HasPool (reference.name)) {

				CreatePool (reference.name, reference);
				pooledObject = GetFromPool (reference.name);
				Debug.Log (pooledObject);
			} else {

				Pool pool = GetPool (reference.name);
				pooledObject = pool.Get (true);
			}

			return pooledObject;
		}

		public static T GetFromOrCreatePool<T>(GameObject reference) {

			return GetFromOrCreatePool (reference).GetComponent<T> ();
		}

		public static Pool CreatePool(string poolName, GameObject poolObject, int count = 1) {

			if (HasPool (poolName))
				throw new Exception ("There is already a pool with this name");

			var pool = new Pool (poolName, poolObject, count);
			pool.RootGameObject.transform.Reset (managerTransRef);
			pools.Add (pool);
			return pool;
		}

		public static Pool CreatePool(string poolName) {

			if (HasPool (poolName))
				throw new Exception ("There is already a pool with this name");

			var pool = new Pool (poolName);
			pool.RootGameObject.transform.Reset (managerTransRef);
			pools.Add (pool);
			return pool;
		}

		public static void AddToPool(string poolName, GameObject poolObject) {

			if (!HasPool (poolName))
				throw new Exception ("There's no pool with this name");

			var pool = GetPool (poolName);
			pool.Add (poolObject);
		}

		public static void ClearAllPools() {

			for (int i = 0; i < pools.Count; i++) {

				pools[i].Clear ();
			}
		}

		public static void DeleteAllPools() {

			for (int i = 0; i < pools.Count; i++) {

				pools[i].Delete ();
			}
			pools.Clear ();
		}
	}
}