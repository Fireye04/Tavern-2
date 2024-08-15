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
    public Control cont;

    [Export]
    public LineEdit offer;

    [Export]
    public LineEdit count;

    [Export]
    public Button confirm;

    [Export]
    public Label Price;

    public static bool started = false;

    public override void _Ready() {
        goldLabel.Text = "Gold: " + GameState.gold.ToString();
    }

    public (LineEdit, LineEdit) startStuff(NPC_Resource npcr, string res) {
        started = true;
        repLabel.Visible = true;
        cont.Visible = true;
        repLabel.Text = "Rep: " + npcr.Rep.ToString();
        Price.Text = "Going Price: " + (npcr.Name != "N-Ref" ? GameState.resourceVals[res] : GameState.resourceVals[res] + 1) + " each";


        return (offer, count);
    }

    public void endStuff() {
        started = false;
        repLabel.Visible = false;
        cont.Visible = false;
    }

    public void repUpdate(NPC_Resource npcr) {
        repLabel.Text = "Rep: " + npcr.Rep.ToString();
    }

    public void goldUpdate() {
        goldLabel.Text = "Gold: " + GameState.gold.ToString();
    }

}



