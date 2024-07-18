using Godot;
using System;

public partial class DayUI : Control {
	public int currentDay = 0;

	[Export]
	public dayLabel dl;

	[Export]
	public StatusLabel sl;

	public void nextDay() {
		currentDay += 1;
		dl.dayUpdate();
		sl.stateUpdate();
	}

	public void nextState() {
		sl.stateUpdate();
	}
}
