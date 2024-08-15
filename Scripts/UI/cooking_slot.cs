using Godot;
using System;
using System.Collections.Generic;

public partial class cooking_slot : MarginContainer {

    public string item;

    public cooking dad;

    [Export]
    public PackedScene ingred;

    [Export]
    public Label itemName;

    [Export]
    public Control spawnloc;

    [Export]
    public Button cookButton;

    public List<(string, int)> recipie;

    public void init(cooking d, string cookitem) {
        if (GameState.currentState != State.open) {
            cookButton.Visible = false;
        }
        dad = d;
        item = cookitem;
        //update name label
        itemName.Text = item;
        //get ingredients and create slots
        recipie = GameState.RecipiesIngredients[cookitem];
        foreach (var ing in recipie) {
            var slot = ingred.Instantiate();
            spawnloc.AddChild(slot);
            var islot = (ingredent)slot;
            //init slot from gamestate 
            islot.init(ing.Item1, ing.Item2.ToString());
        }
    }

    private void _on_button_pressed() {
        dad.cook(item);
    }

}

