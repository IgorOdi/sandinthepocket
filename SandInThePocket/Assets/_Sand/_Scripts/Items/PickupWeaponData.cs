using UnityEngine;

namespace Sand.Items {

	[CreateAssetMenu (menuName = "Items/Pickup Weapon", fileName = "New Pickup Weapon")]
	public class PickupWeaponData : PickupItemData {

        public GameObject PickupPrefab;
		public GameObject ControllerPrefab;
	}
}