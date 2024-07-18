using Godot;
using System;
using System.Collections.Generic;
using System.Transactions;

public partial class GameState : Node {
    public static State currentState = State.closed_morning;


    //Menu persistence
    public static List<(string, int)> currentMenu = new List<(string, int)>(){
        ("none", 0),
        ("none", 0)
    };

    public static List<string> recipies = new List<string> {
        "wine",
        "ale",
        "rum"
    };


    public static Dictionary<string, bool> availableRecipies = new Dictionary<string, bool>(){
        {"none", true},
        {"wine", true},
        {"ale", true},
        {"rum", true}
    };

}

public enum State {
    closed_morning,
    open,
    closed_afternoon
}
