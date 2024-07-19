using Godot;
using System;
using System.Collections.Generic;

public partial class cooking_slot : MarginContainer {

	public string item;

	[Export]
	public PackedScene ingred;

	[Export]
	public Label itemName;

	[Export]
	public Control spawnloc;

	public List<(string, int)> recipie;

	public void init(string cookitem) {
		item = cookitem;
		//update name label
		itemName.Text = item;
		//get ingredients and create slots
		recipie = GameState.Recipies[cookitem];
		foreach (var ing in recipie) {
			var slot = ingred.Instantiate();
			spawnloc.AddChild(slot);
			var islot = (ingredent)slot;
			//init slot from gamestate 
			islot.init(ing.Item1, ing.Item2.ToString());
		}
	}
}





