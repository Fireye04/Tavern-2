using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class menu : Control {

	[Export]
	public VBoxContainer mList;

	public List<menu_slot> menuSlots = new List<menu_slot>();

	[Export]
	public PackedScene mslot;


	//for finalized recipies
	public List<(string, int)> menuList = new List<(string, int)>();


	public override void _Ready() {
		menuList = GameState.currentMenu;

		foreach (var item in menuList) {

			menu_slot slot = (menu_slot)mslot.Instantiate();
			mList.AddChild(slot);
			menuSlots.Add(slot);
			slot.initOptions(this, item.Item1, item.Item2);
		}

	}


	public void stopEdits() {
		foreach (var slot in menuSlots) {
			slot.stopEdit();
		}
	}

	public void allowEdits() {
		foreach (var slot in menuSlots) {
			slot.allowEdit();
		}
	}

	public void updateRecipies(string prev, string cur) {
		if (cur != "none") {
			GameState.availableRecipies[cur] = false;
		}

		GameState.availableRecipies[prev] = true;


		GD.Print(prev + " -> " + cur);
		foreach (var slot in menuSlots) {
			slot.updateOptions();
		}
	}

	public void activate() {
		Visible = true;
	}

	public void deactivate() {
		Visible = false;

		for (int i = 0; i < menuSlots.Count; i++) {

			menuList[i] = menuSlots[i].finalValue();
			GD.Print(menuSlots[i].slotRecipie.Item1 + " - " + menuList[i]);
		}

		GameState.currentMenu = menuList;

	}



}
