using Godot;
using System;

public interface INPC {
    public int getSpawnChance();

    void setUI(barter bItem);

    void init(NPC obj);
}
