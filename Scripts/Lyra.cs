using Godot;
using System;

public partial class Lyra : Node {

    private NPC_Resource npc_Resource;

    public override void _Ready() {
        npc_Resource = GD.Load<NPC_Resource>("res://Resources/Lyra_Silverwind.tres");

    }

}
