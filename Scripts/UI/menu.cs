using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class menu : Control {
	[Export]
	public VBoxContainer mList;

	public List<menu_slot> menuSlots = new List<menu_slot>();

	public static List<string> recipies = new List<string> {
		"wine",
		"ale"
	};

	public Dictionary<string, bool> availableRecipies = new Dictionary<string, bool>();


	//for finalized recipies
	public Dictionary<string, int> menuList = new Dictionary<string, int>();


	public override void _Ready() {
		foreach (var item in mList.GetChildren()) {
			menuSlots.Add((menu_slot)item);
		}

		foreach (var recipie in recipies) {
			availableRecipies.Add(recipie, true);
		}


		foreach (var slot in menuSlots) {
			slot.initOptions();
		}
	}

	public void updateRecipies(string prev, string cur) {
		if (cur != "none") {
			availableRecipies[cur] = false;
		}

		if (prev != "none") {
			availableRecipies[prev] = true;
		}

		foreach (var slot in menuSlots) {
			slot.updateOptions(availableRecipies);
		}
	}

	public void activate() {
		Visible = true;
	}

	public void deactivate() {
		Visible = false;
		foreach (var slot in menuSlots) {
			if (slot.slotRecipie.Item1 != "none") {

				menuList[slot.slotRecipie.Item1] = slot.finalValue();
				GD.Print(slot.slotRecipie.Item1 + " - " + menuList[slot.slotRecipie.Item1]);
			} else {
				slot.finalValue();
			}

		}
	}



}
