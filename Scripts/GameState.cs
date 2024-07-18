using Godot;
using System;
using System.Collections.Generic;
using System.Transactions;

public partial class GameState : Node {

    //Actual game state
    public static State currentState = State.closed_morning;


    public static Dictionary<string, int> resourceVals = new Dictionary<string, int>(){
        {"wine", 5},
        {"ale", 1},
        {"rum", 3}
    };

    //Player posessions
    public static int gold = 100;

    public static Dictionary<string, int> inventory = new Dictionary<string, int>(){
        {"wine", 0},
        {"ale", 0},
        {"rum", 0},
    };

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
