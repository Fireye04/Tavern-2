using Godot;
using System;
using System.Collections.Generic;
public partial class PlayerInteraction : Area2D
{

	private List<IInteractable> interactablesInRange = new List<IInteractable>();

	private void _on_body_entered(Node2D body)
	{
		if (body is IInteractable){
			IInteractable iBody = (IInteractable)body;
			interactablesInRange.Add(iBody);
			
		}
	}

	private void _on_body_exited(Node2D body)
	{
		if (body is IInteractable){
			IInteractable iBody = (IInteractable)body;
			if (interactablesInRange.Contains(iBody)){
				interactablesInRange.Remove(iBody);
				
			}
		}
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("interact")){
			IInteractable target = _find_nearest_interactable();

			if (target != null) {
				target.interact();
			}
		}
		
	}

	private IInteractable _find_nearest_interactable(){
		if (interactablesInRange.Count >= 1){

			return interactablesInRange[0];
		} else {
			return null;
		}
	}

}
