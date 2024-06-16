using Godot;
using System;

public partial class Main : Node2D {
	public int daysPassed = 0;

	private void _on_control_pressed() {
		daysPassed += 1;
	}


}



