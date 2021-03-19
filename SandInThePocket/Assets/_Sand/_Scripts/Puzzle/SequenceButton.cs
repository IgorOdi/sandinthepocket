using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceButton : SwitchButton
{
    public Action<bool> onButtonPress;

	public override void SetActive(bool active) {
		this.active = active;
		animator.SetBool ("Active", active);
        onButtonPress?.Invoke (active);
	}

}
