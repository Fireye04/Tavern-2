using Godot;
using System;

public partial class Kitchen : StaticBody2D, IInteractable {

	[Export]
	public cooking cookUI;

	public bool canInteract() {
		return true;
	}

	public void interact() {
		if (GameState.currentState == State.open) {
			cookUI.activate();
		}

	}
}
