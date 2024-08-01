using Godot;
using System;
using DialogueManagerRuntime;
using System.Threading.Tasks;
using System.ComponentModel;

public partial class Carver : Node, IDialogueSource, INPC {

	public int spawnChance = 1;

	public static barter barterItem { get; set; }

	[Export]
	public bool repeatedDeal = false;

	[Export]
	public int happiness = 0;

	[Export]
	public LineEdit offerItem;

	private int offerAmount;

	public static Tuple<string, int> item;

	private int convoCount = 0;

	private NPC_Resource npc_Resource;

	private static NPC dad;


	public override void _Ready() {
		npc_Resource = GD.Load<NPC_Resource>("res://Resources/Carver.tres");

	}



	public async Task Deal(string resource) {

		offerItem = barterItem.startStuff(npc_Resource);

		var value = GameState.resourceVals[resource.ToLower()];
		item = new Tuple<string, int>(resource, value);
		offerItem.GrabFocus();
		await ToSignal(offerItem, "text_submitted");
		try {
			offerAmount = offerItem.Text.ToInt();
			if (GameState.gold < offerAmount) {
				offerItem.Text = "";
				offerItem.PlaceholderText = "funds";
				await Deal(resource);
			}
			happiness = offerAmount - value;
			offerItem.Text = "";
			offerItem.PlaceholderText = "Offer";

		} catch (FormatException) {
			offerItem.Text = "";
			offerItem.PlaceholderText = "int only";
			await Deal(resource);

		}

	}

	public void transaction() {
		removeGold(offerAmount);
		GameState.inventory[item.Item1] += 1;
	}


	public void endDeal() {
		barterItem.endStuff();
	}

	public void addGold(int val) {
		GameState.gold -= val;
		barterItem.goldUpdate();
	}

	public void removeGold(int val) {
		GameState.gold -= val;
		barterItem.goldUpdate();
	}

	public void addRep(int val) {
		npc_Resource.Rep += val;
		barterItem.repUpdate(npc_Resource);
	}

	public void removeRep(int val) {
		npc_Resource.Rep -= val;
		barterItem.repUpdate(npc_Resource);
	}

	public void leaveT() {
		dad.leave();
	}


	public string getConversation() {
		npc_Resource.convoCount += 1;
		if (npc_Resource.convoCount == 1) {
			if (GameState.currentState == State.open) {
				return "convo1_inside";
			} else {
				return "convo1_outside";
			}

		} else {
			if (GameState.currentState == State.open) {
				return "catchall_inside";
			} else {
				return "catchall_outside";
			}
		}

	}

	public void setUI(barter bItem) {

		barterItem = bItem;
	}

	public int getSpawnChance() {
		return spawnChance;
	}

	public void init(NPC obj) {
		dad = obj;
	}
}
