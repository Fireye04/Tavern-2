using Godot;
using System;

public interface INPC {
    public int getSpawnChance();

    void setUI(barter bItem, DayUI dui);

    void init(NPC obj);
}
