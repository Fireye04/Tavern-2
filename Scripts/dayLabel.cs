using Godot;
using System;

public partial class dayLabel : RichTextLabel {
	[Export]
	private int currentDay;

	private void _on_control_pressed() {
		currentDay += 1;
		Text = "Day " + currentDay.ToString();
	}


}


