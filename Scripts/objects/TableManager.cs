using DialogueManagerRuntime;
using Godot;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

public partial class TableManager : Node2D {

    public List<Table> tables = new List<Table>();

    [Export]
    public Godot.Collections.Array<NPC_Resource> npcList;

    [Export]
    public Traveler_Resource tRepo;

    [Export]
    public Tavern tavernObject;

    public static Godot.Collections.Array<NPC_Resource> Customers;


    public static List<NPC_Resource> takenList;

    public override void _Ready() {
        Customers = new Godot.Collections.Array<NPC_Resource>();
        takenList = new List<NPC_Resource>();

        foreach (var tab in GetChildren()) {
            var tabl = (Table)tab;
            tables.Add(tabl);
            tabl.setManager(this);
        }
    }
    public void pCustomers() {
        GD.Print("\nCustomers:");
        foreach (var item in Customers) {
            GD.Print(item.Name);
        }
        GD.Print(" ");
    }

    public void setCustomers() {
        var rand = new Random();
        int numTravelers = 1 + GameState.getPrices() + (GameState.tavernRep * 2);

        for (int i = 0; i < numTravelers; i++) {
            Customers.Add(generateTraveler());
        }

        foreach (NPC_Resource npc in npcList) {
            if (rand.Next(100) <= npc.spawnChance) {
                Customers.Add(npc);
            }
        }

        // shuffle the list stolen from
        // https://stackoverflow.com/questions/273313/randomize-a-listt
        int n = Customers.Count;
        while (n > 1) {
            n--;
            int k = rand.Next(n + 1);
            NPC_Resource value = Customers[k];
            Customers[k] = Customers[n];
            Customers[n] = value;

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

        travelers spr = getnewT(tRepo.cSprites[rand.Next(tRepo.cSprites.Count)], rand);

        string na = spr.Names[rand.Next(spr.Names.Count)];
        tRepo.usedTravelers.Add(spr);

        return new NPC_Resource(spr.cSprite, tRepo.Dialogue, true, na, 1, tRepo.DSource, true);
    }

    public travelers getnewT(travelers spr, Random rand) {
        if (tRepo.usedTravelers.Contains(spr)) {
            spr = tRepo.cSprites[rand.Next(tRepo.cSprites.Count)];
            return getnewT(spr, rand);
        }

        return spr;
    }


    public void npcTaken(NPC_Resource taken) {
        npcList.Remove(taken);
        takenList.Add(taken);
    }

    public void npcFree(NPC freed, Table tar) {
        npcList.Add(freed.stats);
        takenList.Remove(freed.stats);
        // actually useful:

        freed.QueueFree();

        if (Customers.Count > 0) {
            spawn(tar);
        } else {
            if (takenList.Count == 0) {
                tavernObject.closeTavern();
            }
        }
    }

    public void spawn(Table tab) {
        NPC_Resource target = Customers[0];
        Customers.RemoveAt(0);
        // pCustomers();
        npcTaken(target);

        tab.spawnNpc(target);
    }

    public void open() {
        setCustomers();
        pCustomers();
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
        Customers.Clear();
        if (takenList.Count > 0) {
            foreach (var tab in tables) {
                try {
                    tab.clearNpc();
                } catch (System.ObjectDisposedException) { }

            }
        }

    }

}
