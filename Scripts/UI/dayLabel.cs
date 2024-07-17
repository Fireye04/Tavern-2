using Godot;
using System;

public partial class dayLabel : RichTextLabel {

	[Export]
	public DayUI dayUI;

	public override void _Ready() {
		Text = "Day " + dayUI.currentDay.ToString();
	}

	public void dayUpdate() {
		Text = "Day " + dayUI.currentDay.ToString();
	}

}

