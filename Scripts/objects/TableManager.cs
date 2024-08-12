using Godot;
using System;
using System.Collections.Generic;
using System.Data.Common;

public partial class TableManager : Node2D {

	public List<Table> tables = new List<Table>();

	[Export]
	public Godot.Collections.Array<NPC_Resource> npcList;

	[Export]
	public Traveler_Resource tRepo;

	public Godot.Collections.Array<NPC_Resource> Customers = new Godot.Collections.Array<NPC_Resource>();


	public static List<NPC_Resource> takenList;

	public override void _Ready() {
		takenList = new List<NPC_Resource>();

		foreach (var tab in GetChildren()) {
			var tabl = (Table)tab;
			tables.Add(tabl);
			tabl.setManager(this);
		}
	}

	public void setCustomers() {
		var rand = new Random();
		int numTravelers = 1 + GameState.prices + (GameState.tavernRep * 2);

		for (int i = 0; i < numTravelers; i++) {
			Customers.Add(generateTraveler());
		}

		foreach (var npc in npcList) {
			if (rand.Next(100) <= npc.spawnChance) {
				Customers.Add(npc);
			}
		}
	}

	public NPC_Resource generateTraveler() {
		var rand = new Random();

		bool full = true;
		foreach (var item in tRepo.cSprites) {
			if (!tRepo.usedTravelers.Contains(item)) {
				full = false;
			}
		}

		if (full) {
			tRepo.usedTravelers.Clear();
		}

		travelers spr = null;

		while (!tRepo.usedTravelers.Contains(spr)) {
			spr = tRepo.cSprites[rand.Next(tRepo.cSprites.Count)];
		}

		string na = spr.Names[rand.Next(spr.Names.Count)];
		tRepo.usedTravelers.Add(spr);

		NPC_Resource trav = new NPC_Resource(spr.cSprite, tRepo.Dialogue, true, na, 1, tRepo.DSource, true);

		return trav;
	}


	public void npcTaken(NPC_Resource taken) {
		npcList.Remove(taken);
		takenList.Add(taken);
	}

	public void npcFree(NPC_Resource freed, Table tar) {
		npcList.Add(freed);
		takenList.Remove(freed);
		// actually useful:

		if (Customers.Count > 0) {
			spawn(tar);
		}
	}

	public void spawn(Table tab) {
		NPC_Resource target = Customers[0];
		Customers.RemoveAt(0);
		npcTaken(target);

		tab.spawnNpc(target);
	}

	public void open() {
		setCustomers();
		GD.Print(Customers.Count);

		if (Customers.Count > tables.Count) {
			foreach (var tab in tables) {

				spawn(tab);

			}
		} else {
			for (int i = 0; i < Customers.Count; i++) {
				spawn(tables[i]);
			}
		}

	}

	public void clear() {
		foreach (var tab in tables) {
			tab.clearNpc();
		}
	}

}
