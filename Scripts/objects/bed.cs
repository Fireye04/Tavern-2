using Godot;
using System;
using System.ComponentModel;

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
		} else if (GameState.currentState == State.closed_morning && menuHasItems()) {
			tav.openTavern();
		}
	}

	private bool menuHasItems() {
		foreach (var item in GameState.currentMenu) {
			if (item.Item1 != "none") {
				return true;
			}
		}
		GD.Print("Populate ur menu, bitch");
		return false;
	}
}
