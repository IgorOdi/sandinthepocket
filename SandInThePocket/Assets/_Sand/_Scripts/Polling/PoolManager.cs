using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sand.Pooling {

	public class PoolManager : MonoBehaviour {

		private static Dictionary<string, Queue<GameObject>> pools = new Dictionary<string, Queue<GameObject>> ();
		private static Transform managerTransRef;

		[RuntimeInitializeOnLoadMethod (RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Initialize() {

			SceneManager.LoadSceneAsync ("Pooling", LoadSceneMode.Additive);
		}

		void Awake() => managerTransRef = transform;

		public static bool HasPool(string poolName) {

			return managerTransRef.Find (poolName) != null;
		}

		public static Transform CreatePool(string poolName, GameObject poolObject, int count = 1) {

			Transform poolTrans = CreatePool (poolName);

			GameObject newCopy;
			for (int i = 0; i < count; i++) {

				newCopy = Instantiate (poolObject);
				AddToPool (poolName, newCopy);
			}

			return poolTrans;
		}

		public static Transform CreatePool(string poolName) {

			if (!pools.ContainsKey (poolName)) {

				pools.Add (poolName, new Queue<GameObject> ());
			}

			Transform poolParent = managerTransRef.Find (poolName);
			if (!poolParent) {

				poolParent = new GameObject (poolName).transform;
				ResetTransform (poolParent, managerTransRef);
				return poolParent;
			} else {

				throw new Exception ("Already has a pool with this name");
			}
		}

		public static void AddToPool(string poolName, GameObject poolObject) {

			Transform poolParent;
			if (!HasPool (poolName)) {

				poolParent = CreatePool (poolName);
			} else {

				poolParent = managerTransRef.Find (poolName);
			}

			if (pools[poolName].Contains (poolObject))
				throw new Exception ("This object is already on a pool");

			pools[poolName].Enqueue (poolObject);
			ResetTransform (poolObject.transform, poolParent, false);
		}

		public static GameObject GetFromPool(string poolName, Transform parent = null, Action onFail = null) {

			if (pools[poolName].Count <= 0) {

				onFail?.Invoke ();
				return null;
			}

			var pooled = pools[poolName].Dequeue ();
			if (parent == null) {
				ResetTransform (pooled.transform, null, false);
				SceneManager.MoveGameObjectToScene (pooled, SceneManager.GetActiveScene ());
			}

			ResetTransform (pooled.transform, parent);
			return pooled;
		}

		public static void ClearPool(string poolName) {

			foreach (GameObject item in pools[poolName]) {
				Destroy (item);
			}

			pools[poolName].Clear ();
		}

		public static void ClearAllPools() {

			foreach (KeyValuePair<string, Queue<GameObject>> item in pools) {

				ClearPool (item.Key);
			}
		}

		public static void DeletePool(string poolName) {

			pools.Remove (poolName);
			Destroy (managerTransRef.Find (poolName).gameObject);
		}

		public static void DeleteAllPools() {

			string[] poolNames = pools.Keys.ToArray ();
			for (int i = 0; i < poolNames.Length; i++) {

				DeletePool (poolNames[i]);
			}
		}

		private static void ResetTransform(Transform trans, Transform parent = null, bool activeState = true) {

			trans.transform.parent = parent;
			trans.transform.localPosition = Vector3.zero;
			trans.transform.localEulerAngles = Vector3.zero;
			trans.transform.localScale = Vector3.one;
		}
	}
}