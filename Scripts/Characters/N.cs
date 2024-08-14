using Godot;
using System;
using DialogueManagerRuntime;
using System.Threading.Tasks;
using System.ComponentModel;

public partial class N : Node, IDialogueSource, INPC {

    public int spawnChance = 1;

    public static barter barterItem { get; set; }

    public static DayUI dayItem { get; set; }

    [Export]
    public bool repeatedDeal = false;

    [Export]
    public int happiness = 0;

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
        npc_Resource = GD.Load<NPC_Resource>("res://Resources/Characters/N_Ref.tres");

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
        npc_Resource.convoCount += 1;
        if (npc_Resource.convoCount == 1) {
            if (GameState.currentState == State.open) {
                // make this procedure a function as well
                npc_Resource.completedConvos.Add("convo1_inside");
                return "convo1_inside";
            } else {
                npc_Resource.completedConvos.Add("convo1_outside");
                return "convo1_outside";
            }

        } else {
            if (GameState.currentState == State.open) {
                //TODO: make this easier to write, as I'm gonna have to do that a lot
                if (
                    npc_Resource.completedConvos.Contains("convo1_inside") &&
                    !npc_Resource.completedConvos.Contains("convo1_inside_recieve_order")
                    ) {
                    npc_Resource.completedConvos.Add("convo1_inside_recieve_order");
                    return "convo1_inside_recieve_order";
                } else {
                    npc_Resource.completedConvos.Add("catchall_inside");
                    return "catchall_inside";
                }

            } else {
                npc_Resource.completedConvos.Add("catchall_outside");
                return "catchall_outside";
            }
        }

    }

    public void setUI(barter bItem, DayUI dui) {

        barterItem = bItem;
        dayItem = dui;
    }

    public int getSpawnChance() {
        return spawnChance;
    }

    public void init(NPC obj, PlayerController pc) {
        dad = obj;
    }
}
