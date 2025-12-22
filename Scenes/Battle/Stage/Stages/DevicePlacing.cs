using Godot;
using System;

public partial class DevicePlacing : Stage
{
	[Signal]
	public delegate void OnGridDeviceAddedEventHandler(Vector2I position);
	
	[Signal]
	public delegate void OnGridDeviceRemovedEventHandler(Vector2I position);
	
	int DeviceLimit = 2;
	
	public override void Load()
	{
		Map map = mapRootNode as Map;
		Connect("OnGridDeviceAdded", new Callable(map, "AddedDevice"));
		Connect("OnGridDeviceRemoved", new Callable(map, "RemovedDevice"));
	}
	
	void AddDevice(Vector2I position)
	{
		TileMapLayer entityLayer = mapGridNode.GetNode<TileMapLayer>("Entities");
		entityLayer.SetCell(position, 0, new Vector2I(0, 0), 1); // device
	}
	
	void RemoveDevice(Vector2I position)
	{
		TileMapLayer entityLayer = mapGridNode.GetNode<TileMapLayer>("Entities");
		entityLayer.SetCell(position, 0, new Vector2I(0, 0), 0); //placeholder
	}
	
	public override void Input(InputEvent @event) {
		if (@event is InputEventMouseButton eventButton)
		{
			if (eventButton.Pressed)
			{
				Vector2 globalMousePos = GetGlobalMousePosition();
				Vector2 localMousePos = MapGridNode.ToLocal(globalMousePos);
				TileMapLayer entityLayer = MapGridNode.GetNode<TileMapLayer>("Entities");
				Vector2I cellPos = entityLayer.LocalToMap(localMousePos);
				
				switch (entityLayer.GetCellAlternativeTile(cellPos))
				{
					case -1:
						break;
					case 0:
						AddDevice(cellPos);
						EmitSignal(SignalName.OnGridDeviceAdded, cellPos);
						break;
					case 1:
						RemoveDevice(cellPos);
						EmitSignal(SignalName.OnGridDeviceRemoved, cellPos);
						break;
					default:	
						break;
				}
				
				/*
	   			var spaceState = GetWorld2D().DirectSpaceState;
				var query = new PhysicsPointQueryParameters2D();
				query.Position = globalMousePos;
				query.CollideWithAreas = true;
				
				var result = spaceState.IntersectPoint(query, 2);
				
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
				}*/
			}
		}
	}
}
