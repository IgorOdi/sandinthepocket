using UnityEngine;

public class SwitchButton : MonoBehaviour {

	[SerializeField]
	GameObject actionGameObject;

	protected Animator animator;
	protected bool active;

	[SerializeField]
	bool canBeDisabled;

	void Awake() {
		animator = GetComponentInChildren<Animator> ();
	}

	public virtual void SetActive(bool active) {
		this.active = active;
		actionGameObject.GetComponent<ITriggerableAction> ().SetActive (active);
		animator.SetBool ("Active", active);
	}

	void OnTriggerEnter(Collider other) {
		if (!canBeDisabled && active) {
			return;
		}
		SetActive (!active);
	}
}
