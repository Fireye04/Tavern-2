using DialogueManagerRuntime;
using Godot;
using System;

public partial class NPC : CharacterBody2D, IInteractable {

    public NPC_Resource stats;

    public static barter bItem;

    public static DayUI dui;

    public Table table;

    private IDialogueSource iSource;

    public void init(NPC_Resource npc, barter b, DayUI duii, Table tab) {
        stats = npc;
        bItem = b;
        dui = duii;
        table = tab;
        var asp = (AnimatedSprite2D)GetNode("AnimatedSprite2D");
        asp.SpriteFrames = npc.cSprite;
        var newNode = stats.DSource.Instantiate();
        AddChild(newNode);
        var bSource = (INPC)newNode;
        bSource.init(this);

        bSource.setUI(bItem, dui);


        iSource = (IDialogueSource)newNode;
    }

    public void leave() {
        if (table is not null) {
            table.clearNpc();
        } else {
            QueueFree();
        }

    }

    public bool canInteract() {
        return stats.Can_interact;
    }

    public void interact() {
        DialogueManager.ShowExampleDialogueBalloon(stats.Dialogue, iSource.getConversation());

    }


}
