using Godot;
using System;
using System.Collections.Generic;

public partial class menu_slot : HBoxContainer {

    public menu menuItem;

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

    public void stopEdit() {
        optionb.Disabled = true;
        price.Editable = false;
    }

    public void allowEdit() {
        optionb.Disabled = false;
        price.Editable = true;
    }

    public void initOptions(menu mitem, string defItem, int defCost) {
        menuItem = mitem;
        optionb.AddItem("none");
        items.Add("none");
        foreach (var item in GameState.recipies) {
            optionb.AddItem(item);
            items.Add(item);
        }
        slotRecipie = new Tuple<string, int>(defItem, defCost);
        optionb.Select(items.IndexOf(defItem));
        price.Text = defCost.ToString();
        updateOptions();

    }

    public void updateOptions() {

        foreach (var item in GameState.availableRecipies) {
            var ind = items.FindIndex(x => x == item.Key);
            if (!item.Value && optionb.Selected != ind) {

                optionb.SetItemDisabled(ind, true);

            } else {
                optionb.SetItemDisabled(ind, false);
            }

        }

    }

    private void _on_recipie_item_selected(long index) {
        prev = items.IndexOf(slotRecipie.Item1);
        cur = (int)index;
        var selected = optionb.GetItemText(cur);
        slotRecipie = new Tuple<string, int>(selected, slotRecipie.Item2);
        menuItem.updateRecipies(optionb.GetItemText(prev), selected);
        prev = cur;
    }

    private void _on_line_edit_text_submitted(string new_text) {
        try {
            slotRecipie = new Tuple<string, int>(slotRecipie.Item1, new_text.ToInt());
            price.ReleaseFocus();
        } catch (FormatException) {
            price.Text = slotRecipie.Item2.ToString();
            price.ReleaseFocus();
        }
    }

    public (string, int) finalValue() {
        try {
            slotRecipie = new Tuple<string, int>(slotRecipie.Item1, price.Text.ToInt());
            return (slotRecipie.Item1, price.Text.ToInt());
        } catch (FormatException) {
            price.Text = slotRecipie.Item2.ToString();
            return (slotRecipie.Item1, slotRecipie.Item2);
        }

    }

}





