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
	public bool Can_interact { get; set; }

	[Export]
	public string Name { get; set; }

	[Export]
	public int Rep { get; set; }

	[Export]
	public PackedScene DSource { get; set; }

	public int setSpawn() {
		// returns 0-100%
		if (isTraveler) {
			return 100;
		}

		int final = (GameState.prices * 10) + (Rep * 9);
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

	public bool isTraveler;

	public NPC_Resource() : this(null, null, false, null, 0, null) { }

	public NPC_Resource(SpriteFrames sprite, Resource dialogue, bool can_interact, string name, int rep, PackedScene source, bool trav = false) {
		cSprite = sprite;
		Dialogue = dialogue;
		Can_interact = can_interact;
		Name = name;
		Rep = rep;
		DSource = source;
		convoCount = 0;
		completedConvos = new HashSet<string>();
		isTraveler = trav;
	}
}
