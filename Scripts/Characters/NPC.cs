using DialogueManagerRuntime;
using Godot;
using System;

public partial class NPC : CharacterBody2D, IInteractable {

	public NPC_Resource stats;

	private IDialogueSource iSource;

	public override void _Ready() {
		var newNode = stats.DSource.Instantiate();
		AddChild(newNode);

		var bSource = (IBarter)newNode;

		// bSource.setUI(bItem);



		iSource = (IDialogueSource)newNode;
	}

	public bool canInteract() {
		return stats.Can_interact;
	}

	public void interact() {
		DialogueManager.ShowExampleDialogueBalloon(stats.Dialogue, iSource.getConversation());

	}


}
