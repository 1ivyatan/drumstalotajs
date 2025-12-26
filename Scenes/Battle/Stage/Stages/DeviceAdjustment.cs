using Godot;
using System;

public partial class DeviceAdjustment : Stage
{
	EntityLayer entityLayer;
	
	public override void LoadStage()
	{
		Map map = mapRootNode as Map;	
		entityLayer = mapGridNode.GetNode<TileMapLayer>("EntityLayer") as EntityLayer;
		
	}
	
	public override void Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton eventButton)
		{
			if (eventButton.Pressed)
			{
				GD.Print("some press, dunno");
			}
		}
	}
}
