using System;
using Sand.Combat.Attacks;

namespace Sand.Combat {

	public interface IDamageable {

		void CauseDamage(AttackData attackData, Action<EAttackResult, CombatActor> success, Action<bool, CombatActor> killed);
	}
}