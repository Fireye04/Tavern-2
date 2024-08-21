using Godot;
using System;
using DialogueManagerRuntime;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Collections.Generic;

public partial class Lyra : Node, IDialogueSource, INPC {

    public int spawnChance = 1;

    public static barter barterItem { get; set; }

    public static DayUI dayItem { get; set; }

    [Export]
    public bool repeatedDeal = false;

    [Export]
    public int happiness = 0;

    [Export]
    public LineEdit offerItem;

    public LineEdit countItem;

    private int _offerAmount;
    private int _countAmount;

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
        npc_Resource = GD.Load<NPC_Resource>("res://Resources/Characters/Lyra_Silverwind.tres");

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

        (offerItem, countItem) = barterItem.startStuff(npc_Resource, resource);

        var value = GameState.resourceVals[resource.ToLower()];
        item = new Tuple<string, int>(resource, value);
        offerItem.GrabFocus();
        await ToSignal(barterItem.confirm, "pressed");

        try {
            _offerAmount = offerItem.Text.ToInt();
            _countAmount = countItem.Text.ToInt();
            if (_offerAmount < 0 || _countAmount < 0) {
                throw new ArgumentException();
            }
            if (GameState.gold < _offerAmount * _countAmount) {
                offerItem.Text = "";
                offerItem.PlaceholderText = "funds";
                countItem.Text = "";
                countItem.PlaceholderText = "funds";
                await Deal(resource);
            }
            if (_countAmount > 0) {
                happiness = _offerAmount - value;
            } else {
                happiness = 0;
            }

            offerItem.Text = "";
            countItem.Text = "";
            offerItem.PlaceholderText = "Offer";
            countItem.PlaceholderText = "Quantity";

        } catch (FormatException) {
            countItem.Text = "";
            countItem.PlaceholderText = "int only";
            offerItem.Text = "";
            offerItem.PlaceholderText = "int only";
            await Deal(resource);

        } catch (ArgumentException) {
            countItem.Text = "";
            countItem.PlaceholderText = ">0 only";
            offerItem.Text = "";
            offerItem.PlaceholderText = ">0 only";
            await Deal(resource);
        }

    }

    public void transaction() {
        removeGold(_offerAmount * _countAmount);
        GameState.inventory[item.Item1] += _countAmount;
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

    public void addTRep(double amt) {
        GameState.tavernRep += amt;
    }

    public void removeTRep(double amt) {
        GameState.tavernRep -= amt;
    }

    public void addCompletedConvo(string convo) {
        npc_Resource.completedConvos.Add(convo);
    }


    public string getConversation() {
        if (npc_Resource.convoCount == 1) {
            if (GameState.currentState == State.open) {
                // make this procedure a function as well
                if (dad.interactionCount == 1) {
                    return "convo1_inside";
                } else {
                    return "convo1_inside_repeat";
                }

            } else {
                npc_Resource.Can_interact = false;
                return "convo1_outside";
            }
        } else {
            if (GameState.currentState == State.open) {
                //TODO: make this easier to write, as I'm gonna have to do that a lot
                /*if (
                    npc_Resource.completedConvos.Contains("convo1_inside") &&
                    !npc_Resource.completedConvos.Contains("convo1_inside_recieve_order")
                    ) {*/

                if (dad.interactionCount == 1) {
                    return "catchall_inside";
                } else {
                    return "catchall_inside_repeat";
                }

            } else {
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
