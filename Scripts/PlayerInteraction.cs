using Godot;
using System;
public partial class PlayerInteraction : Area2D
{
	private void _on_body_entered(Node2D body)
	{
		if (body is IInteractable){
			IInteractable iBody = (IInteractable)body;
			iBody.interact();
		}
	}
}









