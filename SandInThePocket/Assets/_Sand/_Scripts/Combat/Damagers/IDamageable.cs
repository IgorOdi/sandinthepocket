using System;
using Sand.Combat.Attacks;

namespace Sand.Combat.Damaging {

	public interface IDamageable {

		void CauseDamage(DamagerData attackData, Action<EAttackResult, CombatActor> success, Action<bool, CombatActor> killed);
		
		void TickDamage(Status status, Action<EAttackResult, CombatActor> tickResult, Action<bool, CombatActor> killResult);
	}
}