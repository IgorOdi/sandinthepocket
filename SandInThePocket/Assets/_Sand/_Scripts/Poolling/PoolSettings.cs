using System.Collections.Generic;
using UnityEngine;

namespace Sand.Pooling {

	[System.Serializable]
	public class PoolSettingsSet {

		public string PoolName;
		public GameObject GameObject;
		public int StartingCopies;
	}

	[CreateAssetMenu (menuName = "Pooling/Settings", fileName = "PoolSettings")]
	public class PoolSettings : ScriptableObject {

		public List<PoolSettingsSet> PoolSettingsSets;

		public void Initialize() {

			foreach (var item in PoolSettingsSets) {

				PoolManager.CreatePool (item.PoolName, item.GameObject, item.StartingCopies);
			}
		}
	}
}