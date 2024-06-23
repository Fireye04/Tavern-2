using Godot;
using System;
using System.Threading.Tasks.Dataflow;

public partial class barter : Control {
	[Export]
	public int gold = 100;

	public Node target;

	[Export]
	public RichTextLabel goldLabel;

	[Export]
	public RichTextLabel repLabel;


	public override void _Ready() {

		goldLabel.Text = "Gold: " + gold.ToString();
	}

	public void startDialogue(NPC_Resource resource) {

		repLabel.Text = "Rep: " + resource.Rep.ToString();
	}

	public void fuckyou() {
		GD.Print("fuck you");
	}
}
