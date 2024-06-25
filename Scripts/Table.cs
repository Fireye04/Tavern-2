using Godot;
using System;

public partial class Table : StaticBody2D {
	public void interact() {
		GD.Print("I AM A TABLE");
	}
	public bool canInteract() {
		return true;
	}
}
