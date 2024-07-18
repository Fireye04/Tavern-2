using DialogueManagerRuntime;
using Godot;
using System;

public partial class NPC : CharacterBody2D, IInteractable {

	public NPC_Resource stats;

	public static barter bItem;

	private IDialogueSource iSource;

	public void init(NPC_Resource npc, barter b) {
		stats = npc;
		bItem = b;
		var newNode = stats.DSource.Instantiate();
		AddChild(newNode);

		var bSource = (INPC)newNode;
		bSource.init(this);

		bSource.setUI(bItem);
		iSource = (IDialogueSource)newNode;
	}

	public void leave() {
		QueueFree();
	}

	public bool canInteract() {
		return stats.Can_interact;
	}

	public void interact() {
		DialogueManager.ShowExampleDialogueBalloon(stats.Dialogue, iSource.getConversation());

	}


}
