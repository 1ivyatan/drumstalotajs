using Godot;
using System;

public partial class Entities : TileMapLayer
{
	int maxDevices = 2;
	
	public void ToggleDevice(Vector2I position)
	{
		SetCell(position, 0, new Vector2I(0, 0));
		GD.Print("added device");
		GD.Print(position);
	}
}
