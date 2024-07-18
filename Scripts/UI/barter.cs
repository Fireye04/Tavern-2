using Godot;
using Godot.Collections;
using System;
using System.Configuration.Assemblies;
using System.Threading.Tasks.Dataflow;
using System.Threading.Tasks;
using System.ComponentModel;

public partial class barter : Control {

	public Node target;

	[Export]
	public RichTextLabel goldLabel;

	[Export]
	public RichTextLabel repLabel;

	[Export]
	public LineEdit offer;

	public static bool started = false;

	public override void _Ready() {
		goldLabel.Text = "Gold: " + GameState.gold.ToString();
	}

	public LineEdit startStuff(NPC_Resource npcr) {
		started = true;
		repLabel.Visible = true;
		offer.Visible = true;
		repLabel.Text = "Rep: " + npcr.Rep.ToString();

		return offer;
	}

	public void endStuff() {
		started = false;
		repLabel.Visible = false;
		offer.Visible = false;
	}

	public void repUpdate(NPC_Resource npcr) {
		repLabel.Text = "Rep: " + npcr.Rep.ToString();
	}

	public void goldUpdate() {
		goldLabel.Text = "Gold: " + GameState.gold.ToString();
	}

}



