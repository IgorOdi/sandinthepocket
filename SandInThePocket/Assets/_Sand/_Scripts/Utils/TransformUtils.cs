using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sand.Utils {

	public static class TransformUtils {

		public static void Reset(this Transform transform, Transform parent = null, bool activeState = true) {

			transform.parent = parent;
			transform.localPosition = Vector3.zero;
			transform.localEulerAngles = Vector3.zero;
			transform.localScale = Vector3.one;
			transform.gameObject.SetActive (activeState);
		}

		public static void Reset(this Transform transform, Transform parent, Vector3 position, Vector3 direction, bool activeState = true) {

			transform.parent = parent;
			transform.localPosition = position;
			transform.localEulerAngles = direction;
			transform.localScale = Vector3.one;
			transform.gameObject.SetActive (activeState);
		}

		public static void MoveToCurrentScene(this GameObject gameObject) {

			SceneManager.MoveGameObjectToScene (gameObject, SceneManager.GetActiveScene ());
		}
	}
}