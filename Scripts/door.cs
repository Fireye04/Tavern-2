using Godot;
using System;

public partial class door : StaticBody2D, IInteractable {

	[Export]
	public string targetPath;

	public bool canInteract() {
		return true;
	}

	public void interact() {
		GetTree().ChangeSceneToFile(targetPath);
	}
}
