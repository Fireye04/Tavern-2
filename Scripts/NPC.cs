using DialogueManagerRuntime;
using Godot;
using System;

public partial class NPC : CharacterBody2D, IInteractable {

	[Export]
	private Resource _dialogue;

	[Export]
	private bool can_interact = true;

	public void interact() {
		DialogueManager.ShowExampleDialogueBalloon(_dialogue, "start");

	}
	public bool canInteract() {
		return can_interact;
	}
}

