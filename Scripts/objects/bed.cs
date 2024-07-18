using Godot;
using System;

public partial class bed : StaticBody2D, IInteractable {

	[Export]
	public Tavern tav;

	public bool canInteract() {
		return true;
	}

	public void interact() {
		if (GameState.currentState == State.closed_afternoon) {
			tav.endDay();
		} else if (GameState.currentState == State.open) {
			tav.closeTavern();
		} else if (GameState.currentState == State.closed_morning) {
			tav.openTavern();
		}
	}
}
