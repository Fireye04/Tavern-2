using DialogueManagerRuntime;
using Godot;
using System;

public partial class NPC : CharacterBody2D, IInteractable {
	[Export]
	public Resource Stats;
	private NPC_Resource npc_Resource;

	private IDialogueSource iSource;

	public override void _Ready() {
		npc_Resource = (NPC_Resource)Stats;

		var newNode = npc_Resource.DSource.Instantiate();

		iSource = (IDialogueSource)newNode;


	}

	public bool canInteract() {
		return npc_Resource.Can_interact;
	}

	public void interact() {
		DialogueManager.ShowExampleDialogueBalloon(npc_Resource.Dialogue, iSource.getConversation());

	}


}
