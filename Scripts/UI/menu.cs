using Godot;
using System;
using System.Collections.Generic;

public partial class menu : Control {
    [Export]
    public VBoxContainer mList;

    public List<HBoxContainer> menuSlots;

    public List<string> recipies;


    public override void _Ready() {
        foreach (var item in mList.GetChildren()) {
            menuSlots.Add((HBoxContainer)item);
        }
    }



}
