using DialogueManagerRuntime;
using Godot;
using System;

public partial class NPC_Resource : Resource {

	[Export]
	public Resource Dialogue { get; set; }

	[Export]
	public bool Can_interact { get; set; }

	[Export]
	public string Name { get; set; }

	[Export]
	public int Rep { get; set; }

	[Export]
	public Resource D_Source { get; set; }

	public NPC_Resource() : this(null, false, null, 0, null) { }

	public NPC_Resource(Resource dialogue, bool can_interact, string name, int rep, Resource source) {
		Dialogue = dialogue;
		Can_interact = can_interact;
		Name = name;
		Rep = rep;
		D_Source = source;
	}
}
