using Godot;
using System;
using System.Collections.Generic;
using System.Transactions;

public partial class GameState : Node {

    //Actual game state
    public static State currentState = State.closed_morning;

    public static int getTRep() {
        return (int)Math.Ceiling(tavernRep);
    }

    public static float tavernRep = 0;

    public static Godot.Collections.Array<NPC_Resource> usedTravelers = new();

    //Resources
    public static Godot.Collections.Dictionary<string, int> resourceVals = new(){
        {"wine", 5},
        {"ale", 1},
        {"rum", 3},
        {"bread", 2},
        {"beef", 3},
        {"corn", 3},
        {"cheese", 2},
        {"milk", 2}
    };

    public static Dictionary<string, List<(string, int)>> RecipiesIngredients = new Dictionary<string, List<(string, int)>>(){
        {"wine", new List<(string, int)>{("wine", 1)}},
        {"ale", new List<(string, int)>{("ale", 1)}},
        {"rum", new List<(string, int)>{("rum", 1)}},
        {"milk", new List<(string, int)>{("milk", 1)}},
        {"sandwitch", new List<(string, int)>{("bread", 2), ("beef", 1), ("cheese", 1)}},
        {"corn", new List<(string, int)>{("corn", 1)}},
        {"steak", new List<(string, int)>{("beef", 2)}},
    };

    //Player posessions
    public static int gold = 100;

    public static Dictionary<string, int> inventory = new Dictionary<string, int>(){
        {"wine", 5},
        {"ale", 5},
        {"rum", 5},
        {"bread", 5},
        {"beef", 5},
        {"corn", 5},
        {"cheese", 5},
        {"milk", 5}

    };

    public static string held = "Nothing";

    //Menu persistence
    public static List<(string, int)> currentMenu = new List<(string, int)>(){
        ("none", 0),
        ("none", 0),
        ("none", 0),
        ("none", 0)
    };


    private static int getItemValue(string Item) {
        var total = 0;
        foreach (var ing in RecipiesIngredients[Item]) {
            total += resourceVals[ing.Item1] * ing.Item2;
        }

        return total;
    }

    public static int getPrices() {
        var total = 0;
        foreach (var item in currentMenu) {
            if (item.Item1 == "none") {
                continue;
            }

            total += getItemValue(item.Item1) - item.Item2;
        }
        return total;
    }

    public static List<string> recipies = new List<string> {
        "wine",
        "ale",
        "rum",
        "milk",
        "sandwitch",
        "corn",
        "steak"
    };


    public static Dictionary<string, bool> availableRecipies = new Dictionary<string, bool>(){
        {"none", true},
        {"wine", true},
        {"ale", true},
        {"milk", true},
        {"sandwitch", true},
        {"corn", true},
        {"steak", true},

    };

}

public enum State {
    closed_morning,
    open,
    closed_afternoon
}
