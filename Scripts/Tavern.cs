using Godot;
using System;

public partial class Tavern : Node2D {

	[Export]
	public DayUI day;

	[Export]
	public TableManager tabMan;

	public void openTavern() {
		// Order matters, nextstate upadtes based on value!
		// this is stupid but so am i
		GameState.currentState = State.open;
		day.nextState();
		tabMan.spawn();
	}

	public void closeTavern() {
		// Order matters, nextstate upadtes based on value!
		// this is stupid but so am i
		GameState.currentState = State.closed_afternoon;
		day.nextState();
		tabMan.clear();
	}


	public void endDay() {
		// Order matters, nextday upadtes based on value!
		// this is stupid but so am i
		GameState.currentState = State.closed_morning;
		day.nextDay();
	}
}
