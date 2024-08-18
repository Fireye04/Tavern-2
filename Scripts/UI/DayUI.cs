using Godot;
using System;

public partial class DayUI : Control {
    public int currentDay = 0;

    [Export]
    public dayLabel dl;

    [Export]
    public StatusLabel sl;

    [Export]
    public Label held;

    public void nextDay() {
        currentDay += 1;
        dl.dayUpdate();
        sl.stateUpdate();
        updateHeld(GameState.held);
    }

    public void nextState() {
        sl.stateUpdate();
    }

    public void updateHeld(string item) {
        held.Text = "Holding: " + item;
    }
}
