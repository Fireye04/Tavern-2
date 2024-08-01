using Godot;
using System;
using System.Collections.Generic;

public partial class TableManager : Node2D {

	public List<Table> tables = new List<Table>();

	[Export]
	public NPC_Resource npc1;

	[Export]
	public NPC_Resource npc2;

	[Export]
	public NPC_Resource npc3;

	[Export]
	public NPC_Resource npc4;

	[Export]
	public NPC_Resource npc5;

	public static List<NPC_Resource> npcList;

	public static List<NPC_Resource> takenList;

	public override void _Ready() {

		npcList = new List<NPC_Resource>{
			npc1,
			npc2,
			npc3,
			npc4,
			npc5,
		};

		takenList = new List<NPC_Resource>();

		if (GetChildren().Count > npcList.Count) {
			throw new Exception("Cannot have fewer NPCs than tables");
		}

		foreach (var tab in GetChildren()) {
			var tabl = (Table)tab;
			tables.Add(tabl);
			tabl.setManager(this);
		}
	}


	public void npcTaken(NPC_Resource taken) {
		npcList.Remove(taken);
		takenList.Add(taken);
	}

	public void npcFree(NPC_Resource freed) {
		npcList.Add(freed);
		takenList.Remove(freed);
	}

	public void spawn() {
		foreach (var tab in tables) {
			tab.spawnNpc(npcList);
		}
	}

	public void clear() {
		foreach (var tab in tables) {
			tab.clearNpc();
		}
	}

}
