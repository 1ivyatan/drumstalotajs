using Godot;
using System;

public partial class DevicePlacing : Stage
{
	[Signal]
	public delegate void ToggleDeviceEventHandler(Vector2I position);
	
	public override void Load()
	{
		Map map = MapRootNode as Map;
		Connect("ToggleDevice", new Callable(map, "ToggleDevice"));
	}
	
	public override void Input(InputEvent @event) {
		if (@event is InputEventMouseButton eventButton)
		{
			if (eventButton.Pressed)
			{
				Vector2 globalMousePos = GetGlobalMousePosition();
				
	   			var spaceState = GetWorld2D().DirectSpaceState;
				var query = new PhysicsPointQueryParameters2D();
				query.Position = globalMousePos;
				query.CollideWithAreas = true;
				
				var result = spaceState.IntersectPoint(query, 1);
				
				if (result.Count > 0)
				{
					Area2D collider = (Area2D)result[0]["collider"];
					
					if (collider.IsInGroup("placeholder"))
					{
						Vector2 localPos = MapGridNode.ToLocal(globalMousePos);
						TileMapLayer selectorLayer = MapGridNode.GetNode<TileMapLayer>("Placeholders");
						EmitSignal(SignalName.ToggleDevice, selectorLayer.LocalToMap(localPos));
					}
				}
			}
		}
	}
}
