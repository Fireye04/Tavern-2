using Godot;
using System;

public partial class DayUI : Control {
	public int currentDay = 0;

	[Export]
	public Button dButton;

	public void dayOver() {
		dButton.Disabled = false;
	}

	public void dayStart() {
		dButton.Disabled = true;

		foreach (var table in GetTree().GetNodesInGroup("Tables")) {
			var tab = (Table)table;
			tab.spawnNpc();
		}

	}


}
