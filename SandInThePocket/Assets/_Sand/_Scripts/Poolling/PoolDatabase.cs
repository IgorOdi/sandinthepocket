using System.Collections.Generic;
using UnityEngine;

namespace Sand.Pooling {

	[CreateAssetMenu (menuName = "Database/Pooling", fileName = "PoolDatabase")]
	public class PoolDatabase : ScriptableObject {

		public List<PoolSettings> PoolSettings;

		public void Initialize() {

			foreach (var item in PoolSettings) {

				PoolManager.CreatePool (item.PoolName, item.GameObject, item.StartingCopies);
			}
		}
	}
}