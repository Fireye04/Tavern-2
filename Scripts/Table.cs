using Godot;
using System;
using System.Collections.Generic;
using System.Text;

public partial class Table : StaticBody2D {

	[Export]
	public NPC_Resource npc1;

	[Export]
	public NPC_Resource npc2;

	public List<NPC_Resource> npcList;

	[Export]
	public barter bItem { get; set; }

	public override void _Ready() {
		npcList = new List<NPC_Resource>{
			npc1,
			npc2
		};

	}

	public void spawnNpc() {
		Random rand = new Random();
		var spawns = new List<NPC_Resource>();
		foreach (var npc in npcList) {
			for (var i = 0; i < npc.spawnChance; i++) {
				spawns.Add(npc);
			}
		}
		var target = spawns[rand.Next(spawns.Count)];
		GD.Print("spawning " + target.Name);
	}
}
