using DialogueManagerRuntime;
using Godot;
using System;

public partial class NPC : CharacterBody2D, IInteractable {

	public static NPC_Resource stats;

	public static barter bItem;

	private IDialogueSource iSource;

	public void init(NPC_Resource npc, barter b) {
		stats = npc;
		bItem = b;
		var newNode = stats.DSource.Instantiate();
		AddChild(newNode);

		var bSource = (IBarter)newNode;

		bSource.setUI(bItem);
		iSource = (IDialogueSource)newNode;
	}

	public bool canInteract() {
		return stats.Can_interact;
	}

	public void interact() {
		DialogueManager.ShowExampleDialogueBalloon(stats.Dialogue, iSource.getConversation());

	}


}
