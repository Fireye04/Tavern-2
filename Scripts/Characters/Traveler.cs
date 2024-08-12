using Godot;
using System;
using DialogueManagerRuntime;
using System.Threading.Tasks;
using System.ComponentModel;

public partial class Traveler : Node, IDialogueSource {

	public int spawnChance = 1;

	public static barter barterItem { get; set; }

	public static DayUI dayItem { get; set; }

	[Export]
	public LineEdit offerItem;

	private int offerAmount;

	public static Tuple<string, int> item;

	[Export]
	private NPC_Resource npc_Resource;

	private static NPC dad;

	[Export]
	public Godot.Collections.Array orderItem = new Godot.Collections.Array(){
		"Nothing",
		0
	};


	public bool orderCorrect = false;

	public override void _Ready() {
		npc_Resource = GD.Load<NPC_Resource>("res://Resources/Traveler.tres");

	}

	public void getOrder() {
		// Note: breaks if the menu is empty
		var rand = new Random();
		var selec = ("none", 0);
		while (true) {
			// This is genuinely horrible fix this you animal
			selec = GameState.currentMenu[rand.Next(GameState.currentMenu.Count)];
			if (!(selec.Item1 is "none")) {
				break;
			}
		}

		orderItem = new Godot.Collections.Array(){
			selec.Item1,
			selec.Item2
		};
	}

	public void recieveOrder() {
		if (GameState.held == orderItem[0].ToString()) {
			changeHeld("Nothing");
			addGold((int)orderItem[1]);

			orderCorrect = true;
		} else {
			orderCorrect = false;
		}
	}


	public void endDeal() {
		barterItem.endStuff();
	}

	// TODO: Move global edits to GameState
	public void changeHeld(string newitem) {
		GameState.held = newitem;
		dayItem.updateHeld(newitem);
	}

	public void addGold(int val) {
		GameState.gold += val;
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
		return "default";

	}

	public void setUI(barter bItem, DayUI dui) {

		barterItem = bItem;
		dayItem = dui;
	}

	public int getSpawnChance() {
		return spawnChance;
	}

	public void init(NPC obj) {
		dad = obj;
	}
}
