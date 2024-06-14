using Godot;
using System;
using System.Collections.Generic;
using System.Numerics;
public partial class PlayerInteraction : Area2D {

    private List<Node2D> interactablesInRange = new List<Node2D>();

    [Export]
    private CharacterBody2D player;


    private void _on_ready() {
        if (player == null) {
            player = (CharacterBody2D)GetParent();
        }
    }

    private void _on_body_entered(Node2D body) {
        if (body is IInteractable) {
            interactablesInRange.Add(body);

        }
    }

    private void _on_body_exited(Node2D body) {
        if (body is IInteractable) {
            if (interactablesInRange.Contains(body)) {
                interactablesInRange.Remove(body);

            }
        }
    }

    public override void _Input(InputEvent @event) {
        if (@event.IsActionPressed("interact")) {
            Node2D target = _find_nearest_interactable();

            IInteractable iTarget = (IInteractable)target;

            if (target != null) {
                iTarget.interact();
            } else {
                GD.Print("None in Range");
            }
        }

    }

    private Node2D _find_nearest_interactable() {
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

}
