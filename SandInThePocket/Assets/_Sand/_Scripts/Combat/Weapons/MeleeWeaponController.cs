using Sand.Combat.Attacks;
using Sand.Utils;
using UnityEngine;

namespace Sand.Combat.Weapons {

	public class MeleeWeaponController : BaseWeaponController {

		public AttackData NextAttack { get { return WeaponData.Combo.GetAttack (comboIndex); } }
		protected int comboIndex;

		protected override void Update() {

			base.Update ();
			if (Input.GetKeyDown (KeyCode.Space) && cooldownRunningTime <= 0) OnWeaponPress ();
		}

		public override void OnWeaponPress() {

			cooldownRunningTime = WeaponData.Cooldown;
			attacking = true;

			var damager = SpawnHitArea ();
			damager.Initialize (NextAttack);
			Debug.Log ($"Attacking with {WeaponData.Name}\nAttack Index: {comboIndex}\nAttackDamage: {GetNextAttackDamage ()}");
			this.RunDelayed (NextAttack.Delay + NextAttack.Duration, () => attacking = false);

			IncreaseComboIndex ();
		}

		public int GetNextAttackDamage() {

			return GetAttackDamage (comboIndex);
		}

		public int GetAttackDamage(int index) {

			var attack = WeaponData.Combo.GetAttack (index);
			return Mathf.RoundToInt ((float) WeaponData.BaseDamage * attack.WeaponDamageMultiplier + attack.ExtraDamage);
		}

		protected void IncreaseComboIndex() {

			if (comboIndex < WeaponData.Combo.Attacks.Count - 1) {
				comboIndex++;
			} else {
				comboIndex = 0;
			}
		}

		//TODO: Overwrite by a pool
		private Damager SpawnHitArea() {

			GameObject hitArea = new GameObject ($"{WeaponData.Name} Hit Area {comboIndex}");
			hitArea.transform.position = transform.position;

			if (NextAttack.ColliderBuildData.ColliderBuildType == EColliderBuildType.Box) {
				var boxCollider = hitArea.AddComponent<BoxCollider> ();
				boxCollider.size = NextAttack.ColliderBuildData.Size;
				boxCollider.center = NextAttack.ColliderBuildData.Offset;
				boxCollider.isTrigger = true;
			} else {
				var sphereCollider = hitArea.AddComponent<SphereCollider> ();
				sphereCollider.radius = NextAttack.ColliderBuildData.Radius;
				sphereCollider.center = NextAttack.ColliderBuildData.Offset;
				sphereCollider.isTrigger = true;
			}

			return hitArea.AddComponent<Damager> ();
		}
	}
}