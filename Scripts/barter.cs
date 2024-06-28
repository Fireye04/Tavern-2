using Godot;
using Godot.Collections;
using System;
using System.Configuration.Assemblies;
using System.Threading.Tasks.Dataflow;
using System.Threading.Tasks;

public partial class barter : Control {

	public Dictionary<string, int> resourceVals = new Dictionary<string, int>(){
		{"wine", 5},
		{"ale", 1}
	};

	[Export]
	public int gold = 100;

	public Node target;

	[Export]
	public RichTextLabel goldLabel;

	[Export]
	public RichTextLabel repLabel;

	[Export]
	public LineEdit offer;

	public static bool started = false;

	public override void _Ready() {

		goldLabel.Text = "Gold: " + gold.ToString();
	}

	public LineEdit startStuff(NPC_Resource npcr) {
		started = true;
		repLabel.Visible = true;
		offer.Visible = true;
		repLabel.Text = "Rep: " + npcr.Rep.ToString();

		return offer;
	}

	// private void _on_line_edit_text_submitted(string new_text) {
	// 	if (started) {
	// 		try {
	// 			GD.Print(new_text.ToInt() + 1);
	// 		} catch {
	// 			offer.AddThemeColorOverride("font_color", Color.Color8(255, 0, 0));
	// 			wait(1);
	// 			offer.RemoveThemeColorOverride("font_color");
	// 		}

	// 	}
	// }

	// private async void wait(int seconds) {
	// 	await Task.Delay(TimeSpan.FromMilliseconds(seconds * 1000));
	// }

}



