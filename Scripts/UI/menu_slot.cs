using Godot;
using System;
using System.Collections.Generic;

public partial class menu_slot : HBoxContainer {
    [Export]
    public menu menuItem;

    public List<string> recipies;

    public Dictionary<string, int> menuList;

    public override void _Ready() {
        recipies = new List<string>(menuItem.recipies);
        var optionb = GetNode<OptionButton>("recipie");
        foreach (var item in recipies) {
            optionb.AddItem(item);
        }
    }



}
