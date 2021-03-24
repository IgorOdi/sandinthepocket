using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencePuzzleController : MonoBehaviour {
	[SerializeField]
	private List<SequenceButton> buttonList;

	private List<SequenceButton> currentSequenceList = new List<SequenceButton> ();

	public bool PuzzleCompleted;

	public void ResetPuzzle() {
		for (int i = 0; i < buttonList.Count; i++) {
			buttonList[i].onButtonPress = null;
			buttonList[i].SetActive (false);
		}
		AddOnButtonPress ();
		currentSequenceList.Clear ();
	}

	private void IsPuzzleCompleted() {
		for (int i = 0; i < currentSequenceList.Count; i++) {
			if (currentSequenceList[i] != buttonList[i]) {
				ResetPuzzle ();
				return;
			}
		}
		PuzzleCompleted = true;
	}

	private void AddCurrentButton(bool active, SequenceButton currentButton) {
		if (active) {
			currentSequenceList.Add (currentButton);
		}
		if (currentSequenceList.Count == buttonList.Count) {
			IsPuzzleCompleted ();
		}
	}

	private void AddOnButtonPress() {
		for (int i = 0; i < buttonList.Count; i++) {
            SequenceButton currentSequenceButton = buttonList[i];
			buttonList[i].onButtonPress += (active) => AddCurrentButton (active, currentSequenceButton);
		}
	}

	void Awake() {
		AddOnButtonPress ();
	}
}
