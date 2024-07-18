using Godot;
using System;


public partial class door : StaticBody2D, IInteractable {

	[Export]
	public string targetPath;

	public bool canInteract() {
		return true;
	}

	public void interact() {
		if (GameState.currentState == State.closed_morning) {
			GetTree().ChangeSceneToFile(targetPath);
		} else if (GameState.currentState == State.closed_afternoon) {
			GetTree().ChangeSceneToFile(targetPath);
		} else {
			GD.Print("Why are you here. This should be impossible.");
			GetTree().ChangeSceneToFile(targetPath);
		}


	}
}
