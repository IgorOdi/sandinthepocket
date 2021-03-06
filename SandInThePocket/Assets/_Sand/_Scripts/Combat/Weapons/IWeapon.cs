namespace Sand.Combat.Weapons {

	public interface IWeapon {

		void OnEquip();
		void OnUnequip();

		void OnWeaponPress();
		void OnWeaponHold();
		void OnWeaponRelease();
	}
}