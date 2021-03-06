using System;
using Sand.Combat.Attacks;

namespace Sand.Combat {

	public interface IDamageable {

		void CauseDamage(AttackData attackData, Action<bool> success, Action<bool> killed);
	}
}