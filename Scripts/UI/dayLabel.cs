using Godot;
using System;

public partial class dayLabel : RichTextLabel {

	public DayUI dayUI;

	public override void _Ready() {
		dayUI = (DayUI)GetParent().GetParent();
		Text = "Day " + dayUI.currentDay.ToString();
	}

	private void _on_next_day_pressed() {
		dayUI.currentDay += 1;
		Text = "Day " + dayUI.currentDay.ToString();
	}

}

