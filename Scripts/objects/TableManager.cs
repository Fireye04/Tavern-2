using Godot;
using System;
using System.Collections.Generic;

public partial class TableManager : Node2D {

    public List<Table> tables = new List<Table>();

    [Export]
    public Godot.Collections.Array<NPC_Resource> NPCs;

    [Export]
    public Traveler tRepo;

    public static List<NPC_Resource> npcList;

    public static List<NPC_Resource> takenList;

    public override void _Ready() {

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
