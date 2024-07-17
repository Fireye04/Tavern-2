using Godot;
using System;

public partial class StatusLabel : RichTextLabel {

	public override void _Ready() {
		Text = stateToStr(GameState.currentState);
	}

	public void stateUpdate() {
		Text = stateToStr(GameState.currentState);
	}

	private string stateToStr(State st) {
		if (st == State.closed_morning) {
			return "Closed (Morning)";
		} else if (st == State.closed_afternoon) {
			return "Closed (Afternoon)";
		} else {
			return "Open";
		}
	}
}
