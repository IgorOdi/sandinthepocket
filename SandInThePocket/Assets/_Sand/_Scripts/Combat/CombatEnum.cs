namespace Sand.Combat {

	public enum EDamageType {

		Inherit,
		Normal,
		Fire, //Has chance to Burn;
		Water, //Has chance to Wet;
		Electric, //Has chance to Paralyze;
		Freeze, //Has chance to Freeze;
	}

	public enum ECombatStatus {

		Stagger,
		Stun,
		Burn,
		Wet,
		Paralyzed,
		Frozen,
	}

	public enum EColliderBuildType {

		Box,
		Sphere,
	}
}