using Godot;
using System;
using System.Collections.Generic;

public partial class cooking : Control {

	public List<Node> cslots = new List<Node>();

	[Export]
	public PlayerController pc;

	[Export]
	public PackedScene ingred;

	[Export]
	public Control spawnloc;



	public void activate() {
		pc.UIActive = "cook";
		foreach (var item in GameState.currentMenu) {
			if (item.Item1 is "none") {
				continue;
			}

			var slot = ingred.Instantiate();
			spawnloc.AddChild(slot);
			var islot = (cooking_slot)slot;
			//init slot from gamestate 
			islot.init(item.Item1);

			cslots.Add(slot);
		}

		Visible = true;
	}

	public void deactivate() {
		pc.UIActive = "";
		Visible = false;
		List<Node> lst = new List<Node>(cslots);
		for (int i = 0; i < lst.Count; i++) {
			cslots[i].QueueFree();
		}
		cslots.Clear();
	}

	private void _on_button_pressed() {
		deactivate();
	}

}
