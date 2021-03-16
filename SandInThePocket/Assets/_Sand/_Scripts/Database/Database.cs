using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;

namespace Sand.Database.Items {

	public abstract class Database<T> : ScriptableObject where T : Object {

		public List<T> items;
		protected virtual string findTypeName { get; set; }

#if UNITY_EDITOR
		[Button ("Find")]
		public void GetAll() {

			items = new List<T> ();
			var assets = AssetDatabase.FindAssets ($"t:{findTypeName}");

			foreach (string guid in assets) {

				string path = AssetDatabase.GUIDToAssetPath (guid);
				items.Add (AssetDatabase.LoadAssetAtPath<T> (path));
			}
		}
#endif
	}
}