using DialogueManagerRuntime;
using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class travelers : Resource {

	[Export]
	public SpriteFrames cSprite { get; set; }

	[Export]
	public Godot.Collections.Array<string> Names;

	public travelers() : this(null, null) { }

	public travelers(SpriteFrames sprites, Godot.Collections.Array<string> names) {
		cSprite = sprites;
		Names = names;
	}
}
