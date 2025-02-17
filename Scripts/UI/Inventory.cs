using Godot;
using System;
using System.Collections.Generic;

public partial class Inventory : Control {

	[Export]
	public VBoxContainer invBox;


	public void activate() {
		foreach (var item in GameState.inventory) {
			var rtl = new Label {
				CustomMinimumSize = new Vector2(100, 25),
				Text = item.Key + "- " + item.Value.ToString()
			};
			invBox.AddChild(rtl);
		}
		Visible = true;
	}

	public void deactivate() {
		foreach (var node in invBox.GetChildren()) {
			node.QueueFree();
		}
		Visible = false;
	}

}
