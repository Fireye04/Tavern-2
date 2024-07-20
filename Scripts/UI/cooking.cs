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

	[Export]
	public Control invspawn;

	[Export]
	public DayUI dui;


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
			islot.init(this, item.Item1);

			cslots.Add(slot);
		}

		foreach (var item in GameState.inventory) {
			var rtl = new Label {
				HorizontalAlignment = HorizontalAlignment.Center,
				CustomMinimumSize = new Vector2(100, 25),
				Text = item.Key + "- " + item.Value.ToString()
			};
			invspawn.AddChild(rtl);
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

		foreach (var item in invspawn.GetChildren()) {
			item.QueueFree();
		}

	}

	// Exit button
	private void _on_button_pressed() {
		deactivate();
	}

	public void cook(string item) {
		bool cancook = true;
		foreach (var ing in GameState.RecipiesIngredients[item]) {
			if (GameState.inventory[ing.Item1] < ing.Item2) {
				GD.Print("Insufficient " + ing.Item1);
				cancook = false;
			}
		}

		if (cancook) {
			foreach (var ing in GameState.RecipiesIngredients[item]) {
				GameState.inventory[ing.Item1] -= ing.Item2;
			}
			GameState.held = item;
			dui.updateHeld(GameState.held);
		}

	}

}
