using Sand.Items;
using UnityEngine;

namespace Sand.Database.Items {

	[CreateAssetMenu (menuName = "Database/Key", fileName = "KeyDatabase")]
	public class KeyDatabase : Database<PickupKeyData> { }
}