using System;
using Sand.Combat.Attacks;

namespace Sand.Combat.Damaging {

	public interface IDamageable {

		void CauseDamage(DamagerData attackData, Action<EAttackResult, CombatActor> success, Action<bool, CombatActor> killed);
	}
}