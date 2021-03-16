using UnityEngine;

namespace Sand.Items {

	[CreateAssetMenu (menuName = "Items/Pickup Item", fileName = "New Pickup Item")]
	public class PickupItemData : ScriptableObject {

        public GameObject PickupPrefab;
	}
}