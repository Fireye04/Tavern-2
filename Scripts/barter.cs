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

	public void startStuff(string rep) {
		repLabel.Visible = true;
		repLabel.Text = "Rep: " + rep;

	}

}
