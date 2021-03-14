using UnityEngine;

namespace Sand.Utils {

	public static class TransformUtils {

		public static void Reset(this Transform trans, Transform parent = null, bool activeState = true) {

			trans.parent = parent;
			trans.localPosition = Vector3.zero;
			trans.localEulerAngles = Vector3.zero;
			trans.localScale = Vector3.one;
			trans.gameObject.SetActive (activeState);
		}
	}
}