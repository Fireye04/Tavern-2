using Godot;
using System;

public partial class GameState : Node {
    public static State currentState = State.closed_morning;

}

public enum State {
    closed_morning,
    open,
    closed_afternoon
}
