using Godot;
using System;

public partial class Lyra : Node, IDialogueSource, IBarter {

    private barter barterItem;

    private int convoCount = 0;

    private NPC_Resource npc_Resource;


    public override void _Ready() {
        npc_Resource = GD.Load<NPC_Resource>("res://Resources/Lyra_Silverwind.tres");

    }

    public void barter() {
        GD.Print(npc_Resource.Name);
        barterItem.startDialogue(npc_Resource);
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
        barterItem.fuckyou();
    }
}
