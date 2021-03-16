using UnityEngine;
using Sand.Items;

namespace Sand.Database.Items {

	[CreateAssetMenu (menuName = "Database/Weapon", fileName = "WeaponDatabase")]
	public class WeaponDatabase : Database<PickupWeaponData> {

		protected override string findTypeName => "pickupweapondata";
	}
}