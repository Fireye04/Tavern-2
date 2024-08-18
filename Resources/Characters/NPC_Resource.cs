using DialogueManagerRuntime;
using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class NPC_Resource : Resource {

    [Export]
    public SpriteFrames cSprite { get; set; }

    [Export]
    public Resource Dialogue { get; set; }

    [Export]
    public bool Can_interact = true;

    [Export]
    public Godot.Collections.Array<string> Names { get; set; }

    public string getName() {
        if (Names.Count == 1) {
            return Names[0];
        } else if (Names.Count > 1) {
            var rand = new Random();
            return Names[rand.Next(Names.Count)];
        }

        return "404; Name not found";
    }

    [Export]
    public int Rep { get; set; }

    [Export]
    public PackedScene DSource { get; set; }

    public Godot.Collections.Array orderItem;

    public int setSpawn() {
        // returns 0-100%
        if (isTraveler) {
            return 100;
        }

        int final = (GameState.getPrices() * 10) + (Rep * 9);
        if (final < 0) {
            return 0;
        } else if (final > 100) {
            return 100;
        }

        return final;
    }

    public int spawnChance {
        get { return setSpawn(); }
        set { spawnChance = value; }
    }

    public int convoCount { get; set; }

    public HashSet<string> completedConvos { get; set; }

    [Export]
    public bool isTraveler;

    public NPC_Resource() : this(null, null, false, null, 0, null) { }

    public NPC_Resource(SpriteFrames sprite, Resource dialogue, bool can_interact, Godot.Collections.Array<string> names, int rep, PackedScene source, bool trav = false) {
        cSprite = sprite;
        Dialogue = dialogue;
        Can_interact = can_interact;
        Names = names;
        Rep = rep;
        DSource = source;
        convoCount = 0;
        completedConvos = new HashSet<string>();
        isTraveler = trav;
        orderItem = new Godot.Collections.Array() { "Nothing", 0 };
    }
}
