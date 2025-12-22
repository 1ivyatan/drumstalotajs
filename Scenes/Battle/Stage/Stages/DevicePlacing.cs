using Godot;
using System;

public partial class DevicePlacing : Stage
{
	[Signal]
	public delegate void ToggledDeviceInGridEventHandler(Vector2I position);
	
	public override void Load()
	{
		Map map = MapRootNode as Map;
		Connect("ToggledDeviceInGrid", new Callable(map, "ToggledDeviceInGrid"));
	}
	
	void ToggleDevice(Vector2I position)
	{
		TileMapLayer entityLayer = MapGridNode.GetNode<TileMapLayer>("Entities");
		entityLayer.SetCell(position, 0, new Vector2I(0, 0));
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
						Vector2I tilePos = selectorLayer.LocalToMap(localPos);
						
						ToggleDevice(tilePos);
						EmitSignal(SignalName.ToggledDeviceInGrid, tilePos);
					}
				}
			}
		}
	}
}
