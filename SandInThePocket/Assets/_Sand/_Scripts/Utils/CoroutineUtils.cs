using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sand.Utils {

	public static class CoroutineUtils {

		public static Coroutine RunDelayed(this MonoBehaviour mono, float delay, Action callback) {

			return mono.StartCoroutine (WaitFor (delay, callback));
		}

		private static IEnumerator WaitFor(float delay, Action callback) {

			float startTime = Time.time;
			while (Time.time < startTime + delay)
				yield return null;
				
			callback?.Invoke ();
		}
	}
}