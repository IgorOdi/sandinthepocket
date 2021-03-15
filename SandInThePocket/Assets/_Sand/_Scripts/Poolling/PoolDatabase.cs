using System.Collections.Generic;
using UnityEngine;

namespace Sand.Pooling {

	[CreateAssetMenu (menuName = "Pooling/Database", fileName = "PoolDatabase")]
	public class PoolDatabase : ScriptableObject {

		public List<PoolSettings> PoolSettingsSets;

		public void Initialize() {

			foreach (var item in PoolSettingsSets) {

				PoolManager.CreatePool (item.PoolName, item.GameObject, item.StartingCopies);
			}
		}
	}
}