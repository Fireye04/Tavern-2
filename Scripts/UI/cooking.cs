using Godot;
using System;
using System.Collections.Generic;

public partial class cooking : Control {

	public List<cooking_slot> cslots;

	[Export]
	public PackedScene ingred;

	[Export]
	public Control spawnloc;

	public void activate() {

		foreach (var item in GameState.currentMenu) {
			if (item.Item1 is "none") {
				continue;
			}

			var slot = ingred.Instantiate();
			spawnloc.AddChild(slot);
			var islot = (cooking_slot)slot;
			//init slot from gamestate 
			islot.init(item.Item1);
		}

		Visible = true;
	}

	public void deactivate() {

		Visible = false;
	}
}
