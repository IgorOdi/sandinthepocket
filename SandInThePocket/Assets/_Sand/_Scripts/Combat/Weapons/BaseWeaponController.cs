using Sand.Combat.Attacks;
using Sand.Utils;
using UnityEngine;

namespace Sand.Combat.Weapons {

	public abstract class BaseWeaponController : MonoBehaviour {

		public BaseWeaponData WeaponData;
		public CombatActor Context { get; set; }
		public GameObject DropObject { get; set; }
		public virtual BaseAttackData NextAttack => WeaponData.Combo.GetAttack (comboIndex);

		public bool IsAttacking { get; protected set; }

		protected float cooldownRunningTime;
		protected float comboResetRunningTime;
		protected int comboIndex;

		public virtual void OnEquip() {

			gameObject.SetActive (true);
		}

		public virtual void OnUnequip() {

			gameObject.SetActive (false);
		}

		public virtual void OnCollect(CombatActor actor) {

			Context = actor;
			transform.Reset (actor.transform);
		}

		public virtual void OnDrop() {

			Context = null;
			Vector3 previousPosition = transform.position;
			var drop = Instantiate (DropObject);
			drop.transform.Reset (null, new Vector3 (previousPosition.x, 0, previousPosition.z), Vector3.zero);

			Destroy (gameObject);
		}

		public virtual void OnWeaponPress() { }
		public virtual void OnWeaponHold() { }
		public virtual void OnWeaponRelease() { }

		void OnEnable() => Initialize ();
		protected virtual void Initialize() => Context = GetComponent<CombatActor> ();
		protected void SetAttacking(bool attacking) => IsAttacking = attacking;

		protected virtual void Update() {

			if (Input.GetKeyDown (KeyCode.LeftControl) || Input.GetKeyDown (KeyCode.LeftCommand)) {

				OnDrop ();
			}

			if (cooldownRunningTime > 0 && !IsAttacking) {
				cooldownRunningTime -= Time.deltaTime;
			}

			if (comboResetRunningTime > 0 && !IsAttacking) {
				comboResetRunningTime -= Time.deltaTime;
			}

			if (comboResetRunningTime <= 0) comboIndex = 0;
		}
	}
}