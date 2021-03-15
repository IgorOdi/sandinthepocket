namespace Sand.Combat {

	public enum EWeaponElement {

		None,
		Fire,
		Water,
		Lightning,
		Ice,
	}

	public enum ECombatStatus {

		Stagger,
		Stun,
		Burn,
		Wet,
		Paralyzed,
		Frozen,
	}

	public enum EStatusLevel {

		Level_01,
		Level_02,
		Level_03,
	}

	public enum EColliderBuildType {

		Box,
		Sphere,
	}

	public enum EAttackResult {

		Success,
		Miss,
		Blocked
	}

	public enum EComboWeaponType {

		Melee,
		Ranged
	}
}