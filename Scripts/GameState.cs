using Godot;
using System;
using System.Collections.Generic;
using System.Transactions;

public partial class GameState : Node {

	//Actual game state
	public static State currentState = State.closed_morning;

	//Resources
	public static Dictionary<string, int> resourceVals = new Dictionary<string, int>(){
		{"wine", 5},
		{"ale", 1},
		{"rum", 3},
		{"bread", 2},
		{"beef", 3}
	};

	public static Dictionary<string, List<(string, int)>> RecipiesIngredients = new Dictionary<string, List<(string, int)>>(){
		{"wine", new List<(string, int)>{("wine", 1)}},
		{"ale", new List<(string, int)>{("ale", 1)}},
		{"rum", new List<(string, int)>{("rum", 1)}},
		{"sandwitch", new List<(string, int)>{("bread", 2), ("beef", 1)}},
		{"wine", new List<(string, int)>{("wine", 1)}},
		{"ale", new List<(string, int)>{("ale", 1)}},
		
	};

	//Player posessions
	public static int gold = 100;

	public static Dictionary<string, int> inventory = new Dictionary<string, int>(){
		{"wine", 5},
		{"ale", 5},
		{"rum", 5},
	};

	public static string held = "Nothing";

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
