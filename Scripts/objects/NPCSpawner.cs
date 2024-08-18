using Godot;
using System;
using System.Collections.Generic;
using System.Text;

public partial class NPCSpawner : Node2D {

    [Export]
    public NPC_Resource npcr;

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
        spawnNpc();
    }

    public void spawnNpc() {
        GD.Print("spawning " + npcr.getName());
        npc = (NPC)pnpc.Instantiate();
        AddChild(npc);
        npc.init(npcr, bItem, dUI, null, pc);
        npc.Position = Position;


    }
}
