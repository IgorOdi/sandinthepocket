using System;
using System.Collections;
using System.Collections.Generic;
using Sand.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sand.Pooling {

	public class Pool {

		public string Name { get; set; }
		public Queue<GameObject> PooledObjects { get; set; }
		public GameObject RootGameObject;

		private GameObject reference;

		public Pool(string name, GameObject poolObject, int count = 1) {

			Name = name;
			PooledObjects = new Queue<GameObject> ();
			RootGameObject = new GameObject (Name);
			reference = poolObject;

			GameObject newCopy;
			for (int i = 0; i < count; i++) {

				newCopy = GameObject.Instantiate (reference);
				newCopy.transform.Reset (RootGameObject.transform, false);
				PooledObjects.Enqueue (newCopy);
			}
		}

		public Pool(string name) {

			Name = name;
			PooledObjects = new Queue<GameObject> ();
			RootGameObject = new GameObject (Name);
		}

		public void Add(GameObject gameObject) {

			PooledObjects.Enqueue (gameObject);
			gameObject.transform.Reset (RootGameObject.transform, false);
		}

		public GameObject Get(Action onFail) {

			if (PooledObjects.Count <= 0) {

				onFail?.Invoke ();
			}

			return Get ();
		}

		public GameObject Get(bool failProof) {

			if (PooledObjects.Count <= 0 && failProof) {

				Add (GameObject.Instantiate (reference));
			}

			return Get ();
		}

		public GameObject Get() {

			return PooledObjects.Dequeue ();
		}

		public T Get<T>() {

			return Get ().GetComponent<T> ();
		}

		public void Clear() {

			int count = PooledObjects.Count;
			for (int i = 0; i < count; i++) {

				GameObject.Destroy (PooledObjects.Dequeue ());
			}
		}

		public void Delete() {

			Clear ();
			GameObject.Destroy (RootGameObject);
		}
	}
}