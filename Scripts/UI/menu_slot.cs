using Godot;
using System;
using System.Collections.Generic;

public partial class menu_slot : HBoxContainer {
    [Export]
    public menu menuItem;

    public Dictionary<string, bool> recipies = new Dictionary<string, bool>();

    //for finalized recipies
    public Dictionary<string, int> menuList = new Dictionary<string, int>();

    public int prev;
    public int cur;

    public List<string> items = new List<string>();

    [Export]
    public OptionButton optionb;

    [Export]
    public TextEdit price;

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
        menuItem.updateRecipies(optionb.GetItemText(prev), optionb.GetItemText(cur));

        prev = cur;
    }
}



