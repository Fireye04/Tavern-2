using Godot;
using System;

public partial class NPC : CharacterBody2D, IInteractable {
    public void interact() {
        GD.Print("Heyyy");
    }
    public bool canInteract() {
        return true;
    }
}

