using Godot;
using System;

public partial class Lyra : Node, IDialogueSource, IBarter {

	public static barter barterItem { get; set; }
	public int happiness = 0;

	public static Tuple<string, int> item;

	private int convoCount = 0;

	private NPC_Resource npc_Resource;


	public override void _Ready() {
		npc_Resource = GD.Load<NPC_Resource>("res://Resources/Lyra_Silverwind.tres");

	}



	public async void deal(string resource) {
		var offer = barterItem.startStuff(npc_Resource);
		var value = barterItem.resourceVals[resource.ToLower()];
		item = new Tuple<string, int>(resource, value);
		await ToSignal(offer, "text_submitted");
		try {
			var resp = offer.Text;
			GD.Print(resp);
			happiness = value - resp.ToInt();
			GD.Print(happiness.ToString());
		} catch (FormatException) {
			offer.Text = "int only";
			deal(resource);
		}

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
