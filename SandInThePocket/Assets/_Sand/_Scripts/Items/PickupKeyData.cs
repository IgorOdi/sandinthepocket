using UnityEngine;

namespace Sand.Items {

	[CreateAssetMenu (menuName = "Items/Pickup Key", fileName = "New Pickup Key")]
	public class PickupKeyData : PickupItemData {

		public string KeyString;
	}
}