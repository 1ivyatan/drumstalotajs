using Godot;
using System;

public partial class DevicePlacement : Stage
{
	public override void _ExitTree()
	{
		GD.Print(this);
		GD.Print("im being removed");
	}
}
