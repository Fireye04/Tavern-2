using Godot;
using System;
using System.Numerics;
using System.Collections.Generic;
using DialogueManagerRuntime;

public partial class PlayerController : CharacterBody2D {
	[Export]
	public int Speed { get; set; } = 400;

	private Godot.Vector2 _player_in = new();

	private List<Node2D> interactablesInRange = new List<Node2D>();

	private CharacterBody2D player;

	[Export]
	private Resource rejectionText;


	private void _on_ready() {
		player = (CharacterBody2D)GetParent();
	}

	private void _on_interaction_range_body_entered(Node2D body) {
		if (body is IInteractable) {
			interactablesInRange.Add(body);

		}
	}

	private void _on_interaction_range_body_exited(Node2D body) {
		if (body is IInteractable) {
			if (interactablesInRange.Contains(body)) {
				interactablesInRange.Remove(body);

			}
		}
	}


	public Node2D find_nearest_interactable() {
		if (interactablesInRange.Count == 1) {
			return interactablesInRange[0];

		} else if (interactablesInRange.Count > 1) {

			(Node2D, float) closest = default;

			for (int i = 0; i < interactablesInRange.Count; i++) {
				Godot.Vector2 itemPos = interactablesInRange[i].GlobalPosition;

				float distance = GlobalPosition.DistanceTo(itemPos);

				if (closest.Item1 is null) {
					closest = (interactablesInRange[i], distance);
				} else {
					if (distance <= closest.Item2) {
						closest = (interactablesInRange[i], distance);
					}
				}
			}

			return closest.Item1;

		} else {
			return null;
		}
	}



	public override void _UnhandledInput(InputEvent @event) {

		if (@event.IsActionPressed("interact")) {
			Node2D target = find_nearest_interactable();

			if (target != null) {

				IInteractable iTarget = (IInteractable)target;
				if (iTarget.canInteract()) {
					iTarget.interact();
					_player_in = new Godot.Vector2();
					return;
				} else {
					DialogueManager.ShowExampleDialogueBalloon(rejectionText, "start");
				}
			} else {
				GD.Print("None in Ranga");
			}
		}

		_player_in = Input.GetVector("left", "right", "up", "down");
	}



	public override void _PhysicsProcess(double delta) {
		Velocity = _player_in * Speed;
		MoveAndSlide();
	}
}

