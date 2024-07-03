using Godot;
using System;
using DialogueManagerRuntime;
using System.Threading.Tasks;
using System.ComponentModel;

public partial class Lyra : Node, IDialogueSource, IBarter {

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


	public override void _Ready() {
		npc_Resource = GD.Load<NPC_Resource>("res://Resources/Lyra_Silverwind.tres");

	}



	public async Task Deal(string resource) {

		offerItem = barterItem.startStuff(npc_Resource);

		var value = barterItem.resourceVals[resource.ToLower()];
		item = new Tuple<string, int>(resource, value);
		offerItem.GrabFocus();
		await ToSignal(offerItem, "text_submitted");
		try {
			offerAmount = offerItem.Text.ToInt();
			if (barterItem.pc.gold < offerAmount) {
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
		barterItem.pc.inventory[item.Item1] += 1;
	}


	public void endDeal() {
		barterItem.endStuff();
	}

	public void addGold(int val) {
		barterItem.pc.gold -= val;
		barterItem.goldUpdate();
	}

	public void removeGold(int val) {
		barterItem.pc.gold -= val;
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
