using Godot;
using System;

public partial class Lyra : Node, IDialogueSource, IBarter {

	public static barter barterItem { get; set; }

	private int convoCount = 0;

	private NPC_Resource npc_Resource;


	public override void _Ready() {
		npc_Resource = GD.Load<NPC_Resource>("res://Resources/Lyra_Silverwind.tres");

	}

	public void barter() {
		var bitch = npc_Resource.Rep.ToString();
		if (barterItem != null) {
			barterItem.startStuff(bitch);
		}

	}

	public void deal(string resource) {
		GD.Print(resource);
	}

	public string getConversation() {
		convoCount += 1;
		if (convoCount == 1) {
			return "convo1";
		} else {
			return "catchall";
		}

	}

	public void setUI(barter bItem) {

		barterItem = bItem;
	}
}
