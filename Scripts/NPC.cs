using DialogueManagerRuntime;
using Godot;
using System;

public partial class NPC : CharacterBody2D, IInteractable {
	[Export]
	public Resource Stats;

	[Export]
	public barter bItem { get; set; }

	public NPC_Resource npc_Resource;

	private IDialogueSource iSource;

	public override void _Ready() {
		npc_Resource = (NPC_Resource)Stats;

		var newNode = npc_Resource.DSource.Instantiate();
		AddChild(newNode);
		// newNode.Owner = this;

		var bSource = (IBarter)newNode;

		bSource.setUI(bItem);



		iSource = (IDialogueSource)newNode;
	}

	public bool canInteract() {
		return npc_Resource.Can_interact;
	}

	public void interact() {
		DialogueManager.ShowExampleDialogueBalloon(npc_Resource.Dialogue, iSource.getConversation());

	}


}
