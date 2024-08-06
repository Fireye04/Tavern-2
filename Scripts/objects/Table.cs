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

	public override void _Ready() {

	}

	public void setManager(TableManager man) {
		manager = man;
	}

	public void spawnNpc(List<NPC_Resource> available) {

		Random rand = new Random();
		var spawns = new List<NPC_Resource>();
		foreach (var npc in available) {
			for (var i = 0; i < npc.spawnChance; i++) {
				spawns.Add(npc);
			}
		}
		var target = spawns[rand.Next(spawns.Count)];
		manager.npcTaken(target);
		GD.Print("spawning " + target.Name);
		npc = (NPC)pnpc.Instantiate();
		AddChild(npc);
		npc.init(target, bItem, dUI);
		npc.Position = ((Node2D)GetNode("Spawn Location")).Position;
	}

	public void clearNpc() {
		manager.npcFree(npc.stats);
		npc.QueueFree();
	}
}
