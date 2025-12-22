using Godot;
using System;

public partial class DevicePlacing : Stage
{
	[Signal]
	public delegate void ToggleDeviceEventHandler();
	
	public override void Load()
	{
		Map map = MapRootNode as Map;
		Connect("ToggleDevice", new Callable(map, "AddDevice"));
	}
	
	public override void Input(InputEvent @event) {
		if (@event is InputEventMouseButton eventButton)
		{
			if (eventButton.Pressed)
			{
	   			var spaceState = GetWorld2D().DirectSpaceState;
				var query = new PhysicsPointQueryParameters2D();
				query.Position = GetGlobalMousePosition();
				query.CollideWithAreas = true;
				
				var result = spaceState.IntersectPoint(query, 1);
				
				if (result.Count > 0)
				{
					Area2D collider = (Area2D)result[0]["collider"];
					
					if (collider.IsInGroup("placeholder"))
					{
						EmitSignal(SignalName.ToggleDevice);
					}
				}
			}
		}
	}
}
