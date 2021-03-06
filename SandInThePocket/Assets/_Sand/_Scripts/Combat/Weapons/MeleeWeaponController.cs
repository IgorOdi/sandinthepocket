using Sand.Combat.Attacks;
using Sand.Utils;
using UnityEngine;

namespace Sand.Combat.Weapons {

	public class MeleeWeaponController : BaseWeaponController {

		public AttackData NextAttack { get { return WeaponData.Combo.GetAttack (comboIndex); } }
		protected int comboIndex;

		public override void OnWeaponPress() {

			cooldownRunningTime = WeaponData.Cooldown;
			NextAttack.Context = WeaponData;
			SetAttacking (true);

			var damager = SpawnHitArea ();
			damager.Initialize (NextAttack);
			Debug.Log ($"Attacking with {WeaponData.Name}\nAttack Index: {comboIndex} | AttackDamage: {GetNextAttackDamage ()}");
			this.RunDelayed (NextAttack.TimingData.TotalDuration, () => SetAttacking (false));
			comboResetRunningTime = WeaponData.ComboResetTime;

			IncreaseComboIndex ();
		}

		protected void IncreaseComboIndex() {

			if (comboIndex < WeaponData.Combo.Attacks.Count - 1) {
				comboIndex++;
			} else {
				comboIndex = 0;
			}
		}

		protected void SetAttacking(bool attacking) => IsAttacking = attacking;
		protected DamageData GetDamageSet(int index) => WeaponData.Combo.GetAttack (index).DamageData;
		protected DamageData GetNextDamageSet() => GetDamageSet (comboIndex);

		protected int GetAttackDamage(int index) => GetDamageSet (index).GetFullDamage (WeaponData.BaseDamage);
		protected int GetNextAttackDamage() => GetAttackDamage (comboIndex);

		protected override void Update() {

			base.Update ();
			if (Input.GetKeyDown (KeyCode.Z) && cooldownRunningTime <= 0) OnWeaponPress ();
			if (comboResetRunningTime <= 0) comboIndex = 0;
		}

		private Damager SpawnHitArea() {

			//TODO: Convert to pool;
			GameObject hitArea = new GameObject ($"{WeaponData.Name} Hit Area {comboIndex}");
			hitArea.transform.parent = transform;
			hitArea.transform.localPosition = Vector3.zero;
			hitArea.transform.localEulerAngles = Vector3.zero;

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