using DialogueManagerRuntime;
using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class Traveler_Resource : Resource {

	[Export]
	public Godot.Collections.Array<travelers> cSprites { get; set; }

	[Export]
	public Resource Dialogue { get; set; }

	[Export]
	public PackedScene DSource { get; set; }

	public HashSet<string> completedConvos { get; set; }

	public HashSet<travelers> usedTravelers { get; set; }

	public Traveler_Resource() : this(null, null, null) { }

	public Traveler_Resource(Godot.Collections.Array<travelers> sprites, Resource dialogue, PackedScene src) {
		cSprites = sprites;
		Dialogue = dialogue;
		DSource = src;
		completedConvos = new HashSet<string>();
		usedTravelers = new HashSet<travelers>();
	}
}
