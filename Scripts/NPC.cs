using DialogueManagerRuntime;
using Godot;
using System;

public abstract class NPC : IInteractable {


    private Resource _dialogue;

    private bool can_interact;

    public string Name;
    public int Rep;


    public bool canInteract() {
        return can_interact;
    }

    public abstract void interact();
}

