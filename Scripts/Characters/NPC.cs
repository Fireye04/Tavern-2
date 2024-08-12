using DialogueManagerRuntime;
using Godot;
using System;

public partial class NPC : CharacterBody2D, IInteractable {

	public NPC_Resource stats;

	public static barter bItem;

	public static DayUI dui;

	private IDialogueSource iSource;

	public void init(NPC_Resource npc, barter b, DayUI duii) {
		stats = npc;
		bItem = b;
		dui = duii;
		var asp = (AnimatedSprite2D)GetNode("AnimatedSprite2D");
		asp.SpriteFrames = npc.cSprite;
		var newNode = stats.DSource.Instantiate();
		AddChild(newNode);

		var bSource = (INPC)newNode;
		bSource.init(this);

		bSource.setUI(bItem, dui);
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
