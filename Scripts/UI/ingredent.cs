using Godot;
using System;
using System.ComponentModel.Design.Serialization;

public partial class ingredent : MarginContainer {
	[Export]
	public Label iName;

	[Export]
	public Label iCount;

	public void init(string name, string count) {
		iName.Text = name;
		iCount.Text = count;
	}
}
