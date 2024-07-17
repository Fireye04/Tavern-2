using Godot;
using System;

public partial class Jack : Node, IDialogueSource, IBarter, INPC {

	public int spawnChance = 1;

	private int convoCount = 0;

	private barter barterItem;

	private NPC_Resource npc_Resource;

	public override void _Ready() {
		npc_Resource = GD.Load<NPC_Resource>("res://Resources/Lyra_Silverwind.tres");

	}

	public void barter() {


	}

	public NPC_Resource GetResource() {
		return npc_Resource;
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

	public int getSpawnChance() {
		return spawnChance;
	}
}
