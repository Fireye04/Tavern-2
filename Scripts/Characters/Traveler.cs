using Godot;
using System;
using DialogueManagerRuntime;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.Generic;

public partial class Traveler : Node, IDialogueSource, INPC {

    public int spawnChance = 1;

    public static barter barterItem { get; set; }

    public static DayUI dayItem { get; set; }

    private int offerAmount;

    private static PlayerController player;

    private static NPC dad;

    public bool orderCorrect = false;

    public override void _Ready() {
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

        getStats().orderItem = new Godot.Collections.Array(){
            selec.Item1,
            selec.Item2
        };
    }

    public void recieveOrder() {
        if (GameState.held == getStats().orderItem[0].ToString()) {
            changeHeld("Nothing");
            addGold((int)getStats().orderItem[1]);

            orderCorrect = true;
        } else {
            orderCorrect = false;
        }
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

    public void leaveT() {
        var thing = (NPC)player.find_nearest_interactable();
        thing.leave();
    }


    public string getConversation() {
        var s = getStats();
        s.convoCount += 1;
        if (s.convoCount == 1) {
            // make this procedure a function as well
            s.completedConvos.Add("convo1_inside");
            return "convo1_inside";

        } else {
            //TODO: make this easier to write, as I'm gonna have to do that a lot
            if (
                s.completedConvos.Contains("convo1_inside") &&
                !s.completedConvos.Contains("convo1_inside_recieve_order")
                ) {
                s.completedConvos.Add("convo1_inside_recieve_order");
                return "convo1_inside_recieve_order";
            } else {
                s.completedConvos.Add("catchall_inside");
                return "catchall_inside";
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

    public static NPC_Resource getStats() {
        dad = (NPC)player.find_nearest_interactable();
        GD.Print(dad.stats.convoCount);
        return dad.stats;
    }

    public void init(NPC obj, PlayerController pc) {
        player = pc;
    }
}
