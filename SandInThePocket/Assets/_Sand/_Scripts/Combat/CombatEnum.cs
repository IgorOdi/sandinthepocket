namespace Sand.Combat {

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

	public enum EAttackResult {

		Success,
		Miss,
		Blocked
	}
}