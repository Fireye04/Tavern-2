using Godot;
using System;
using System.Collections.Generic;
using System.Text;

public partial class Table : StaticBody2D {

    public static TableManager manager;

    [Export]
    public barter bItem { get; set; }

    [Export]
    public DayUI dUI { get; set; }

    [Export]
    public PackedScene pnpc;

    public NPC npc;

    [Export]
    public PlayerController pc;

    public override void _Ready() {

    }

    public void setManager(TableManager man) {
        manager = man;
    }

    public void spawnNpc(NPC_Resource target) {

        GD.Print("spawning " + target.Name);
        npc = (NPC)pnpc.Instantiate();
        AddChild(npc);
        npc.init(target, bItem, dUI, this, pc);
        npc.Position = ((Node2D)GetNode("Spawn Location")).Position;
    }

    public void clearNpc() {
        manager.npcFree(npc, this);
    }
}
