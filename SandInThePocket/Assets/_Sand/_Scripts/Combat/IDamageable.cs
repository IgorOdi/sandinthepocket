using System;
using Sand.Combat.Attacks;

namespace Sand.Combat {

	public interface IDamageable {

		void CauseDamage(MeleeAttackData attackData, Action<EAttackResult, CombatActor> success, Action<bool, CombatActor> killed);
	}
}