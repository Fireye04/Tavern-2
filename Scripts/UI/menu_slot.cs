using Godot;
using System;
using System.Collections.Generic;

public partial class menu_slot : HBoxContainer {
	[Export]
	public menu menuItem;

	public Dictionary<string, bool> recipies = new Dictionary<string, bool>();

	public Tuple<string, int> slotRecipie = new Tuple<string, int>("none", 0);

	public int prev;
	public int cur;

	public List<string> items = new List<string>();

	[Export]
	public OptionButton optionb;

	[Export]
	public LineEdit price;

	public override void _Ready() {


	}

	public void initOptions() {
		recipies = new Dictionary<string, bool>(menuItem.availableRecipies);
		optionb.AddItem("none");
		items.Add("none");
		foreach (var item in recipies) {
			if (item.Value) {
				optionb.AddItem(item.Key);
				items.Add(item.Key);
			}
		}
	}

	public void updateOptions(Dictionary<string, bool> available) {

		foreach (var item in available) {
			var ind = items.FindIndex(x => x == item.Key);
			if (!item.Value && optionb.Selected != ind) {

				optionb.SetItemDisabled(ind, true);

			} else {
				optionb.SetItemDisabled(ind, false);
			}




		}

	}

	private void _on_recipie_item_selected(long index) {

		cur = (int)index;
		var selected = optionb.GetItemText(cur);
		slotRecipie = new Tuple<string, int>(selected, slotRecipie.Item2);
		menuItem.updateRecipies(optionb.GetItemText(prev), selected);
		prev = cur;
	}

	private void _on_line_edit_text_submitted(string new_text) {
		try {
			slotRecipie = new Tuple<string, int>(slotRecipie.Item1, new_text.ToInt());
			price.PlaceholderText = "gold";
			price.ReleaseFocus();
		} catch (FormatException) {
			price.Text = "";
			price.PlaceholderText = "int only";
			price.ReleaseFocus();
		}
	}

	public int finalValue() {
		try {
			return price.Text.ToInt();
		} catch (FormatException) {
			return slotRecipie.Item2;
		}

	}

}





