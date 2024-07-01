using Godot;
using System;
using System.Collections.Generic;

public partial class Inventory : Control {

	[Export]
	public VBoxContainer invBox;

	public bool isActive = false;

	public void activate(Dictionary<string, int> inv) {
		foreach (var item in inv) {
			var rtl = new RichTextLabel {
				CustomMinimumSize = new Vector2(100, 25),
				Text = item.Key + "- " + item.Value.ToString()
			};
			invBox.AddChild(rtl);
		}
		isActive = true;
		Visible = true;
	}

	public void deactivate() {
		foreach (var node in invBox.GetChildren()) {
			node.QueueFree();
		}
		isActive = false;
		Visible = false;
	}

}
