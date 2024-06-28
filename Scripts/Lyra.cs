using Godot;
using System;
using DialogueManagerRuntime;
using System.Threading.Tasks;

public partial class Lyra : Node, IDialogueSource, IBarter {

	public static barter barterItem { get; set; }

	[Export]
	public int happiness = 0;

	[Export]
	public LineEdit offerItem;

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
			var resp = offerItem.Text;
			GD.Print(resp);
			happiness = resp.ToInt() - value;
			GD.Print(happiness.ToString());
		} catch (FormatException) {
			offerItem.PlaceholderText = "int only";
			await Deal(resource);
		}

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
